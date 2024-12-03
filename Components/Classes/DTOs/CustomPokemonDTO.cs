using PokemonTeamBuilder.Components.Classes.PokemonData;

namespace PokemonTeamBuilder.Components.Classes.DTOs
{
    public class CustomPokemonDTO
    {
        public string CustomPokemonNickname { get; set; }
        public int CustomPokemonId { get; set; }
        public int PokemonId { get; set; }
        public string PokemonName { get; set; }
        public string Image { get; set; }
        public List<PokeType> PokemonTypes { get; set; } = new List<PokeType>();
    }
}
