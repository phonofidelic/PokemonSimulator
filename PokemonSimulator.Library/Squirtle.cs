// Ignore Spelling: Squirtle

using UI;

namespace PokemonSimulator.Library
{
    public class Squirtle(List<Attack> attacks) :
        WaterPokemon("Squirtle", attacks), IEvolvable
    {
        //public override void Evolve()
        //{
        //    Level += 10;
        //    ConsoleUI.WriteLine($"\n{Name} is evolving...");
        //    // ToDo: Change name
        //    Name = "Squirtle";
        //    Console.WriteLine($"Now it is a {Name} and its level is {Level}");
        //}
        protected override void _Evolve()
        {
            ConsoleUI.WriteLine($"{Name} has reached its final stage of evolution!");
        }
    }
}