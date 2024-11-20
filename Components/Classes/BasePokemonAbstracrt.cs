using PokemonTeamBuilder.Components.Classes.BasePokemonSubClasses;

namespace PokemonTeamBuilder.Components.Classes
{
    public abstract class BasePokemonAbstract
    {
        public string BasePokemonId { get; set; } //NationalDexNumber
        public string PokemonName { get; set; }
        public List<AppPokemonType> Types { get; set; } = new List<AppPokemonType>();
        public double Weight { get; set; }
        public double Height { get; set; }
        public List<AppPokemonAbilities> Abilities { get; set; } = new List<AppPokemonAbilities>();
        public List<AppStats> Stats { get; set; } = new List<AppStats>();
        public AppEvolutionInfromation EvoInfo { get; set; } = new AppEvolutionInfromation();
        public List<AppPokemonMoves> Moves { get; set; } = new List<AppPokemonMoves>();
        public List<string> Sprites { get; set; } = new List<string>();
    }
}
