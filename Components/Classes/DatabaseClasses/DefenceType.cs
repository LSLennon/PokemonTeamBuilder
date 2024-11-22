namespace PokemonTeamBuilder.Components.Classes.DatabaseClasses
{
    public class DefenceType
    {
        public int DefenceTypeId { get; set; }
        public string DefenceTypeName { get; set; }
        public ICollection<TypeEffectiveness> Effectiveness { get; set; }
    }
}
