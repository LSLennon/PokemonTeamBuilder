using PokeApiNet;
using PokemonTeamBuilder.Components.Classes;
using PokemonTeamBuilder.Components.Classes.DatabaseClasses;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using static MudBlazor.CategoryTypes;
using Type = PokeApiNet.Type;

namespace PokemonTeamBuilder.Data
{
    public class PokemonApiService
    {
        PokeApiClient pokeClient = new PokeApiClient();
        public async Task<List<PokeType>> AddTypeNames()
        {
            List<PokeType> pokemonTypes = new List<PokeType>();
            await foreach (var typeRef in pokeClient.GetAllNamedResourcesAsync<Type>())
            {
                PokeType typeToAdd = new PokeType();
                typeToAdd.PokeTypeName = typeRef.Name;
                pokemonTypes.Add(typeToAdd);
            }
            return pokemonTypes;
        }

        public async Task<List<TypeEffectiveness>> GetEffectiveness(List<PokeType> pokeTypes)
        {
            List<TypeEffectiveness> damageCalculationsToAdd = new List<TypeEffectiveness>();
            foreach (var pokeType in pokeTypes)
            {
                Type allTypes = await pokeClient.GetResourceAsync<Type>(pokeType.PokeTypeName);
                foreach (var type in allTypes.DamageRelations.NoDamageTo)
                {
                    var matchingAttackType = pokeTypes.FirstOrDefault(at => at.PokeTypeName.Equals(type.Name, StringComparison.OrdinalIgnoreCase));
                    TypeEffectiveness addDmg = new TypeEffectiveness
                    {
                        AttackTypeId = pokeType.PokeTypeId,
                        DamageCalculation = 0,
                        DefenceTypeId = matchingAttackType.PokeTypeId,
                    };
                    damageCalculationsToAdd.Add(addDmg);
                }
                foreach (var type in allTypes.DamageRelations.HalfDamageTo)
                {
                    var matchingAttackType = pokeTypes.FirstOrDefault(at => at.PokeTypeName.Equals(type.Name, StringComparison.OrdinalIgnoreCase));
                    TypeEffectiveness addDmg = new TypeEffectiveness
                    {
                        AttackTypeId = pokeType.PokeTypeId,
                        DamageCalculation = 0.5,
                        DefenceTypeId = matchingAttackType.PokeTypeId,
                    };
                    damageCalculationsToAdd.Add(addDmg);
                }
                foreach (var type in allTypes.DamageRelations.DoubleDamageTo)
                {
                    var matchingAttackType = pokeTypes.FirstOrDefault(at => at.PokeTypeName.Equals(type.Name, StringComparison.OrdinalIgnoreCase));
                    TypeEffectiveness addDmg = new TypeEffectiveness
                    {
                        AttackTypeId = pokeType.PokeTypeId,
                        DamageCalculation = 2,
                        DefenceTypeId = matchingAttackType.PokeTypeId,
                    };
                    damageCalculationsToAdd.Add(addDmg);
                }
            }
            return damageCalculationsToAdd;

        }

        public async Task<BasePokemon> GetPokemon(string IndividualName, List<PokeType> pokeTypes, string LastVersion)
        {
            var apiRetrun = await pokeClient.GetResourceAsync<Pokemon>(IndividualName);
            var basePokemon = new BasePokemon
            {
                Height = apiRetrun.Height, //coverts from decimeters to cm
                Weight = apiRetrun.Weight, //converts from hectograms to gramms
                PokedexEntry = await GetPokedexEntry(IndividualName, LastVersion),
            };
            foreach (var ability in apiRetrun.Abilities)
            {
                basePokemon.Abilities.Add(await GetAbility(ability, LastVersion));
            }
            basePokemon.Sprites.Add(new PokeSprites
            {
                isDefault = true,
                Url = apiRetrun.Sprites.FrontDefault,
            });
            basePokemon.Sprites.Add(new PokeSprites
            {
                isDefault = false,
                Url = apiRetrun.Sprites.FrontShiny,
            });
            basePokemon.Stats.Add(await GetStats(apiRetrun.Stats));

            return basePokemon;
        }

