using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PokeApiNet;
using PokemonTeamBuilder.Components.Classes;
using PokemonTeamBuilder.Components.Classes.ManyToMany;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonTeamBuilder.Data
{
    public class PokemonApiServices
    {
        private readonly ApplicationDbContext _context;

        public PokemonApiServices(ApplicationDbContext context)
        {
            _context = context;
        }

        PokeApiClient pokeClient = new PokeApiClient();

        public async Task<List<PokeType>> GetTypeList()
        {
            return await _context.PokeTypes.ToListAsync();
        }

        public async Task<List<PokeMove>> GetMoveList()
        {
            return await _context.PokeMoves.ToListAsync();
        }

        public async Task<List<PokeMethod>> GetMethodList()
        {
            return await _context.PokeMethods.ToListAsync();
        }

        public async Task GetPokemonForDatabase(int nationalDexNumber)
        {
            var apiReturn = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(nationalDexNumber);
            var pokemon = new Components.Classes.Pokemon
            {
                PokemonId = apiReturn.Id,
                PokemonName = apiReturn.Name,
                PokedexEntry = await GetPokedexEntry(apiReturn.Species.Name),
                Height = apiReturn.Height,
                Weight = apiReturn.Weight,
                BaseStats = await GetStats(apiReturn.Stats),
                Image = await GetImage(apiReturn.Id)
            };

            foreach (var type in apiReturn.Types)
            {
                var typeToAdd = await TypeAsync(type.Type.Name);
                var link = new MPokemonToTypes
                {
                    Pokemon = pokemon,
                    PokeType = typeToAdd,
                };
                pokemon.PokemonTypes.Add(link);
            }
            foreach (var move in apiReturn.Moves)
            {
                var moveToAdd = await MoveAsync(move.Move.Name);
                var link = new MPokemonToMoves
                {
                    Pokemon = pokemon,
                    PokeMove = moveToAdd,
                    Level = move.VersionGroupDetails.Last().LevelLearnedAt,
                    PokeMethod = await MethodAsync(move.VersionGroupDetails.Last().MoveLearnMethod),
                };
                pokemon.Moves.Add(link);
            }
            foreach (var ability in apiReturn.Abilities)
            {
                var abilityToAdd = await AbilityAsync(ability.Ability.Name);
                var link = new MPokemonToAbilities
                {
                    Pokemon = pokemon,
                    PokeAbility = abilityToAdd,
                };
                pokemon.Abilities.Add(link);
            }
            _context.Add(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetImage(int pokemonId)
        {
            string paddedId = pokemonId.ToString("D3"); // "D3" formats the number to 3 digits

            string url = $"https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/{paddedId}.png";

            return url;
        }

        public async Task<PokeType> TypeAsync(string item)
        {
            var dataCheck = _context.PokeTypes
                .FirstOrDefault(t => t.PokeTypeName == item);
            if (dataCheck != null)
            {
                return dataCheck;
            }
            else
            {
                PokeType pokeType = new PokeType
                {
                    PokeTypeName = item,
                };
                _context.PokeTypes.Add(pokeType);
                await _context.SaveChangesAsync();
                return pokeType;
            }
        }

        public async Task<PokeMove> MoveAsync(string item)
        {
            var dataCheck = _context.PokeMoves
                .FirstOrDefault(t => t.MoveName == item);
            if (dataCheck != null)
            {
                return dataCheck;
            }
            else
            {
                var apiMove = await pokeClient.GetResourceAsync<Move>(item);
                PokeMove move = new PokeMove
                {
                    PokeMoveId = apiMove.Id,
                    MoveName = apiMove.Name,
                    Accuracy = apiMove.Accuracy,
                    MoveType = await TypeAsync(apiMove.Type.Name),
                    PP = apiMove.Pp,
                    Power = apiMove.Power,
                    DamgeClass = apiMove.DamageClass.Name,
                    FlavourText = apiMove.FlavorTextEntries
                            .Where(ft => ft.Language.Name == "en")
                            .LastOrDefault()?.FlavorText ?? "N/A",
                };
                _context.PokeMoves.Add(move);
                await _context.SaveChangesAsync();
                return move;
            }
        }

        public async Task<PokeMethod> MethodAsync(NamedApiResource<MoveLearnMethod> item)
        {
            var dataCheck = _context.PokeMethods
                .FirstOrDefault(t => t.Name == item.Name);
            if (dataCheck != null)
            {
                return dataCheck;
            }
            else
            {
                var apiReturn = await pokeClient.GetResourceAsync<MoveLearnMethod>(item);
                PokeMethod pokeMethod = new PokeMethod
                {
                    Name = apiReturn.Name,
                    Description = apiReturn.Descriptions.Where(ft => ft.Language.Name == "en")
                        .LastOrDefault()?.Description ?? "N/A",
                };
                _context.PokeMethods.Add(pokeMethod);
                await _context.SaveChangesAsync();
                return pokeMethod;
            }
        }

        public async Task<PokeAbility> AbilityAsync(string item)
        {
            var dataCheck = _context.PokeAbilities
               .FirstOrDefault(t => t.AbilityName == item);
            if (dataCheck != null)
            {
                return dataCheck;
            }
            else
            {
                var apiAbility = await pokeClient.GetResourceAsync<Ability>(item);
                PokeAbility pokeAbility = new PokeAbility
                {
                    AbilityName = apiAbility.Name,
                    AbilityDescription = apiAbility.FlavorTextEntries
                            .Where(ft => ft.Language.Name == "en")
                            .LastOrDefault()?.FlavorText ?? "N/A",
                };
                _context.PokeAbilities.Add(pokeAbility);
                await _context.SaveChangesAsync();
                return pokeAbility;
            }
        }

        public async Task<string> GetPokedexEntry(string species)
        {
            var apireturn = await pokeClient.GetResourceAsync<PokemonSpecies>(species);
            var flavourText = apireturn.FlavorTextEntries
                .Where(ft => ft.Language.Name == "en");
            return flavourText.Last().FlavorText;
        }

        public async Task<PokeStats> GetStats(List<PokemonStat> stats)
        {
            PokeStats returnPokeStats = new PokeStats();
            foreach (var sta in stats)
            {
                if (sta.Stat.Name == "hp")
                {
                    returnPokeStats.HP = sta.BaseStat;
                }
                if (sta.Stat.Name == "attack")
                {
                    returnPokeStats.Attack = sta.BaseStat;
                }
                if (sta.Stat.Name == "defense")
                {
                    returnPokeStats.Defence = sta.BaseStat;
                }
                if (sta.Stat.Name == "special-attack")
                {
                    returnPokeStats.SpAttack = sta.BaseStat;
                }
                if (sta.Stat.Name == "special-defense")
                {
                    returnPokeStats.SpDefence = sta.BaseStat;
                }
                if (sta.Stat.Name == "speed")
                {
                    returnPokeStats.Speed = sta.BaseStat;
                }
            }
            return returnPokeStats;
        }

        public async Task GetEffectivenessForDatabase(List<PokeType> pokeTypes)
        {
            foreach (var pokeType in pokeTypes)
            {
                PokeApiNet.Type allTypes = await pokeClient.GetResourceAsync<PokeApiNet.Type>(pokeType.PokeTypeName);
                foreach (var type in allTypes.DamageRelations.NoDamageTo)
                {
                    var matchingAttackType = pokeTypes
                        .FirstOrDefault(at => at.PokeTypeName.ToLower() == type.Name.ToLower());
                    TypeEffectiveness addDmg = new TypeEffectiveness
                    {
                        AttackTypeId = pokeType.PokeTypeId,
                        DamageCalculation = 0,
                        DefenceTypeId = matchingAttackType.PokeTypeId,
                    };
                    _context.TypeEffectivenesses.Add(addDmg);
                }
                foreach (var type in allTypes.DamageRelations.HalfDamageTo)
                {
                    var matchingAttackType = pokeTypes
                        .FirstOrDefault(at => at.PokeTypeName.ToLower() == type.Name.ToLower());
                    TypeEffectiveness addDmg = new TypeEffectiveness
                    {
                        AttackTypeId = pokeType.PokeTypeId,
                        DamageCalculation = 0.5,
                        DefenceTypeId = matchingAttackType.PokeTypeId,
                    };
                    _context.TypeEffectivenesses.Add(addDmg);
                }
                foreach (var type in allTypes.DamageRelations.DoubleDamageTo)
                {
                    var matchingAttackType = pokeTypes
                        .FirstOrDefault(at => at.PokeTypeName.ToLower() == type.Name.ToLower());
                    TypeEffectiveness addDmg = new TypeEffectiveness
                    {
                        AttackTypeId = pokeType.PokeTypeId,
                        DamageCalculation = 2,
                        DefenceTypeId = matchingAttackType.PokeTypeId,
                    };
                    _context.TypeEffectivenesses.Add(addDmg);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
