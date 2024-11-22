namespace PokemonTeamBuilder.Components.Classes.DatabaseClasses
{
    public class PokeType
    {
        public int PokeTypeId { get; set; }
        public string PokeTypeName { get; set; }
        public ICollection<TypeEffectiveness> AttackEffectiveness { get; set; }
        public ICollection<TypeEffectiveness> DefenceEffectiveness { get; set; }

    }
}
