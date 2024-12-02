using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PokeApiNet;
using PokemonTeamBuilder.Components.Classes.ManyToMany;
using PokemonTeamBuilder.Components.Classes.ManyToMany.BasePokemon;
using PokemonTeamBuilder.Components.Classes.PokemonData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using static MudBlazor.CategoryTypes;

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

        public int moveCount = 0;
        public int typeCount = 0;
        public int abilityCount = 0;
        public int methodCount = 0;

        public async Task<List<PokeType>> GetTypeList()
        {
            return await _context.PokeTypes.ToListAsync();
        }

        public async Task<List<PokeMove>> GetMoveList()
        {
            return await _context.PokeMoves.ToListAsync();
        }

        public async Task GetPokemonForDatabase(int nationalDexNumber)
        {
            var apiReturn = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(nationalDexNumber);
            var gameVersion = await GetLastVersionGroup(apiReturn.Abilities.Last().Ability.Name);
            var pokemon = new Components.Classes.PokemonData.Pokemon
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
                foreach (var version in move.VersionGroupDetails)
                {
                    if (version.VersionGroup.Name == gameVersion.Name)
                    {
                        var moveToAdd = await MoveAsync(move.Move.Name, gameVersion);
                        var link = new MPokemonToMoves
                        {
                            Pokemon = pokemon,
                            PokeMove = moveToAdd,
                            Level = version.LevelLearnedAt,
                            PokeMethod = await MethodAsync(version.MoveLearnMethod),
                        };
                        pokemon.Moves.Add(link);
                    }
                }
            }
            foreach (var ability in apiReturn.Abilities)
            {
                var abilityToAdd = await AbilityAsync(ability.Ability.Name, gameVersion);
                var link = new MPokemonToAbilities
                {
                    Pokemon = pokemon,
                    PokeAbility = abilityToAdd,
                };
                pokemon.Abilities.Add(link);
            }
            Console.WriteLine($"Id: {pokemon.PokemonId} - Name: {pokemon.PokemonName}");
            Console.WriteLine($"Type Count: {typeCount}     Move Count: {moveCount}     Method Count: {methodCount}     Ability Count: {abilityCount}");
            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task<VersionGroup> GetLastVersionGroup(string name)
        {
            var apiReturn = await pokeClient.GetResourceAsync<Ability>(name);
            var apiVersion = await pokeClient.GetResourceAsync<VersionGroup>(apiReturn.FlavorTextEntries.Last().VersionGroup.Name);
            return apiVersion;
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
                typeCount += 1;
                await _context.SaveChangesAsync();
                return pokeType;
            }
        }

        public async Task<PokeMove> MoveAsync(string item, VersionGroup versionGroup)
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
                if (apiMove.Machines.FirstOrDefault(ap => ap.VersionGroup.Name == versionGroup.Name) != null)
                {
                    var apiMachine = await pokeClient.GetResourceAsync(apiMove.Machines.Select(m => m.Machine));
                    move.MachineName = apiMachine.FirstOrDefault(mn => mn.VersionGroup.Name == versionGroup.Name).Item.Name;
                }
                _context.PokeMoves.Add(move);
                moveCount += 1;
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
                methodCount += 1;
                await _context.SaveChangesAsync();
                return pokeMethod;
            }
        }

        public async Task<PokeAbility> AbilityAsync(string item, VersionGroup versionGroup)
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
                            .Where(ft => ft.VersionGroup.Name == versionGroup.Name)
                            .LastOrDefault()?.FlavorText ?? "N/A",
                };
                _context.PokeAbilities.Add(pokeAbility);
                abilityCount += 1;
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

        public async Task GetMoveFlavourText()
        {
            List<PokeMove> moves = await GetMoveList();
            foreach (var move in moves)
            {
                var apiMove = await pokeClient.GetResourceAsync<Move>(move.MoveName);
                move.FlavourText = apiMove.FlavorTextEntries
                            .Where(ft => ft.Language.Name == "en")
                            .LastOrDefault()?.FlavorText ?? "N/A";
                _context.PokeMoves.Update(move);
                Console.WriteLine($"{move.MoveName} Flavour Text: {move.FlavourText}");
                await _context.SaveChangesAsync();
            }
        }

        public async Task GetNatures()
        {
            var AllNatures = await pokeClient.GetNamedResourcePageAsync<Nature>(25, 0);
            foreach (var nature in AllNatures.Results)
            {
                var natureOne = await pokeClient.GetResourceAsync<Nature>(nature.Name);
                Console.WriteLine(natureOne.Name);
                PokeNature pokeNature = new PokeNature
                {
                    NatureName = nature.Name,
                };
                if (natureOne.IncreasedStat != null)
                {
                    Console.WriteLine(natureOne.IncreasedStat.Name);
                    if (natureOne.IncreasedStat.Name == "attack" || natureOne.DecreasedStat.Name == "attack")
                    {
                        if (natureOne.IncreasedStat.Name == "attack")
                        {
                            pokeNature.Attack = 1.1;
                        }
                        else
                        {
                            pokeNature.Attack = 0.9;
                        }
                    }
                    if (natureOne.IncreasedStat.Name == "defense" || natureOne.DecreasedStat.Name == "defense")
                    {
                        if (natureOne.IncreasedStat.Name == "defense")
                        {
                            pokeNature.Defence = 1.1;
                        }
                        else
                        {
                            pokeNature.Defence = 0.9;
                        }
                    }
                    if (natureOne.IncreasedStat.Name == "special-attack" || natureOne.DecreasedStat.Name == "special-attack")
                    {
                        if (natureOne.IncreasedStat.Name == "special-attack")
                        {
                            pokeNature.SpAttack = 1.1;
                        }
                        else
                        {
                            pokeNature.SpAttack = 0.9;
                        }
                    }
                    if (natureOne.IncreasedStat.Name == "special-defense" || natureOne.DecreasedStat.Name == "special-defense")
                    {
                        if (natureOne.IncreasedStat.Name == "special-defense")
                        {
                            pokeNature.SpDefence = 1.1;
                        }
                        else
                        {
                            pokeNature.SpDefence = 0.9;
                        }
                    }
                    if (natureOne.IncreasedStat.Name == "speed" || natureOne.DecreasedStat.Name == "speed")
                    {
                        if (natureOne.IncreasedStat.Name == "speed")
                        {
                            pokeNature.Speed = 1.1;
                        }
                        else
                        {
                            pokeNature.Speed = 0.9;
                        }
                    }
                }
                await _context.PokeNatures.AddAsync(pokeNature);
                await _context.SaveChangesAsync();

            }
        }

        public async Task GetHeldItems()
        {
            int[] itemCategoryIds = new int[]
        {
            3, 4, 5, 6, 7, //Berries
            12, 13, 14, 15, 16, 17, 18, 19, 42, 44, 45, 46 //Various Held Items
        };
            foreach(int i in itemCategoryIds)
            {
                var group = await pokeClient.GetResourceAsync<ItemCategory>(i);
                foreach(var item in group.Items)
                {
                    HeldItem heldItem = new HeldItem
                    {
                        HeldItemName = item.Name,
                        Description = "N/A Bug in api"
                    };
                    await _context.HeldItems.AddAsync(heldItem);
                    Console.WriteLine($"{heldItem.HeldItemName}");
                    await _context.SaveChangesAsync();
                }
            }
        } //Not Usable due to bug in API
    }
}
