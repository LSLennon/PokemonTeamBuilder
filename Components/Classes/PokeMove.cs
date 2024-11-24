using PokemonTeamBuilder.Components.Classes.DatabaseClasses;

namespace PokemonTeamBuilder.Components.Classes
{
    public class PokeMove
    {
        public int PokeMoveId { get; set; }
        public string MoveName { get; set; }
        public int Accuracy { get; set; }
        public PokeType MoveType { get; set; }
        public string PP { get; set; }
        public string Power { get; set; }
        public string DamgeClass { get; set; }
        public string FlavourText { get; set; }
    }
}
