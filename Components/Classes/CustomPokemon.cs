namespace PokemonTeamBuilder.Components.Classes
{
    public class CustomPokemon
    {
        public int CustomPokemonId { get; set; }
        public string CustomPokemonNickname { get; set; }
        public int CustomPokemonLevel { get; set; }
        public string BasePokemonId { get; set; }
        public  ICollection<PokeMove> CustomPokemonMoves { get; set; }
        public  PokeAbility CustomPokemonAbility { get; set; }
        public  HeldItem CustomPokemonHeldItem { get; set; }

        public int UserTeamId { get; set; }
        public  UserTeam UserTeam { get; set; }

        public int CustomPokemonEVsId { get; set; }
        public  PokeStats CustomPokemonEVs { get; set; }

        public int CustomPokemonIVsId { get; set; }
        public  PokeStats CustomPokemonIVs { get; set; }
    }
}
