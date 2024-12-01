using PokemonTeamBuilder.Components.Classes.PokemonData;
using System.Reflection.Emit;

namespace PokemonTeamBuilder.Components.Classes.Decorator
{
    public class DecoratorCalculateStats : DecoratorPokeStats
    {
        private AbstractPokeStats _EVs;  

        private AbstractPokeStats _IVs; 

        private int _level; 

        private PokeNature _nature;

        public DecoratorCalculateStats(AbstractPokeStats baseStats, AbstractPokeStats EVs, AbstractPokeStats IVs, int level, PokeNature nature)
            : base(baseStats)
        {
            _EVs = EVs;
            _IVs = IVs;
            _level = level;
            _nature = nature;
        }

        public virtual int CalculateHP()
        {
            return ((2 * _baseStats.HP + _IVs.HP + (_EVs.HP / 4)) * _level / 100) + _level + 10;
        }

        public virtual int CalculateAttack()
        {
            return Convert.ToInt32(Math.Floor((((2 * _baseStats.Attack + _IVs.Attack + (_EVs.Attack / 4)) * _level / 100) + 5) * _nature.Attack));
        }

        public virtual int CalculateDefence()
        {
            return Convert.ToInt32(Math.Floor((((2 * _baseStats.Defence + _IVs.Defence + (_EVs.Defence / 4)) * _level / 100) + 5) * _nature.Defence));
        }

        public virtual int CalculateSpAttack()
        {
            return Convert.ToInt32(Math.Floor((((2 * _baseStats.SpAttack + _IVs.SpAttack + (_EVs.SpAttack / 4)) * _level / 100) + 5) * _nature.SpAttack));
        }

        public virtual int CalculateSpDefence()
        {
            return Convert.ToInt32(Math.Floor((((2 * _baseStats.SpDefence + _IVs.SpDefence + (_EVs.SpDefence / 4)) * _level / 100) + 5) * _nature.SpDefence));
        }

        public virtual int CalculateSpeed()
        {
            return Convert.ToInt32(Math.Floor((((2 * _baseStats.Speed + _IVs.Speed + (_EVs.Speed / 4)) * _level / 100) + 5) * _nature.Speed));
        }

        public int[] StatsArray()
        {
            return new int[]
            {
                CalculateHP(),
                CalculateAttack(),
                CalculateDefence(),
                CalculateSpAttack(),
                CalculateSpDefence(),
                CalculateSpeed()
            };
        }

        public override int StatTotal()
        {
            return CalculateHP() + CalculateAttack() + CalculateDefence() + CalculateSpAttack() + CalculateSpDefence() + CalculateSpeed();
        }

    }
}
