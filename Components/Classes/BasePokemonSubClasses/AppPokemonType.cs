namespace PokemonTeamBuilder.Components.Classes.BasePokemonSubClasses
{
    public class AppPokemonType
    {
        public int AppPokemonTypeId { get; set; }
        public string TypeName { get; set; }
        public ICollection<TypeEffectiveness> Strengths { get; set; } = new List<TypeEffectiveness>();
        public ICollection<TypeEffectiveness> Weaknesses { get; set; } = new List<TypeEffectiveness>();

    }
}
