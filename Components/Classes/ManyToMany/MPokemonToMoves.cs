﻿namespace PokemonTeamBuilder.Components.Classes.ManyToMany
{
    public class MPokemonToMoves
    {
        public int MPokemonToMovesId { get; set; }
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public int PokeMoveId { get; set; }
        public PokeMove PokeMove { get; set; }
        public int PokeMethodId { get; set; }
        public PokeMethod PokeMethod { get; set; }
        public int Level { get; set; }

    }
}
