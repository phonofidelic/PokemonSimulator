// Ignore Spelling: Bulbasaur
using UI;

namespace PokemonSimulator.Library
{
    public class Bulbasaur(List<Attack> attacks) :
        WaterPokemon("Bulbasaur", attacks), IEvolvable
    {
        protected override void _Evolve()
        {
            ConsoleUI.WriteLine($"{Name} has reached its final stage of evolution!");
        }
    }
}
