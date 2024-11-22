namespace PokemonTeamBuilder.Components.Classes.DatabaseClasses
{
    public class PokedexPokemon
    {
        public string PokedexPokemonId { get; set; } //NationalDexNumber
        public string PokemonName { get; set; }
        public DefenceType DefenceType1 { get; set; }
        public DefenceType? DefenceType2 { get; set; }
    }
}
