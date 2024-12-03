using PokemonTeamBuilder.Components.Classes.UsersData;

namespace PokemonTeamBuilder.Components.Classes.ManyToMany.PokemonCustom
{
    public class MCustomToTeams
    {
        public int MCustomToTeamsId { get; set; }

        public int CustomPokemonId { get; set; }
        public CustomPokemon CustomPokemon { get; set; }

        public int UserTeamId { get; set; }
        public UserTeam UserTeam { get; set; }
    }
}
