namespace PokemonTeamBuilder.Components.Classes.UsersData
{
    // A place for users to store teams of pokemon they made, unfinished implimentation
    public class UserTeam
    {
        public int UserTeamId { get; set; }
        public string UserTeamName { get; set; }
        public ICollection<CustomPokemon> CustomPokemons { get; set; } = new List<CustomPokemon>();

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
