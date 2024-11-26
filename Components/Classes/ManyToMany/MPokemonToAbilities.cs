namespace PokemonTeamBuilder.Components.Classes.ManyToMany
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
