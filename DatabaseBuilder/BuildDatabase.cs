using InclementEmeraldTeamBuilder.Components.Classes;
using InclementEmeraldTeamBuilder.Data;
using System.Text.RegularExpressions;

namespace InclementEmeraldTeamBuilder.DatabaseBuilder
{
    public class BuildDatabase
    {
        private readonly PokeDbContext _context;

        public BuildDatabase(PokeDbContext context)
        {
            _context = context;
        }

        public async Task AddAbilities()
        {
            string filePath = "DatabaseBuilder/GameData2/AbilitySource.txt";
            string[] lines = File.ReadAllLines(filePath);

            Dictionary<string, string> descriptions = new Dictionary<string, string>();
            Regex descriptionRegex = new Regex(@"static const u8 (?<SourceName>s\w+)Description\[\] = _\(\x22(?<Description>.+?)\x22\);");

            foreach (var line in lines)
            {
                var match = descriptionRegex.Match(line);
                if (match.Success)
                {
                    string sourceName = match.Groups["SourceName"].Value;
                    string description = match.Groups["Description"].Value;
                    descriptions[sourceName] = description;
                }

            }

            var mappingRegex = new Regex(@"\[(?<SourceName>ABILITY_\w+)\] = (?<DescriptionSource>s\w+)Description,");

            foreach (var line in lines)
            {
                var match = mappingRegex.Match(line);
                if (match.Success)
                {
                    string sourceName = match.Groups["SourceName"].Value;
                    string descriptionSource = match.Groups["DescriptionSource"].Value;

                    string name = sourceName
                        .Replace("ABILITY_", "")
                        .Replace("_", " ")
                        .ToLowerInvariant();
                    name = char.ToUpper(name[0]) + name.Substring(1);
                    if (descriptions.TryGetValue(descriptionSource, out string description))
                    {
                        Ability abilityToAdd = new Ability
                        {
                            AbilitySourceName = sourceName,
                            AbilityName = name,
                            AbilityDescription = description
                        };
                        Console.WriteLine(abilityToAdd.AbilityName);
                        _context.Abilities.Add(abilityToAdd);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task AddMoveNames()
        {

            string filePath = "DatabaseBuilder/GameData2/MoveSource1.txt";
            string fileContent = File.ReadAllText(filePath);

            var mappings = new Dictionary<string, string>();
            var mappingRegex = new Regex(@"\[(?<MoveName>MOVE_\w+)\] = (?<DescriptionSource>s\w+)Description,");

            foreach (Match match in mappingRegex.Matches(fileContent))
            {
                string moveName = match.Groups["MoveName"].Value;
                string descriptionSource = match.Groups["DescriptionSource"].Value;
                mappings[descriptionSource] = moveName;
            }

            var descriptions = new Dictionary<string, string>();
            var descriptionRegex = new Regex(@"static const u8 (?<DescriptionSource>s\w+)Description\[\] = _\(\x22(?<Description>(.|\n)*?)\x22\);");

            foreach (Match match in descriptionRegex.Matches(fileContent))
            {
                string descriptionSource = match.Groups["DescriptionSource"].Value;
                string description = match.Groups["Description"].Value.Replace("\n", " ").Trim();
                descriptions[descriptionSource] = description;
            }

            foreach (var mapping in mappings)
            {
                if (descriptions.TryGetValue(mapping.Key, out string description))
                {
                    string moveName = mapping.Value;
                    string name = moveName
                        .Replace("MOVE_", "")
                        .Replace("_", " ")
                        .ToLowerInvariant();
                    name = char.ToUpper(name[0]) + name.Substring(1);

                    Move moveToAdd = new Move
                    {
                        MoveName = moveName,
                        MoveSourceName = name,
                        MoveDescription = description
                    };
                    moveToAdd = await GetMoveData(moveToAdd);
                    Console.WriteLine(moveToAdd.MoveName);
                    _context.Moves.Add(moveToAdd);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<Move> GetMoveData(Move move)
        {
            string filePath = "DatabaseBuilder/GameData2/MoveSource2.txt";
            string fileContent = File.ReadAllText(filePath);

            var moveBlockRegex = new Regex($@"\[{Regex.Escape(move.MoveSourceName)}\]\s*=\s*\{{(?<Properties>.+?)\}}", RegexOptions.Singleline);

            Match moveMatch = moveBlockRegex.Match(fileContent);

            string propertiesBlock = moveMatch.Groups["Properties"].Value;

            var propertyRegex = new Regex(@"\.(?<Property>\w+)\s*=\s*(?<Value>[^,]+),");

            foreach (Match propertyMatch in propertyRegex.Matches(propertiesBlock))
            {
                string property = propertyMatch.Groups["Property"].Value.Trim();
                string value = propertyMatch.Groups["Value"].Value.Trim();

                switch (property)
                {
                    case "effect":
                        move.MoveEffect = value;
                        break;
                    case "power":
                        move.Power = int.Parse(value);
                        break;
                    case "type":
                        move.PokeType = value;
                        break;
                    case "accuracy":
                        move.Accuracy = int.Parse(value);
                        break;
                    case "pp":
                        moveDetails.PP = int.Parse(value);
                        break;
                    case "secondaryEffectChance":
                        moveDetails.SecondaryEffectChance = int.Parse(value);
                        break;
                    case "target":
                        moveDetails.Target = value;
                        break;
                    case "priority":
                        moveDetails.Priority = int.Parse(value);
                        break;
                    case "flags":
                        moveDetails.Flags = value.Replace(" | ", ", "); // Combine flags into a single string
                        break;
                    case "split":
                        moveDetails.Split = value;
                        break;
                }
            }

            return move;
        }

        public void formatList()
        {
            string filePath = "DatabaseBuilder/GameData2/MoveSource2.txt";

            string content = File.ReadAllText(filePath);

            string pattern = @"#(if|ifdef)\s+[^\n]+\n(?<keep>[\s\S]*?)\n\s*#else[\s\S]*?#endif";
            content = Regex.Replace(content, pattern, match => match.Groups["keep"].Value.Trim());

            pattern = @"#(if|ifdef)\s+[^\n]+\n(?<keep>[\s\S]*?)\n\s*#endif";
            content = Regex.Replace(content, pattern, match => match.Groups["keep"].Value.Trim());

            File.WriteAllText(filePath, content);
            Console.WriteLine("File processed successfully.");

        }
    }
}