// Ignore Spelling: Squirtle
using UI;

namespace PokemonSimulator.Library
{
    public class Squirtle(List<Attack> attacks) :
        WaterPokemon("Squirtle", attacks), IEvolvable
    {
        protected override void _Evolve()
        {
            ConsoleUI.WriteLine($"{Name} has reached its final stage of evolution!");
        }
    }
}