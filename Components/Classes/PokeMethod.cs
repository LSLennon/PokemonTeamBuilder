using PokemonTeamBuilder.Components.Classes.ManyToMany;

namespace PokemonTeamBuilder.Components.Classes
{
    public class PokeMethod
    {
        public int PokeMethodId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<MPokemonToMoves> Abilities { get; set; } = new List<MPokemonToMoves>();
    }
}
