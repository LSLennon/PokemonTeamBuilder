using PokeApiNet;

namespace PokemonTeamBuilder.Components.Classes.BasePokemonSubClasses
{
    public class TypeEffectiveness
    {
        public int TypeEffectivnessId { get; set; }

        public int UserTypeId { get; set; }
        public AppPokemonType UserType { get; set; } = null!;

        public int TargetTypeId { get; set; }
        public AppPokemonType TargetType { get; set; } = null!;

        public double Multiplier { get; set; }
    }
}
