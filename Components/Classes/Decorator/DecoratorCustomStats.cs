using PokeApiNet;
using PokemonTeamBuilder.Components.Classes.PokemonData;

namespace PokemonTeamBuilder.Components.Classes.Decorator
{
    public class DecoratorCustomStats : DecoratorPokeStats
    {
        public event Action OnStatChanged;

        public DecoratorCustomStats(AbstractPokeStats baseStats) : base(baseStats)
        {
        }

        // Override the properties and trigger OnStatChanged when they change
        private int _hp;
        public override int HP
        {
            get => _hp;
            set
            {
                if (_hp != value)
                {
                    _hp = value;
                    NotifyStatsChanged();
                }
            }
        }

        private int _attack;
        public override int Attack
        {
            get => _attack;
            set
            {
                if (_attack != value)
                {
                    _attack = value;
                    NotifyStatsChanged();
                }
            }
        }

        private int _defence;
        public override int Defence
        {
            get => _defence;
            set
            {
                if (_defence != value)
                {
                    _defence = value;
                    NotifyStatsChanged();
                }
            }
        }

        private int _spAttack;
        public override int SpAttack
        {
            get => _spAttack;
            set
            {
                if (_spAttack != value)
                {
                    _spAttack = value;
                    NotifyStatsChanged();
                }
            }
        }

        private int _spDefence;
        public override int SpDefence
        {
            get => _spDefence;
            set
            {
                if (_spDefence != value)
                {
                    _spDefence = value;
                    NotifyStatsChanged();
                }
            }
        }

        private int _speed;
        public override int Speed
        {
            get => _speed;
            set
            {
                if (_speed != value)
                {
                    _speed = value;
                    NotifyStatsChanged();
                }
            }
        }

        // This method will be used to notify listeners that the stats have changed
        private void NotifyStatsChanged() => OnStatChanged?.Invoke();

        // StatTotal can either be delegated to the base class or overridden with custom logic
        public override int StatTotal()
        {
            return HP + Attack + Defence + SpAttack + SpDefence + Speed;
        }
    }


}
