namespace PokemonTeamBuilder.Components.Classes.DatabaseClasses
{
    public class UserTeam
    {
        public int UserTeamId { get; set; }
        public string UserTeamName { get; set; }
        public ICollection<CustomPokemon> CustomPokemons { get; set; } = new List<CustomPokemon>();

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
