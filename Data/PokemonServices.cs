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
        
        public async Task AddTypeAsync(List<AttackType> attackTypes)
        {
            foreach (var typeToAdd in attackTypes)
            {
                DefenceType defenceType = new DefenceType
                {
                    DefenceTypeName = typeToAdd.AttackTypeName,
                };
                Console.WriteLine(defenceType.DefenceTypeName);
                _context.DefenceTypes.Add(defenceType);
                _context.AttackTypes.Add(typeToAdd);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<AttackType>> GetTypeList()
        {
            return await _context.AttackTypes.ToListAsync();
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

        public async Task<int> MatchTypeToId(string name)
        {
            var type = await _context.AttackTypes.FindAsync(name);
            return type.AttackTypeId;
        }
    }
}
