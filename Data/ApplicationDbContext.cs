using Microsoft.EntityFrameworkCore;
using PokemonTeamBuilder.Components.Classes.DatabaseClasses;

namespace PokemonTeamBuilder.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<AppStats> stats { get; set; }
        public DbSet<AttackType> AttackTypes { get; set; }
        public DbSet<CustomPokemon> CustomPokemons { get; set; }
        public DbSet<DefenceType> DefenceTypes { get; set; }
        public DbSet<PokedexPokemon> PokedexPokemons { get; set; }
        public DbSet<TypeEffectiveness> EffectivenessTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Data/PokemonDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TypeEffectiveness>()
           .HasKey(te => te.TypeEffectivnessId);

            modelBuilder.Entity<TypeEffectiveness>()
                .HasOne(te => te.AttackType)
                .WithMany(at => at.Effectiveness)
                .HasForeignKey(te => te.AttackTypeId);

            modelBuilder.Entity<TypeEffectiveness>()
                .HasOne(te => te.DefenceType)
                .WithMany(dt => dt.Effectiveness)
                .HasForeignKey(te => te.DefenceTypeId);
        }

    }
}