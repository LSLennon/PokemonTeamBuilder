using PokemonTeamBuilder.Components.Classes.PokemonData;

namespace PokemonTeamBuilder.Components.Classes.ManyToMany.BasePokemon
{
    // Many to Many connection between Pokemon and PokeTypes
    public class MPokemonToTypes
    {
        public int MPokemonToTypesId { get; set; }
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public int PokeTypeId { get; set; }
        public PokeType PokeType { get; set; }

    }
}
