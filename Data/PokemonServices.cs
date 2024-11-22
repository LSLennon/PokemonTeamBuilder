using Microsoft.EntityFrameworkCore;
using PokemonTeamBuilder.Components.Classes.DatabaseClasses;

namespace PokemonTeamBuilder.Data
{
    public class PokemonServices
    {
        private readonly ApplicationDbContext _context;

        public PokemonServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTypeAsync(List<PokeType> pokeTypes)
        {
            foreach (var typeToAdd in pokeTypes)
            {
                _context.PokeTypes.Add(typeToAdd);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<PokeType>> GetTypeList()
        {
            return await _context.PokeTypes.ToListAsync();
        }

        public async Task<List<PokedexPokemon>> GetPokemonList()
        {
            return await _context.PokedexPokemons
                .Select(p => new PokedexPokemon
                {
                    PokedexPokemonId = p.PokedexPokemonId,
                    PokemonName = p.PokemonName,
                    DefenceType1 = p.DefenceType1,
                    DefenceType2 = p.DefenceType2 ?? null,
                    Sprite = p.Sprite
                })
                .ToListAsync();
        }

        public async Task<PokeType> GetTypeByName(string name)
        {
            return _context.PokeTypes.FirstOrDefault(at => at.PokeTypeName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task AddTypeEffectivness(TypeEffectiveness effectiveness)
        {
            _context.EffectivenessTypes.Add(effectiveness);
            await _context.SaveChangesAsync();
        }

        public async Task AddPokemon(PokedexPokemon pokemon)
        {
            _context.PokedexPokemons.Add(pokemon);
            await _context.SaveChangesAsync();
        }
    }
}
