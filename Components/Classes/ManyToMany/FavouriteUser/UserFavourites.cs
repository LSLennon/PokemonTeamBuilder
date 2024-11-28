using PokemonTeamBuilder.Components.Classes.PokemonData;
using PokemonTeamBuilder.Components.Classes.UsersData;

namespace PokemonTeamBuilder.Components.Classes.ManyToMany.FavouriteUser
{
    public class UserFavourites
    {
        public int UserFavouritesId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
    }
}
