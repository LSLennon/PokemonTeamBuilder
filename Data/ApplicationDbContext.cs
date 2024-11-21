using Microsoft.EntityFrameworkCore;
using PokemonTeamBuilder.Components.Classes;
using PokemonTeamBuilder.Components.Classes.BasePokemonSubClasses;
using PokemonTeamBuilder.Components.Classes.PokedexDatabase;

namespace PokemonTeamBuilder.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<CustomPokemon> CustomPokemons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }
        public DbSet<AppStats> stats { get; set; }
        public DbSet<PokedexPokemon> pokedexPokemon { get; set; }
        public DbSet<AppPokemonType> appPokemonTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Data/PokemonDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany(u => u.UserTeams)
            .WithOne(ut => ut.User)
            .HasForeignKey(ut => ut.UserId);

            modelBuilder.Entity<UserTeam>()
            .HasMany(ut => ut.CustomPokemons)
            .WithOne(cp => cp.UserTeam)
            .HasForeignKey(cp => cp.UserTeamId);

            modelBuilder.Entity<CustomPokemon>()
            .HasOne(cp => cp.CustomPokemonEVs)
            .WithOne()
            .HasForeignKey<CustomPokemon>(cp => cp.CustomPokemonEVsId);

            modelBuilder.Entity<CustomPokemon>()
                .HasOne(cp => cp.CustomPokemonIVs)
                .WithOne()
                .HasForeignKey<CustomPokemon>(cp => cp.CustomPokemonIVsId);

            modelBuilder.Entity<PokedexPokemon>()
                .HasOne(bpa => bpa.PokemonType1)
                .WithOne()
                .HasForeignKey<PokedexPokemon>(bpa => bpa.PokemonType1Id);

            modelBuilder.Entity<PokedexPokemon>()
                .HasOne(bpa => bpa.PokemonType2)
                .WithOne()
                .HasForeignKey<PokedexPokemon>(bpa => bpa.PokemonType2Id);

            modelBuilder.Entity<AppPokemonType>()
                .HasKey(nc => nc.AppPokemonTypeId);
        }
    }
}