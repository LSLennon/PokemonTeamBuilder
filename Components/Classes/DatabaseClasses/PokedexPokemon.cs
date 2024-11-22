namespace PokemonTeamBuilder.Components.Classes.DatabaseClasses
{
    public class PokedexPokemon
    {
        public string PokedexPokemonId { get; set; } //NationalDexNumber
        public string PokemonName { get; set; }
        public PokeType DefenceType1 { get; set; }
        public PokeType? DefenceType2 { get; set; }
        public string Sprite {  get; set; }
    }
}
