using PokemonTeamBuilder.Components.Classes.ManyToMany.BasePokemon;
using PokemonTeamBuilder.Components.Classes.ManyToMany.FavouriteUser;

namespace PokemonTeamBuilder.Components.Classes.PokemonData
{
    public class Pokemon
    {
        public int PokemonId { get; set; }
        public string PokemonName { get; set; }
        public string PokedexEntry { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int BaseStatsId { get; set; }
        public string Image { get; set; }
        public PokeStats BaseStats { get; set; } = new PokeStats();
        public ICollection<MPokemonToAbilities> Abilities { get; set; } = new List<MPokemonToAbilities>();
        public ICollection<MPokemonToMoves> Moves { get; set; } = new List<MPokemonToMoves>();
        public ICollection<MPokemonToTypes> PokemonTypes { get; set; } = new List<MPokemonToTypes>();
    }
}
