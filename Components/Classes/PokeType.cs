using PokemonTeamBuilder.Components.Classes.ManyToMany;

namespace PokemonTeamBuilder.Components.Classes
{
    public class PokeType
    {
        public int PokeTypeId { get; set; }
        public string PokeTypeName { get; set; }
        public ICollection<TypeEffectiveness> AttackEffectiveness { get; set; }
        public ICollection<TypeEffectiveness> DefenceEffectiveness { get; set; }
        public ICollection<MPokemonToTypes> PokemonTypes { get; set; } = new List<MPokemonToTypes>();

    }
}
