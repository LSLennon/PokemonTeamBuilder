using PokemonTeamBuilder.Components.Classes.UsersData;

namespace PokemonTeamBuilder.Components.Classes.Trainers
{
    // Unused but would be included to allow users to create teams to comapre thiers too
    public class Trainer
    {
        public int TrainerId { get; set; }
        public string TrainerName { get; set; }

        public ICollection<CustomPokemon> CustomPokemons { get; set; } = new List<CustomPokemon>();
    }
}
