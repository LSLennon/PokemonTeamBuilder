using PokemonTeamBuilder.Components.Classes.BasePokemonSubClasses;

namespace PokemonTeamBuilder.Components.Classes.PokedexDatabase
{
    public class PokedexPokemon
    {
        public string PokedexPokemonId { get; set; } //NationalDexNumber
        public string PokemonName { get; set; }
        public AppPokemonType PokemonType1 { get; set; }
        public AppPokemonType PokemonType2 { get; set; }
        public int PokemonType1Id { get; set; }
        public int PokemonType2Id { get; set; }
    }
}
