using Microsoft.EntityFrameworkCore;
using PokemonTeamBuilder.Components.Classes.ManyToMany.BasePokemon;
using PokemonTeamBuilder.Components.Classes.ManyToMany.FavouriteUser;
using PokemonTeamBuilder.Components.Classes.ManyToMany.PokemonCustom;
using PokemonTeamBuilder.Components.Classes.PokemonData;
using PokemonTeamBuilder.Components.Classes.UsersData;

namespace PokemonTeamBuilder.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<CustomPokemon> CustomPokemons { get; set; }
        public DbSet<PokeAbility> PokeAbilities { get; set; }
        public DbSet<PokeMethod> PokeMethods { get; set; }
        public DbSet<PokeMove> PokeMoves { get; set; }
        public DbSet<PokeStats> PokeStats { get; set; }
        public DbSet<PokeType> PokeTypes { get; set; }
        public DbSet<TypeEffectiveness> TypeEffectivenesses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }
        public DbSet<MPokemonToMoves> MPokemonToMoves { get; set; }
        public DbSet<MPokemonToAbilities> MPokemonToAbilities { get; set; }
        public DbSet<MPokemonToTypes> MPokemonToTypes { get; set; }
        public DbSet<MCustomToMoves> MCustomToMoves { get; set; }
        public DbSet<PokeNature> PokeNatures { get; set; }
        public DbSet<UserFavourites> UserFavourites { get; set; }
        public DbSet<HeldItem> HeldItems { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Data/PokemonDB.db", o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.BaseStats)
                .WithOne()
                .HasForeignKey<Pokemon>(ps => ps.BaseStatsId);

            modelBuilder.Entity<TypeEffectiveness>()
                .HasKey(te => te.TypeEffectivnessId);

            modelBuilder.Entity<TypeEffectiveness>()
                .HasOne(te => te.AttackType)
                .WithMany(pt => pt.AttackEffectiveness)
                .HasForeignKey(te => te.AttackTypeId);

            modelBuilder.Entity<TypeEffectiveness>()
                .HasOne(te => te.DefenceType)
                .WithMany(pt => pt.DefenceEffectiveness)
                .HasForeignKey(te => te.DefenceTypeId);

            modelBuilder.Entity<CustomPokemon>()
                .HasOne(cp => cp.CustomPokemonEVs)
                .WithOne()
                .HasForeignKey<CustomPokemon>(cp => cp.CustomPokemonEVsId);

            modelBuilder.Entity<CustomPokemon>()
                .HasOne(cp => cp.CustomPokemonIVs)
                .WithOne()
                .HasForeignKey<CustomPokemon>(cp => cp.CustomPokemonIVsId);
        }



    }
}