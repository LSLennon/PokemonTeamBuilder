using PokemonTeamBuilder.Components.Classes.DatabaseClasses;

namespace PokemonTeamBuilder.Components.Classes
{
    public abstract class BasePokemonAbstract
    {
        public string BasePokemonId { get; set; } //NationalDexNumber
        public string PokemonName { get; set; }
        public string PokedexEntry {  get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public List<PokeType> Types { get; set; } = new List<PokeType>();
        public List<AppAbilities> Abilities { get; set; } = new List<AppAbilities>();
        public List<string> Sprites { get; set; } = new List<string>();
        public List<AppStats> Stats { get; set; } = new List<AppStats>();
        public List<AppMove> Moves { get; set; } = new List<AppMove>();
       
    }
}
