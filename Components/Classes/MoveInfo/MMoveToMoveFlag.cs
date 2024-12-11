using InclementEmeraldTeamBuilder.Components.Classes;

namespace InclamentEmeraldTeamBuilder.Components.Classes.MoveInfo
{
    public class MMoveToMoveFlag
    {
        public int MMoveToMoveFlagId { get; set; }
        public int MoveId { get; set; }
        public Move Move { get; set; }
        public int MoveFlagId { get; set; }
        public MoveFlag MoveFlag { get; set; }
    }
}
