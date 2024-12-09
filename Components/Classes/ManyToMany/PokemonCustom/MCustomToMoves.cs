using PokemonTeamBuilder.Components.Classes.ManyToMany.BasePokemon;
using PokemonTeamBuilder.Components.Classes.UsersData;

namespace PokemonTeamBuilder.Components.Classes.ManyToMany.PokemonCustom
{
    // Many to Many connection between CustomPokemon and MPokemonToMoves to allow users to add specific moves to thier pokemon
    public class MCustomToMoves
    {
        public int MCustomToMovesId { get; set; }
        public int CustomPokemonId { get; set; }
        public CustomPokemon CustomPokemon { get; set; }
        public int MPokemonToMovesId { get; set; }
        public MPokemonToMoves MPokemonToMoves { get; set; }
    }
}
