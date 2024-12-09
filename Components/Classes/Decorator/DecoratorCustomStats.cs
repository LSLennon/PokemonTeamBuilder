namespace PokemonTeamBuilder.Components.Classes.Decorator
{
    public class DecoratorCustomStats : DecoratorPokeStats
    {
        public event Action OnStatChanged;

        private const int MaxEV = 252;
        private const int MaxTotalEV = 508;
        private const int MaxIV = 31;

        private StatType _statType;
        public StatType StatType
        {
            get => _statType;
            set
            {
                if (_statType != value)
                {
                    _statType = value;
                    NotifyStatsChanged();
                }
            }
        }

        private int _hp, _attack, _defence, _spAttack, _spDefence, _speed;
        public override int HP
        {
            get => _hp;
            set => SetStat(ref _hp, value);
        }
        public override int Attack
        {
            get => _attack;
            set => SetStat(ref _attack, value);
        }
        public override int Defence
        {
            get => _defence;
            set => SetStat(ref _defence, value);
        }
        public override int SpAttack
        {
            get => _spAttack;
            set => SetStat(ref _spAttack, value);
        }
        public override int SpDefence
        {
            get => _spDefence;
            set => SetStat(ref _spDefence, value);
        }
        public override int Speed
        {
            get => _speed;
            set => SetStat(ref _speed, value);
        }

        public int TotalValue => HP + Attack + Defence + SpAttack + SpDefence + Speed;

        private int TotalEV => _hp + _attack + _defence + _spAttack + _spDefence + _speed;

        //used to make sure Evs stay within their requiered range on the Custom Pokemon screen. also adds event listener so chart can be updated
        private void SetStat(ref int stat, int value)
        {
            switch (StatType)
            {
                case StatType.EV:
                    if (value <= MaxEV && TotalEV + value - stat <= MaxTotalEV)
                    {
                        stat = value;
                        NotifyStatsChanged();
                    }
                    break;
                case StatType.IV:
                    if (value <= MaxIV)
                    {
                        stat = value;
                        NotifyStatsChanged();
                    }
                    break;
            }
        }

        private void NotifyStatsChanged() => OnStatChanged?.Invoke();

        public DecoratorCustomStats(AbstractPokeStats baseStats, StatType statType) : base(baseStats)
        {
            _statType = statType;
        }

        public override int StatTotal()
        {
            return TotalValue; // Adjusted to show the correct total based on the stats
        }
    }


    public enum StatType
    {
        EV,
        IV
    }

}
