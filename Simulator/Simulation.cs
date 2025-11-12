
using PokemonSimulator.Library;

namespace Simulator
{
    internal class Simulation
    {
        private List<MenuListItem<Pokemon>> PokemonList = [];
        private MenuListItem<Pokemon>? MenuSelection = null;

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
                Console.WriteLine("Select a Pokemon from the menu");
                Console.WriteLine("Here are your Pokemon:");

                foreach (MenuListItem<Pokemon> listItem in PokemonList)
                {
                    Console.WriteLine($"{listItem.Index}. {listItem.Value.Name}");
                }

                Console.WriteLine("");
                //charmander.RandomAttack();
                //charmander.Evolve();

                MenuSelection = GetSelectionFromMenu(PokemonList);
                Console.WriteLine($"\n\t{MenuSelection.Value}");
            } while (MenuSelection == null);
            Console.WriteLine("\nPress any key to exit the simulator");
            Console.ReadKey(intercept: true);
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

            for (int i = 1; i <= count; i++) {
                PokemonList.Add(new(i, charmander));
            }
        }
    }
}