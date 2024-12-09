using PokemonTeamBuilder.Components.Classes.ManyToMany.BasePokemon;
using PokemonTeamBuilder.Components.Classes.ManyToMany.PokemonCustom;
using PokemonTeamBuilder.Components.Classes.PokemonData;

namespace PokemonTeamBuilder.Components.Classes.Decorator
{
    public class DecoratorCustomPokemonNotify : DecoratorCustomPokemon
    {
        //thsi decorator adds notificatiosn to certain atributes so the chart can be updated in real time
        public DecoratorCustomPokemonNotify(AbstractCustomPokemon customPoke) : base(customPoke) { }

        private int _CustomPokemonLevel;
        //makes sure the users level is in the requiered range
        public override int CustomPokemonLevel
        {
            get => _CustomPokemonLevel;
            set
            {
                if (value < 1)
                {
                    _CustomPokemonLevel = 1; 
                }
                else if (value > 100)
                {
                    _CustomPokemonLevel = 100; 
                }
                else
                {
                    _CustomPokemonLevel = value;
                }
                NotifyAtributeChanged();
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
