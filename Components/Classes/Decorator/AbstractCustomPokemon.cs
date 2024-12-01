using PokemonTeamBuilder.Components.Classes.ManyToMany.PokemonCustom;
using PokemonTeamBuilder.Components.Classes.PokemonData;
using PokemonTeamBuilder.Components.Classes.UsersData;

namespace PokemonTeamBuilder.Components.Classes.Decorator
{
    public abstract class AbstractCustomPokemon
    {
        public int CustomPokemonId { get; set; }
        public string CustomPokemonNickname { get; set; }
        public virtual int CustomPokemonLevel { get; set; }
        public virtual ICollection<MCustomToMoves> CustomPokemonMoves { get; set; }
        public virtual PokeAbility CustomPokemonAbility { get; set; }
        public HeldItem CustomPokemonHeldItem { get; set; }
        public virtual PokeNature CustomPokemonNature { get; set; } = new PokeNature();

        public int PokemonId { get; set; }
        public PokemonData.Pokemon Pokemon { get; set; }
        public int UserTeamId { get; set; }
        public UserTeam UserTeam { get; set; }

        public int CustomPokemonEVsId { get; set; }
        public PokeStats CustomPokemonEVs { get; set; }

        public int CustomPokemonIVsId { get; set; }
        public PokeStats CustomPokemonIVs { get; set; }
    }
}
