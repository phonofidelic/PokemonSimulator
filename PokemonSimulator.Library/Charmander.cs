// Ignore Spelling: Charmander

namespace PokemonSimulator.Library
{
    public class Charmander(List<Attack> attacks) : 
        FirePokemon("Charmander", attacks), IEvolvable
    {
        public void Evolve()
        {
            Level += 10;
            Console.WriteLine($"\n{Name} is evolving...");
            // ToDo: Change name
            Name = "Charmeleon";
            Console.WriteLine($"Now it is a {Name} and its level is {Level}");
        }
    }
}
