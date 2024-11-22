namespace PokemonTeamBuilder.Components.Classes.DatabaseClasses
{
    public class TypeEffectiveness
    {
        public int TypeEffectivnessId { get; set; }
        public double DamageCalculation { get; set; }
        public int AttackTypeId { get; set; }
        public AttackType AttackType { get; set; }
        public int DefenceTypeId { get; set; }
        public DefenceType DefenceType { get; set; }
    }
}
