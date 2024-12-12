using PokemonTeamBuilder.Components.Classes.UsersData;

namespace PokemonTeamBuilder.Components.Classes.ManyToMany.PokemonCustom
{
    // Many to Many connection between CustomPokemon and UserTeam so Users Can build Teams
    public class MCustomToTeams
    {
        public int MCustomToTeamsId { get; set; }

        public int CustomPokemonId { get; set; }
        public CustomPokemon CustomPokemon { get; set; }

        public int UserTeamId { get; set; }
        public UserTeam UserTeam { get; set; }
    }
}
