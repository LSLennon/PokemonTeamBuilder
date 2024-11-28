using PokemonTeamBuilder.Components.Classes.ManyToMany.FavouriteUser;

namespace PokemonTeamBuilder.Components.Classes.UsersData
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();
        public ICollection<UserFavourites> UserFavourites { get; set; } = new List<UserFavourites>();
    }
}
