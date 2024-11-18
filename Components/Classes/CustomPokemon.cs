namespace PokemonTeamBuilder.Components.Classes
{
    public class CustomPokemon
    {
        public string CustomPokemonId { get; set; }
        public string CustomPokemonNickname { get; set; }
        public int CustomPokemonLevel { get; set; }
        public ICollection<Stats> CustomPokemonEVs { get; set; }
        public ICollection<Stats> CustomPokemonIVs { get; set; }
        public List<string> CustomPokemonMoves { get; set; }
        public string CustomPokemonAbility { get; set; }
        public string CustomPokemonHeldItem { get; set; }
        public string BasePokemonId { get; set; }
    }
}
