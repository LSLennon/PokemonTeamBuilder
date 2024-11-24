using System.Globalization;

namespace PokemonTeamBuilder.Utilities
{
    public static class PokemonUtilities
    {
        public static string CapitalizeName(this string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
        }
    }
}
