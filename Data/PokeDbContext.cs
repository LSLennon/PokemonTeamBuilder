using InclamentEmeraldTeamBuilder.Components.Classes;
using InclamentEmeraldTeamBuilder.Components.Classes.MoveInfo;
using InclementEmeraldTeamBuilder.Components.Classes;
using Microsoft.EntityFrameworkCore;

namespace InclementEmeraldTeamBuilder.Data
{
    public class PokeDbContext : DbContext
    {
        public PokeDbContext(DbContextOptions<PokeDbContext> options)
            : base(options)
        { }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<PokeType> PokeTypes { get; set; }

        //Move Information
        public DbSet<Move> Moves { get; set; }
        public DbSet<MoveEffect> MoveEffects { get; set; }
        public DbSet<MoveFlag> MoveFlags { get; set; }
        public DbSet<MoveStatus> MoveStatuses { get; set; }
        public DbSet<MoveTarget> MoveTargets { get; set; }
        public DbSet<MMoveToMoveFlag> MMoveToMoveFlags { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Data/PokemonDB.db");
        }
    }
}



