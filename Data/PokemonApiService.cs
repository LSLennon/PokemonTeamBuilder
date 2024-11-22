using PokeApiNet;
using PokemonTeamBuilder.Components.Classes;
using PokemonTeamBuilder.Components.Classes.DatabaseClasses;
using Type = PokeApiNet.Type;

namespace PokemonTeamBuilder.Data
{
    public class PokemonApiService
    {
        PokeApiClient pokeClient = new PokeApiClient();
        public async Task<List<AttackType>> AddTypeNames()
        {
            List<AttackType> pokemonTypes = new List<AttackType>();
            await foreach (var typeRef in pokeClient.GetAllNamedResourcesAsync<Type>())
            {
                AttackType typeToAdd = new AttackType();
                typeToAdd.AttackTypeName = typeRef.Name;
                pokemonTypes.Add(typeToAdd);
            }
            return pokemonTypes;
        }

        public async Task<List<TypeEffectiveness>> GetEffectiveness(List<AttackType> attackTypes)
        {
            List<TypeEffectiveness> damageCalculationsToAdd = new List<TypeEffectiveness>();
            foreach (var attackType in attackTypes)
            {
                Type allTypes = await pokeClient.GetResourceAsync<Type>(attackType.AttackTypeName);
                foreach (var type in allTypes.DamageRelations.NoDamageTo)
                {
                    var matchingAttackType = attackTypes.FirstOrDefault(at => at.AttackTypeName.Equals(type.Name, StringComparison.OrdinalIgnoreCase));
                    TypeEffectiveness addDmg = new TypeEffectiveness
                    {
                        AttackTypeId = attackType.AttackTypeId,
                        DamageCalculation = 0,
                        DefenceTypeId = matchingAttackType.AttackTypeId,
                    };
                    damageCalculationsToAdd.Add(addDmg);
                }
                foreach (var type in allTypes.DamageRelations.HalfDamageTo)
                {
                    var matchingAttackType = attackTypes.FirstOrDefault(at => at.AttackTypeName.Equals(type.Name, StringComparison.OrdinalIgnoreCase));
                    TypeEffectiveness addDmg = new TypeEffectiveness
                    {
                        AttackTypeId = attackType.AttackTypeId,
                        DamageCalculation = 0.5,
                        DefenceTypeId = matchingAttackType.AttackTypeId,
                    };
                    damageCalculationsToAdd.Add(addDmg);
                }
                foreach (var type in allTypes.DamageRelations.DoubleDamageTo)
                {
                    var matchingAttackType = attackTypes.FirstOrDefault(at => at.AttackTypeName.Equals(type.Name, StringComparison.OrdinalIgnoreCase));
                    TypeEffectiveness addDmg = new TypeEffectiveness
                    {
                        AttackTypeId = attackType.AttackTypeId,
                        DamageCalculation = 2,
                        DefenceTypeId = matchingAttackType.AttackTypeId,
                    };
                    damageCalculationsToAdd.Add(addDmg);
                }
            }
            return damageCalculationsToAdd;

        }

        public async Task<Pokemon> GetPokemonForDatabase(int NationalDexNumber, List<DefenceType> defenceTypes)
        {
            var apiRetrun = await pokeClient.GetResourceAsync<Pokemon>(NationalDexNumber);
            List<Type> allTypes = await pokeClient.GetResourceAsync(apiRetrun.Types.Select(type => type.Type));
            var Type1 = getType(allTypes[0], defenceTypes);
            var Type2 = new DefenceType();
            if(allTypes.Count == 2)
            {
                Type2 = getType(allTypes[1], defenceTypes);
            }
            PokedexPokemon pokedexPokemon = new PokedexPokemon
            {
                PokedexPokemonId = apiRetrun.Id.ToString(),
                PokemonName = apiRetrun.Name,
                DefenceType1 = Type1,
            };
            
            
        }

        public DefenceType getType(Type type, List<DefenceType> defenceTypes)
        {
            var matchingDefenceType = defenceTypes.FirstOrDefault(at => at.DefenceTypeName.Equals(type.Name, StringComparison.OrdinalIgnoreCase));
            if (matchingDefenceType != null)
            {
                return matchingDefenceType;
            }
            return null;
        }

        /*
        public async Task<BasePokemon> GetPokemon(string IndividualName)
        {
            var apiRetrun = await pokeClient.GetResourceAsync<Pokemon>(IndividualName);
            var basePokemon = new BasePokemon
            {
                BasePokemonId = apiRetrun.Id.ToString(),
                PokemonName = apiRetrun.Name,
            };
            List<Type> allTypes = await pokeClient.GetResourceAsync(apiRetrun.Types.Select(type => type.Type));
            foreach (var type in allTypes)
            {
                AppPokemonType pokemonType = new AppPokemonType
                {
                   TypeName = type.Name,
                };
                basePokemon.Types.Add(pokemonType);
            }
            return basePokemon;
        }
        */
    }
}
