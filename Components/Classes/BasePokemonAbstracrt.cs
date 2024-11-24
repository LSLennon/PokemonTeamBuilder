using PokeApiNet;
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
        public List<PokeAbilities> Abilities { get; set; } = new List<PokeAbilities>();
        public List<PokeSprites> Sprites { get; set; } = new List<PokeSprites>();
        public List<PokeStats> Stats { get; set; } = new List<PokeStats>();
        public List<PokeMove> Moves { get; set; } = new List<PokeMove>();
       
    }
}
