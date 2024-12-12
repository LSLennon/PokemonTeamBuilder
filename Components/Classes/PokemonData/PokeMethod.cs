using PokemonTeamBuilder.Components.Classes.ManyToMany.BasePokemon;

namespace PokemonTeamBuilder.Components.Classes.PokemonData
{
    public class PokeMethod
    {
        public int PokeMethodId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<MPokemonToMoves> Abilities { get; set; } = new List<MPokemonToMoves>();
    }
}
