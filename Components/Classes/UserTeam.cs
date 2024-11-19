namespace PokemonTeamBuilder.Components.Classes
{
    public class UserTeam
    {
        public string UserTeamId { get; set; }
        public string UserTeamName { get; set; }
        public ICollection<CustomPokemon> CustomPokemons { get; set; } = new List<CustomPokemon>();

        public string UserId { get; set; } 
        public User User { get; set; }
    }
}
