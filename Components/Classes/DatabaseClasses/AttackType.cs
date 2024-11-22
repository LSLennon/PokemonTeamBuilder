namespace PokemonTeamBuilder.Components.Classes.DatabaseClasses
{
    public class AttackType
    {
        public int AttackTypeId { get; set; }
        public string AttackTypeName { get; set; }
        public ICollection<TypeEffectiveness> Effectiveness { get; set; }

    }
}
