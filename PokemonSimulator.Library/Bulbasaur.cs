// Ignore Spelling: Bulbasaur
using UI;

namespace PokemonSimulator.Library
{
    public class Bulbasaur(List<Attack> attacks) :
        WaterPokemon("Bulbasaur", attacks), IEvolvable
    {
        public override void Evolve()
        {
            Level += 10;
            ConsoleUI.WriteLine($"\n{Name} is evolving...");
            // ToDo: Change name
            Name = "Bulbasaur";
            ConsoleUI.WriteLine($"Now it is a {Name} and its level is {Level}");
        }
    }
}
