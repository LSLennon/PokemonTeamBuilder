namespace PokemonTeamBuilder.Components.Classes.DatabaseClasses
{
    public class MovesLearnedByPokemon
    {
        public PokeMethod PokeMethod{ get; set; }
        public int PokeMethodId { get; set; }
        public int Level { get; set; }
        public int PokeMoveId { get; set; }
        public PokeMove PokeMove { get; set; }
        public int PokedexPokemonId { get; set; }
        public PokedexPokemon PokedexPokemon { get; set; }

    }
}
