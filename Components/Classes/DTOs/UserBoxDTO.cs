namespace PokemonTeamBuilder.Components.Classes.DTOs
{
    public class UserBoxDTO
    {
        public int UserBoxId { get; set; }
        public string UserBoxName { get; set; }
        public List<CustomPokemonDTO> CustomPokemons { get; set; } = new List<CustomPokemonDTO>();

    }
}
