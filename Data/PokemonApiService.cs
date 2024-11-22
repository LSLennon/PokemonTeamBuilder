using PokeApiNet;
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

        public PokeType getType(Type type, List<PokeType> defenceTypes)
        {
            var matchingPokeType = defenceTypes.FirstOrDefault(at => at.PokeTypeName.Equals(type.Name, StringComparison.OrdinalIgnoreCase));
            if (matchingPokeType != null)
            {
                return matchingPokeType;
            }
            return null;
        }

        public async Task<PokedexPokemon> GetPokemonForDatabase(int NationalDexNumber, List<PokeType> pokeTypes)
        {
            var apiRetrun = await pokeClient.GetResourceAsync<Pokemon>(NationalDexNumber);
            List<Type> allTypes = await pokeClient.GetResourceAsync(apiRetrun.Types.Select(type => type.Type));
            PokedexPokemon pokedexPokemon = new PokedexPokemon
            {
                PokedexPokemonId = apiRetrun.Id.ToString(),
                PokemonName = apiRetrun.Name,
                DefenceType1 = getType(allTypes[0], pokeTypes),
                Sprite = apiRetrun.Sprites.FrontDefault,
            };
            if (allTypes.Count == 2)
            {
                pokedexPokemon.DefenceType2 = getType(allTypes[1], pokeTypes);
            }
            return pokedexPokemon;
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
