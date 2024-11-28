using PokemonTeamBuilder.Components.Classes.PokemonData;

namespace PokemonTeamBuilder.Components.Classes.ManyToMany.BasePokemon
{
    public class MPokemonToAbilities
    {
        public int MPokemonToAbilitiesId { get; set; }
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public int PokeAbilityId { get; set; }
        public PokeAbility PokeAbility { get; set; }

    }
}
