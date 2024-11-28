using PokemonTeamBuilder.Components.Classes.ManyToMany.BasePokemon;

namespace PokemonTeamBuilder.Components.Classes.PokemonData
{
    public class PokeMove
    {
        public int PokeMoveId { get; set; }
        public string MoveName { get; set; }
        public int? Accuracy { get; set; }
        public PokeType MoveType { get; set; }
        public int? PP { get; set; }
        public int? Power { get; set; }
        public string DamgeClass { get; set; }
        public string FlavourText { get; set; }
        public string? MachineName { get; set; }
        public ICollection<MPokemonToMoves> Moves { get; set; } = new List<MPokemonToMoves>();
    }
}