        public PokeType GetType(dynamic type, List<PokeType> defenceTypes)
        {
            var matchingPokeType = defenceTypes.FirstOrDefault(at => at.PokeTypeName.Equals(type.Name, StringComparison.OrdinalIgnoreCase));
            if (matchingPokeType != null)
            {
                return matchingPokeType;
            }
            return null;
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

        public async Task<PokeAbilities> GetAbility(PokemonAbility ability, string LastVersion)
        {
            var apiRetrun = await pokeClient.GetResourceAsync<Ability>(ability.Ability);
            PokeAbilities returnPokeAbility = new PokeAbilities
            {
                AbilityName = apiRetrun.Name,
                AbilityDescription = await GetAbilityFlavourText(apiRetrun.FlavorTextEntries, LastVersion),
            };
            return returnPokeAbility;
        }

        public async Task<string> GetPokedexEntry(string species, string LastVersion)
        {
            var apireturn = await pokeClient.GetResourceAsync<PokemonSpecies>(species);
            return await GetPokedexFlavourText(apireturn.FlavorTextEntries, LastVersion);
        }

        public async Task<string> GetAbilityFlavourText(IEnumerable<dynamic> flavourTexts, string LastVersion)
        {
            var flavourText = flavourTexts
                .Where(ft => ft.Language.Name == "en")
                .Where(ft => ft.VersionGroup.Url.Substring(ft.VersionGroup.Url.Length - 3).Trim('/') == LastVersion);
            if (flavourText.Count() == 1)
            {
                return flavourText.First().FlavorText;
            }
            Console.WriteLine("Fuck");
            return null;
        }

        public async Task<string> GetPokedexFlavourText(List<PokemonSpeciesFlavorTexts> flavourTexts, string LastVersion)
        {

            var flavourText = flavourTexts
                .Where(ft => ft.Language.Name == "en");
            if (flavourText.Count() == 1)
            {
                return flavourText.Last().FlavorText;
            }
            Console.WriteLine("Fuck");
            return null;
        }

        public async Task<PokedexPokemon> GetPokemonForDatabase(int NationalDexNumber, List<PokeType> pokeTypes)
        {
            var apiRetrun = await pokeClient.GetResourceAsync<Pokemon>(NationalDexNumber);
            List<Type> allTypes = await pokeClient.GetResourceAsync(apiRetrun.Types.Select(type => type.Type));
            var pokedexPokemon = new PokedexPokemon
            {
                PokedexPokemonId = apiRetrun.Id,
                PokemonName = apiRetrun.Name,
                DefenceType1 = GetType(allTypes[0], pokeTypes),
                Sprite = apiRetrun.Sprites.FrontDefault,
            };
            if (allTypes.Count == 2)
            {
                pokedexPokemon.DefenceType2 = GetType(allTypes[1], pokeTypes);
            }
            return pokedexPokemon;
        }

        public async Task<PokeMove> GetMoveForDatabase(int MoveId, List<PokeType> moveTypes)
        {
            var apiReturn = await pokeClient.GetResourceAsync<Move>(MoveId);
            var pokeMove = new PokeMove
            {
                PokeMoveId = apiReturn.Id,
                MoveName = apiReturn.Name,
                Accuracy = apiReturn.Accuracy,
                MoveType = GetType(apiReturn.Type, moveTypes),
                PP = apiReturn.Pp,
                Power = apiReturn.Power,
                DamgeClass = apiReturn.DamageClass.Name,
                FlavourText = apiReturn.FlavorTextEntries
                .Where(ft => ft.Language.Name == "en")
                .LastOrDefault()?.FlavorText ?? "N/A",
            };
            return pokeMove;
        }

        public async Task<PokeMethod> GetMethodsForDatabase(int MethodId)
        {
            var apiReturn = await pokeClient.GetResourceAsync<MoveLearnMethod>(MethodId);
            var methodToAdd = new PokeMethod
            {
                Description = apiReturn.Descriptions.Last().Description,
                Name = apiReturn.Name,
            };
            return methodToAdd;
        }

        public async Task<string> UpdatedPokemon(PokedexPokemon pokemon)
        {
            var apiRetrun = await pokeClient.GetResourceAsync<PokemonSpecies>(pokemon.PokedexPokemonId);
            var EngFlavourText = apiRetrun.FlavorTextEntries
                .Where(ft => ft.Language.Name == "en");
            string NewestFlavourText = "";
            int NewestFlavourIndex = 0;
            foreach (var item in EngFlavourText)
            {
                int id = int.Parse(item.Version.Url.Substring(item.Version.Url.Length - 3).Trim('/'));
                if (id > NewestFlavourIndex)
                {
                    var secondApi = await pokeClient.GetResourceAsync<PokeApiNet.Version>(item.Version.Name);
                    NewestFlavourText = secondApi.VersionGroup.Url.Substring(secondApi.VersionGroup.Url.Length - 3).Trim('/');
                }
            }
            return NewestFlavourText;
        }

        public async Task<List<MovesLearnedByPokemon>> UpdatedMoves(PokedexPokemon pokemon, List<PokeMove> allMoves, List<PokeMethod> allMethods)
        {
            var apiRetrun = await pokeClient.GetResourceAsync<Pokemon>(pokemon.PokedexPokemonId);
            List<MovesLearnedByPokemon> movesLearned = new List<MovesLearnedByPokemon>();
            foreach (var move in apiRetrun.Moves)
            {
                MovesLearnedByPokemon pokeMove = new MovesLearnedByPokemon
                {
                    PokeMoveId = allMoves.Where(pm => pm.MoveName
                   .Equals(move.Move.Name, StringComparison.OrdinalIgnoreCase))
                   .Select(pm => pm.PokeMoveId)
                   .FirstOrDefault(),
                    Level = move.VersionGroupDetails.Last().LevelLearnedAt,
                    PokeMethodId = allMethods.Where(pm => pm.Name
                   .Equals(move.VersionGroupDetails.Last().MoveLearnMethod.Name, StringComparison.OrdinalIgnoreCase))
                   .Select(pm => pm.PokeMethodId)
                   .FirstOrDefault(),
                };
                movesLearned.Add(pokeMove);
            }
            return movesLearned;
        }
    }
}
