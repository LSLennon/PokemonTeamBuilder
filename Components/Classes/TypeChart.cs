namespace PokemonTeamBuilder.Components.Classes
{
    public class TypeChart
    {
        public List<string> X4Resist { get; set; } = new List<string>();
        public List<string> X2Resist { get; set; } = new List<string>();
        public List<string> NoDamage { get; set; } = new List<string>();
        public List<string> x2Weakness { get; set; } = new List<string>();
        public List<string> X4Weakness { get; set; } = new List<string>();
    }

    public class EffectivnessChart
    {
        public string Name { get; set; } = "";
        public double Value { get; set; } = 0;
    }
}
