namespace PokemonTeamBuilder.Components.Classes.Decorator
{
    public class DecoratorCustomPokemon : AbstractCustomPokemon
    {
        protected AbstractCustomPokemon _customPoke;


        public DecoratorCustomPokemon(AbstractCustomPokemon customPoke)
        {
            _customPoke = customPoke;
        }
    }
}
