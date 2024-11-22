namespace PokemonTeamBuilder.Components.Classes.DatabaseClasses
{
    public class TypeEffectiveness
    {
        public int TypeEffectivnessId { get; set; }
        public double DamageCalculation { get; set; }
        public int AttackTypeId { get; set; }
        public PokeType AttackType { get; set; }
        public int DefenceTypeId { get; set; }
        public PokeType DefenceType { get; set; }
    }
}
