namespace PokemonTeamBuilder.Components.Classes.DatabaseClasses
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public ICollection<UserTeam> UserTeams { get; set; }
    }
}
