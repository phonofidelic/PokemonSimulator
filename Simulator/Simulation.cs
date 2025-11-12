
using PokemonSimulator.Library;
using System.Collections.Generic;

namespace Simulator
{
    internal class Simulation
    {
        private List<MenuListItem<Pokemon>> PokemonList = [];
        private MenuListItem<Pokemon>? MenuSelection = null;

        private Exception? SimulationException = null;

        internal void Start()
        {
            do
            {
                Console.Clear();

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
                GeneratePokemonMenuList(1);
                Console.WriteLine("\nSelect a Pokemon from the menu");
                Console.WriteLine("Here are your Pokemon:\n");

                foreach (MenuListItem<Pokemon> listItem in PokemonList)
                {
                    Console.WriteLine($"\t{listItem.Index}. {listItem.Value.Name}");
                }

                if (SimulationException != null)
                {
                    DisplaySimulationException(SimulationException.Message);
                }

                try
                {
                    MenuSelection = GetSelectionFromMenu(PokemonList);
                    Console.WriteLine($"\n\t{MenuSelection.Value}");
                } catch (Exception ex)
                {
                    SimulationException = ex;
                    
                }
                
            } while (MenuSelection == null);
            Console.WriteLine("\nPress any key to exit the simulator");
            Console.ReadKey(intercept: true);
        }

        private void DisplaySimulationException(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private MenuListItem<T> GetSelectionFromMenu<T>(List<MenuListItem<T>> menuList)
        {
            var keyPressed = Console.ReadKey(intercept: true).KeyChar.ToString();
            var isIndex = int.TryParse(keyPressed, out int index);
            if (!isIndex)
                throw new Exception("Please use a number to make your selection");
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, menuList.Count);

            return menuList[index - 1];
        }

        private void GeneratePokemonMenuList(int count)
        {
            Console.WriteLine($"Generating {count} Pokemon...");
            Attack fireFang = new("Fire Fang", ElementType.Fire, 20);
            Attack heatTackle = new("Heat Tackle", ElementType.Fire, 30);
            Attack ember = new("Ember", ElementType.Fire, 40);
            Attack waterGun = new("Water Gun", ElementType.Water, 20);
            List<Attack> attacks = [
                fireFang,
                heatTackle,
                ember,
                waterGun, //<- Should throw type error when added to Charmander?
            ];
            Charmander charmander = new(attacks);

            List<MenuListItem<Pokemon>> menuList = new();

            for (int i = 1; i <= count; i++) {
                menuList.Add(new(i, charmander));
            }

            PokemonList = menuList;
        }
    }
}