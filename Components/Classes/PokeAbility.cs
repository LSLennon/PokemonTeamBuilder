using PokemonTeamBuilder.Components.Classes.ManyToMany;

namespace PokemonTeamBuilder.Components.Classes
{
    public class PokeAbility
    {
        public int PokeAbilityId { get; set; }
        public string AbilityName { get; set; }
        public string AbilityDescription { get; set; }
        public ICollection<MPokemonToAbilities> Abilities { get; set; } = new List<MPokemonToAbilities>();
    }
}
