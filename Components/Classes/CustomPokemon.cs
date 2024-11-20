using PokemonTeamBuilder.Components.Classes.BasePokemonSubClasses;

namespace PokemonTeamBuilder.Components.Classes
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


        public Stats CustomPokemonEVs { get; set; }
        public Stats CustomPokemonIVs { get; set; }

        public int CustomPokemonEVsId { get; set; }
        public int CustomPokemonIVsId { get;set; }
    }
}
