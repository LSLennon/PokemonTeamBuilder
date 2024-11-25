using Microsoft.EntityFrameworkCore;
using PokemonTeamBuilder.Components.Classes.DatabaseClasses;

namespace PokemonTeamBuilder.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<PokeStats> stats { get; set; }
        public DbSet<PokeType> PokeTypes { get; set; }
        public DbSet<CustomPokemon> CustomPokemons { get; set; }
        public DbSet<PokedexPokemon> PokedexPokemons { get; set; }
        public DbSet<TypeEffectiveness> EffectivenessTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }
        public DbSet<PokeMove> PokeMoves { get; set; }
        public DbSet<PokeMethod> PokeMethods { get; set; }
        public DbSet<MovesLearnedByPokemon> MovesLearnedByPokemons { get; set; }

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
                .WithMany(at => at.AttackEffectiveness)
                .HasForeignKey(te => te.AttackTypeId);

            modelBuilder.Entity<TypeEffectiveness>()
                .HasOne(te => te.DefenceType)
                .WithMany(dt => dt.DefenceEffectiveness)
                .HasForeignKey(te => te.DefenceTypeId);

            modelBuilder.Entity<MovesLearnedByPokemon>()
            .HasKey(ml => new { ml.PokeMoveId, ml.PokedexPokemonId });

            // Configure relationships
            modelBuilder.Entity<MovesLearnedByPokemon>()
                .HasOne(ml => ml.PokeMove)
                .WithMany(pm => pm.MovesLearnedByPokemons)
                .HasForeignKey(ml => ml.PokeMoveId);

            modelBuilder.Entity<MovesLearnedByPokemon>()
                .HasOne(ml => ml.PokedexPokemon)
                .WithMany(pp => pp.Moves)
                .HasForeignKey(ml => ml.PokedexPokemonId);
        }

    }
}