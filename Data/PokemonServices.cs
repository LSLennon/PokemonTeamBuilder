using PokemonTeamBuilder.Components.Classes;
using PokemonTeamBuilder.Components.Classes.BasePokemonSubClasses;

namespace PokemonTeamBuilder.Data
{
    public class PokemonServices
    {
        private readonly ApplicationDbContext _context;

        public PokemonServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTypeAsync(List<AppPokemonType> type)
        {
            foreach (var typeItem in type)
            {
                _context.appPokemonTypes.Add(typeItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
