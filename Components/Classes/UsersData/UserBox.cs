namespace PokemonTeamBuilder.Components.Classes.UsersData
{
    // A place for users to store pokemon they made and sort them, unfinished implimentation
    public class UserBox
    {
        public int UserBoxId { get; set; }
        public string UserBoxName { get; set;}
        public ICollection<CustomPokemon> CustomPokemons { get; set; } = new List<CustomPokemon>();

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
