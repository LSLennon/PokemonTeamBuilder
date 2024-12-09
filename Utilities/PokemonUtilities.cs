using System.Globalization;

namespace PokemonTeamBuilder.Utilities
{
    public static class PokemonUtilities
    {
        //used because i kept forgetting to include this whenevr i rebuilt the database. took an hour everythime so i never reddid it just to remove this
        public static string CapitalizeName(this string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
        }
    }
}
