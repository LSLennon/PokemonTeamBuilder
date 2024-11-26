﻿using Microsoft.EntityFrameworkCore;
using PokemonTeamBuilder.Components.Classes;
using PokemonTeamBuilder.Components.Classes.ManyToMany;

namespace PokemonTeamBuilder.Data
{
    public class PokedexServices
    {
        private readonly ApplicationDbContext _context;

        public PokedexServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pokemon>> GetPokedexList()
        {
            return await _context.Pokemons
                .Include(p => p.PokemonTypes)
                .ThenInclude(pt => pt.PokeType)
                .ToListAsync();
        }

        
        public async Task<Pokemon> GetPokemonByName(string name)
        {
            return await _context.Pokemons
                .Include(p => p.Abilities)
                .ThenInclude(a => a.PokeAbility)
                .Include(p => p.PokemonTypes)
                .ThenInclude(pt => pt.PokeType)
                .Include(p => p.Moves)
                .ThenInclude(m => m.PokeMove)
                .Include(p => p.Moves)
                .ThenInclude(m => m.PokeMethod)
                .Include(p => p.BaseStats)
                .AsSplitQuery()
                .FirstOrDefaultAsync(p => p.PokemonName == name);

        }

        public async Task<TypeChart> GetTypeInfo(List<MPokemonToTypes> pokeTypes)
        {
            TypeChart typeChart = new TypeChart();
            Dictionary<string, double> effectChart = new Dictionary<string, double>();

            foreach (var type in pokeTypes)
            {
                var damages = _context.TypeEffectivenesses.Where(te => te.DefenceType == type.PokeType);
                foreach (var damage in damages)
                {
                    var attackTypeName = damage.AttackType.PokeTypeName;

                    if (!effectChart.ContainsKey(attackTypeName))
                    {
                        effectChart[attackTypeName] = damage.DamageCalculation;
                    }
                    else
                    {
                        effectChart[attackTypeName] *= damage.DamageCalculation;
                    }
                }
            }

            foreach (var effect in effectChart)
            {
                string typeName = effect.Key;
                double value = effect.Value;

                if (value == 4)
                {
                    typeChart.X4Weakness.Add(typeName);
                }
                else if (value == 2)
                {
                    typeChart.x2Weakness.Add(typeName);
                }
                else if (value == 0)
                {
                    typeChart.NoDamage.Add(typeName);
                }
                else if (value == 0.5)
                {
                    typeChart.X2Resist.Add(typeName);
                }
                else if (value == 0.25)
                {
                    typeChart.X4Resist.Add(typeName);
                }
            }

            return typeChart;
        }


        public async Task<Pokemon> GetPokemonById(int Id)
        {
            return await _context.Pokemons.FirstOrDefaultAsync(p => p.PokemonId == Id);
        }
        
    }
}