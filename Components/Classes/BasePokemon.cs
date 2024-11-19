using PokemonTeamBuilder.Components.Classes.BasePokemonSubClasses;

namespace PokemonTeamBuilder.Components.Classes
{
    public abstract class BasePokemon
    {
        public string BasePokemonId { get; set; } //NationalDexNumber
        public string PokemonName { get; set; }
        public List<PokemonType> Types { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public List<PokemonAbilities> Abilities { get; set; }
        public List<Stats> Stats { get; set; }
        public EvolutionInfromation EvoInfo { get; set; }
        public List<PomkemonMoves> Moves { get; set; }
        public List<string> Sprites { get; set; }
    }
}
