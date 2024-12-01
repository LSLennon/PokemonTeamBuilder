namespace PokemonTeamBuilder.Components.Classes.Decorator
{
    public abstract class DecoratorPokeStats : AbstractPokeStats
    {
        protected AbstractPokeStats _baseStats;


        public DecoratorPokeStats(AbstractPokeStats baseStats)
        {
            _baseStats = baseStats;
        }
    }
}
