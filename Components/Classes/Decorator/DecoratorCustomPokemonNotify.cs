using PokemonTeamBuilder.Components.Classes.ManyToMany.BasePokemon;
using PokemonTeamBuilder.Components.Classes.ManyToMany.PokemonCustom;
using PokemonTeamBuilder.Components.Classes.PokemonData;

namespace PokemonTeamBuilder.Components.Classes.Decorator
{
    public class DecoratorCustomPokemonNotify : DecoratorCustomPokemon
    {
        public DecoratorCustomPokemonNotify(AbstractCustomPokemon customPoke) : base(customPoke) { }

        private int _CustomPokemonLevel;
        public override int CustomPokemonLevel
        {
            get => _CustomPokemonLevel;
            set
            {
                if (_CustomPokemonLevel != value)
                {
                    _CustomPokemonLevel = value;
                    NotifyAtributeChanged();
                }
            }
        }

        private PokeNature _customPokemonNature;
        public override PokeNature CustomPokemonNature
        {
            get => _customPokemonNature;
            set
            {
                if (_customPokemonNature != value)
                {
                    _customPokemonNature = value;
                    NotifyAtributeChanged();
                }
            }
        }


        public event Action OnAtributeChanged;
        private void NotifyAtributeChanged() => OnAtributeChanged?.Invoke();
    }
}
