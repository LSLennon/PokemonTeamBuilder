namespace PokemonTeamBuilder.Components.Classes.DatabaseClasses
{
    public class CustomPokemon
    {
        public int CustomPokemonId { get; set; }
        public string CustomPokemonNickname { get; set; }
        public int CustomPokemonLevel { get; set; }
        public List<string> CustomPokemonMoves { get; set; }
        public string CustomPokemonAbility { get; set; }
        public string CustomPokemonHeldItem { get; set; }
        public string BasePokemonId { get; set; }

        public int UserTeamId { get; set; }
        public UserTeam UserTeam { get; set; }


        public AppStats CustomPokemonEVs { get; set; }
        public AppStats CustomPokemonIVs { get; set; }

        public int CustomPokemonEVsId { get; set; }
        public int CustomPokemonIVsId { get; set; }
    }
}
