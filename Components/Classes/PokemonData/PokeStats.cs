using PokemonTeamBuilder.Components.Classes.Decorator;

namespace PokemonTeamBuilder.Components.Classes.PokemonData
{
    public class PokeStats : AbstractPokeStats
    {
        public int PokeStatsId { get; set; }

        public override int StatTotal()
        {
           return HP + Attack + Defence + SpAttack + SpDefence + Speed;
        }

    }
}
