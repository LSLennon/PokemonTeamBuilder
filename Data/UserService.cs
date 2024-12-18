﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using PokemonTeamBuilder.Components.Classes.DTOs;
using PokemonTeamBuilder.Components.Classes.ManyToMany.FavouriteUser;
using PokemonTeamBuilder.Components.Classes.PokemonData;
using PokemonTeamBuilder.Components.Classes.UsersData;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace PokemonTeamBuilder.Data
{
    public class UserService
    {

        private readonly ApplicationDbContext _context;

        private readonly PokemonAuthenticationService _authStateProvider;

        public UserService(ApplicationDbContext context, PokemonAuthenticationService authStateProvider)
        {
            _context = context;
            _authStateProvider = authStateProvider;
        }

        public async Task AddUserAsync(User FH)
        {
            _context.Users.Add(FH);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(ps => ps.UserId == id);

        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(ps => ps.UserName == username);

        }

        public async Task<bool> RepeatUsernameTest(string username)
        {
            User repeat = await _context.Users.FirstOrDefaultAsync(ps => ps.UserName == username);
            if (repeat != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task UpdateUserAsync(User FH)
        {
            _context.Users.Update(FH);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RegisterUser(string username, string password)
        {
            string salt = UserService.GenerateSalt();
            string passwordHash = UserService.HashPassword(password, salt);

            var user = new User
            {
                UserName = username,
                PasswordHash = passwordHash,
                Salt = salt
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                return false;
            }

            string hashedInputPassword = UserService.HashPassword(password, user.Salt);

            if (hashedInputPassword == user.PasswordHash)
            {
                _authStateProvider.SetUser(username);
                return true;
            }
            return false;
        }

        public void Logout()
        {
            _authStateProvider.ClearUser();
        }

        public static string GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32
            );
            return Convert.ToBase64String(hashed);
        }

        public async Task<List<UserFavourites>> GetUserFavourites(string username)
        {
            var user = await GetUserByUsernameAsync(username);
            return await _context.UserFavourites.Where(u => u.User == user)
                .ToListAsync();
        }

        public async Task<UserFavourites> AddUserFavourite(Pokemon pokemon, string username)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user != null)
            {
                UserFavourites newFav = new UserFavourites
                {
                    Pokemon = pokemon,
                    User = user,
                    PokemonId = pokemon.PokemonId,
                    UserId = user.UserId
                };
                user.UserFavourites.Add(newFav);
                await _context.SaveChangesAsync();
                return newFav;
            }
            return null;
        }

        public async Task<UserFavourites> RemoveUserFavourite(Pokemon pokemon, string username)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user != null)
            {
                var removeFav = await _context.UserFavourites.FirstOrDefaultAsync(rf => rf.Pokemon == pokemon);
                if (removeFav != null)
                {
                    user.UserFavourites.Remove(removeFav);
                    await _context.SaveChangesAsync();
                    return removeFav;
                }
            }
            return null;
        }

        public async Task<bool> AddUserBox(string userBox, string username)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user != null)
            {
                UserBox box = new UserBox
                {
                    UserBoxName = userBox,
                    User = user,
                    UserId = user.UserId
                };
                user.UserBox.Add(box);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AddUserTeam(string userTeam, string username)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user != null)
            {
                UserTeam team = new UserTeam
                {
                    UserTeamName = userTeam,
                    User = user,
                    UserId = user.UserId
                };
                user.UserTeams.Add(team);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<List<UserBoxDTO>> GetListBoxes(string username)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user != null)
            {
                var result = await _context.UserBox
                    .Where(u => u.User == user)
                    .Include(u => u.CustomPokemons)
                        .ThenInclude(cp => cp.Pokemon)
                            .ThenInclude(p => p.PokemonTypes)
                    .Select(u => new UserBoxDTO
                    {
                        UserBoxId = u.UserBoxId,
                        UserBoxName = u.UserBoxName,
                        CustomPokemons = u.CustomPokemons.Select(cp => new CustomPokemonDTO
                        {
                            CustomPokemonNickname = cp.CustomPokemonNickname,
                            CustomPokemonId = cp.CustomPokemonId,
                            PokemonId = cp.Pokemon.PokemonId,
                            PokemonName = cp.Pokemon.PokemonName,
                            Image = cp.Pokemon.Image,
                            PokemonTypes = cp.Pokemon.PokemonTypes.Select(pt => pt.PokeType).ToList()
                        }).ToList()
                    })
                    .ToListAsync();

                return result; 
            }
            return null;


        }

        public async Task<List<UserTeam>> GetListTeams(string username)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user != null)
            {
                return await _context.UserTeams
                    .Where(u => u.User == user)
                .ToListAsync();
            }
            return null;

        }

        public async Task AddCustomPokemonToBox(CustomPokemon pokemon)
        {
            _context.CustomPokemons.Add(pokemon);
            await _context.SaveChangesAsync();
        }
    }

}