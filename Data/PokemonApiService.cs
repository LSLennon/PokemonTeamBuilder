using PokeApiNet;
using PokemonTeamBuilder.Components.Classes;
using PokemonTeamBuilder.Components.Classes.DatabaseClasses;
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

        public async Task<PokedexPokemon> GetPokemonForDatabase(int NationalDexNumber, List<PokeType> pokeTypes)
        {
            var apiRetrun = await pokeClient.GetResourceAsync<Pokemon>(NationalDexNumber);
            List<Type> allTypes = await pokeClient.GetResourceAsync(apiRetrun.Types.Select(type => type.Type));
            var pokedexPokemon = new PokedexPokemon
            {
                PokedexPokemonId = apiRetrun.Id.ToString(),
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

        public async Task<BasePokemon> GetPokemon(string IndividualName, List<PokeType> pokeTypes)
        {
            var apiRetrun = await pokeClient.GetResourceAsync<Pokemon>(IndividualName);
            var basePokemon = new BasePokemon
            {
                BasePokemonId = apiRetrun.Id.ToString(),
                PokemonName = apiRetrun.Name,
                Height = apiRetrun.Height/10, //coverts from decimeters to cm
                Weight = apiRetrun.Weight/100, //converts from hectograms to gramms
            };
            List<Type> allTypes = await pokeClient.GetResourceAsync(apiRetrun.Types.Select(type => type.Type));
            foreach (var type in allTypes)
            {
                var testedType = GetType(type, pokeTypes);
                if (testedType != null)
                {
                    basePokemon.Types.Add(testedType);
                }
            };
            foreach (var ability in apiRetrun.Abilities)
            {
                basePokemon.Abilities.Add(await GetAbility(ability));
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

        public PokeType GetType(Type type, List<PokeType> defenceTypes)
        {
            var matchingPokeType = defenceTypes.FirstOrDefault(at => at.PokeTypeName.Equals(type.Name, StringComparison.OrdinalIgnoreCase));
            if (matchingPokeType != null)
            {
                return matchingPokeType;
            }
            return null;
        }

        public async Task<PokeAbilities> GetAbility(PokemonAbility ability)
        {
            var apiRetrun = await pokeClient.GetResourceAsync<Ability>(ability.Ability);
            var EnglishFlavourTexts = apiRetrun.FlavorTextEntries
                .Where(ft => ft.Language.Name == "en");
            string NewestFlavourText = "";
            int NewestFlavourIndex = 0;
            foreach (var item in EnglishFlavourTexts)
            {
                int id = int.Parse(item.VersionGroup.Url.Substring(item.VersionGroup.Url.Length - 3).Trim('/'));
                if (id > NewestFlavourIndex)
                {
                    NewestFlavourText = item.FlavorText;
                }
            }
            PokeAbilities returnPokeAbility = new PokeAbilities
            {
                AbilityName = apiRetrun.Name,
                AbilityDescription = NewestFlavourText,
            };
            return returnPokeAbility;
        }


        public async Task<PokeStats> GetStats(List<PokemonStat> stats)
        {
            PokeStats returnPokeStats = new PokeStats();
            foreach (var sta in stats)
            {
                if(sta.Stat.Name == "hp")
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
        /*
                        public async Task<PokeMove> GetMove()
                        {

                        }
                        */
    }
}
