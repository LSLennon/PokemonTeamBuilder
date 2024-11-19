using PokemonTeamBuilder.Components.Classes.BasePokemonSubClasses;

namespace PokemonTeamBuilder.Components.Classes
{
    public class CustomPokemon
    {
        public string CustomPokemonId { get; set; }
        public string CustomPokemonNickname { get; set; }
        public int CustomPokemonLevel { get; set; }
        public List<string> CustomPokemonMoves { get; set; }
        public string CustomPokemonAbility { get; set; }
        public string CustomPokemonHeldItem { get; set; }
        public string BasePokemonId { get; set; }

        public string UserTeamId { get; set; }
        public UserTeam UserTeam { get; set; }


        public Stats CustomPokemonEVs { get; set; }
        public Stats CustomPokemonIVs { get; set; }

        public string CustomPokemonEVsId { get; set; }
        public string CustomPokemonIVsId { get;set; }
    }
}
