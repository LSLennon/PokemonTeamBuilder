using PokemonTeamBuilder.Components.Classes.UsersData;

namespace PokemonTeamBuilder.Components.Classes.Trainers
{
    public class Trainer
    {
        public int TrainerId { get; set; }
        public string TrainerName { get; set; }

        public ICollection<CustomPokemon> CustomPokemons { get; set; } = new List<CustomPokemon>();
    }
}
