using InclamentEmeraldTeamBuilder.Components.Classes;
using InclamentEmeraldTeamBuilder.Components.Classes.MoveInfo;

namespace InclementEmeraldTeamBuilder.Components.Classes
{
    public class Move
    {
        public int MoveId { get; set; }
        public string MoveName { get; set; }
        public string MoveSourceName { get; set; }
        public string MoveDescription { get; set; }
        public MoveEffect MoveEffect { get; set; }
        public int Power { get; set; }
        public PokeType PokeType { get; set; }
        public int Accuracy { get; set; }
        public int PP { get; set; }
        public int SecondaryEffectChance { get; set; }
        public MoveTarget MoveTarget { get; set; }
        public int Priority { get; set; }
        public List<MoveFlag> MoveFlags { get; set; } = new List<MoveFlag>();
        public MoveStatus MoveStatus { get; set; }
    }
}
