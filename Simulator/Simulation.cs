
using PokemonSimulator.Library;

namespace Simulator
{
    internal class Simulation
    {
        internal void Start()
        {
            Attack fireFang = new("Fire Fang", ElementType.Fire, 20);
            Attack heatTackle = new("Heat Tackle", ElementType.Fire, 30);
            Attack ember = new("Ember", ElementType.Fire, 40);
            Attack waterGun = new("Water Gun", ElementType.Water, 20);
            // ToDo: Create List<Attack> attacks containing at least 2 of each ElementType

            List<Attack> attacks = [
                fireFang,
                heatTackle,
                ember,
                waterGun, //<- Should throw type error when added to Charmander?
            ];

            Charmander charmander = new(attacks);
            // ToDo: Create List<Pokemon> pokemonList
            /* ToDo: for each pokemon in pokemonList
             *          call pokemon.RaiseLevel()
             *          call pokemon.Attack()
             *          
             *          if a pokemon implements IEvolvable
             *              call pokemon.Evolve
             */
            // ToDo: Catch and handle any errors
            Console.WriteLine("Welcome to the Pokemon Simulator!");
            Console.WriteLine("");
            Console.WriteLine("Here are your Pokemon:");
            Console.WriteLine(charmander);
            Console.WriteLine("");
            charmander.RandomAttack();
            Console.WriteLine("Press any key to exit the program");
            Console.ReadKey();
        }
    }
}