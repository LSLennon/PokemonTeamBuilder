using PokeApiNet;
using PokemonTeamBuilder.Components.Classes;
using PokemonTeamBuilder.Components.Classes.BasePokemonSubClasses;
using Type = PokeApiNet.Type;

namespace PokemonTeamBuilder.Data
{
    public class PokemonApiService
    {
        PokeApiClient pokeClient = new PokeApiClient();

        public async Task<List<AppPokemonType>> AddTypeNames()
        {
            List<AppPokemonType> pokemonTypes = new List<AppPokemonType>();
            await foreach (var typeRef in pokeClient.GetAllNamedResourcesAsync<Type>())
            {
                AppPokemonType typeToAdd = new AppPokemonType();
                typeToAdd.TypeName = typeRef.Name;
                pokemonTypes.Add(typeToAdd);
            }
            return pokemonTypes;
        }
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
    }
}
