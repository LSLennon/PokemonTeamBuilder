﻿using PokemonTeamBuilder.Components.Classes.DatabaseClasses;

namespace PokemonTeamBuilder.Components.Classes
{
    public class AppMove
    {
        public int AppMoveId { get; set; }
        public string MoveName { get; set; }
        public int Accuracy { get; set; }
        public AttackType MoveType { get; set; }
        public string PP { get; set; }
        public string Power { get; set; }
        public string DamgeClass { get; set; }
        public string FlavourText { get; set; }
    }
}