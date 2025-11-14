using PokemonSimulator.Library;
using UI;

namespace Simulator
{
    internal class DynamicSimulation : Simulation
    {
        private List<Pokemon> Pokemons = [];
        internal override void Start()
        {
            var mainMenu = new MenuSystem();
        }

        private static List<Pokemon> GeneratePokemon(int count)
        {
            ConsoleUI.WriteLine($"Generating {count} Pokemon...");
            Attack fireFang = new("Fire Fang", ElementType.Fire, 20);
            Attack heatTackle = new("Heat Tackle", ElementType.Fire, 30);
            Attack ember = new("Ember", ElementType.Fire, 40);
            Attack waterGun = new("Water Gun", ElementType.Water, 20);
            Attack vineWhip = new("Vine Whip", ElementType.Grass, 50);
            List<Attack> attacks = [
                fireFang,
                heatTackle,
                ember,
                waterGun, //<- Should throw type error when added to Charmander?
                vineWhip, //<- Should throw type error when added to Charmander?
            ];
            List<Pokemon> result = new List<Pokemon>();
            for (int i = 0; i < count; i++)
            {
                result.Add(new Charmander(attacks));
            }
            return result;
        }
    }
}