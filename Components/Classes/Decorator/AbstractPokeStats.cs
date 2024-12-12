namespace PokemonTeamBuilder.Components.Classes.Decorator
{
    public abstract class AbstractPokeStats
    {
        public virtual int HP { get; set; }
        public virtual int Attack { get; set; }
        public virtual int Defence { get; set; }
        public virtual int SpAttack { get; set; }
        public virtual int SpDefence { get; set; }
        public virtual int Speed { get; set; }
        public abstract int StatTotal();
    }

}
