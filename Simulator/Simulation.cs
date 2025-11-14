
using PokemonSimulator.Library;
using Simulator.UI;
using System.Collections.Generic;

namespace Simulator
{
    internal class Simulation
    {
        private Menu<Pokemon> _simulationMenu;

        public Simulation()
        {
            // Set up menu UI
            void renderSimulationIntroUI()
            {
                ConsoleUI.WriteLine("Welcome to the Pokemon Simulator!");
                ConsoleUI.WriteLine("");
            }

            // Generate Pokemon List and create the root menu
            var pokemonList = GeneratePokemon(3);
            MenuList<Pokemon, MenuListItem<Pokemon>> pokemonMenuList = [];
            for (var i = 0; i < pokemonList.Count; i++) 
            {
                var pokemon = pokemonList[i];
                void renderPokemonMenuListItem()
                {
                    void renderPokemonInfoMenuIntroUI()
                    {
                        ConsoleUI.WriteLine($"Showing details for {pokemon.Name}");
                        ConsoleUI.WriteLine($"\nWhat would you like to do with {pokemon.Name}?\n");
                    }
                    var attackNames = pokemon.Attacks.Select(attack => attack.Name);
                    void renderShowAttacksUI()
                    {
                        foreach (var attack in pokemon.Attacks)
                        {
                            ConsoleUI.Write($"\n\t{pokemon.Name} knows ");
                            ConsoleUI.ForegroundColor = attack.ElementColor;
                            ConsoleUI.Write($"{attack}");
                            ConsoleUI.ResetColor();
                        }
                    }
                    var pokemonInfoMenuList = new MenuList<string, MenuListItem<string>>
                {
                    
                    new MenuListItem<string>(1, "Show Attacks", renderShowAttacksUI),
                    new MenuListItem<string>(2, "Evolve"),
                    new MenuListItem<string>(3, "Go back to Pokemon list")
                };
                    var pokemonInfoMenu = new Menu<string>($"{pokemon.Name} info", pokemonInfoMenuList, renderPokemonInfoMenuIntroUI);

                    pokemonInfoMenu.Display();
                    ConsoleUI.ReadKey(intercept: true);
                }
                pokemonMenuList.Add(new MenuListItem<Pokemon>(i + 1, pokemonList[i], renderPokemonMenuListItem));
            }
            _simulationMenu = new Menu<Pokemon>("Pokemon Simulator", pokemonMenuList, renderSimulationIntroUI);
        }

        internal void Start()
        {
            // Render the root menu
            _simulationMenu.Display();
        }

        //internal void _Start()
        //{


        //    do
        //    {
        //        Console.Clear();

        //        /* ToDo: for each pokemon in pokemonList
        //         *          call pokemon.RaiseLevel()
        //         *          call pokemon.Attack()
        //         *          
        //         *          if a pokemon implements IEvolvable
        //         *              call pokemon.Evolve
        //         */
        //        // ToDo: Catch and handle any errors
        //        Console.WriteLine("Welcome to the Pokemon Simulator!");
        //        Console.WriteLine("");
        //        GeneratePokemonMenuList(1);
        //        List < Pokemon > = GeneratePokemon(1);

        //        try
        //        {
        //            //DisplayPokemonList(PokemonMenuList);
        //            _pokemonMenuList.Display();
        //            Console.WriteLine();
        //        }
        //        catch (Exception ex)
        //        {
        //            SimulationException = ex;
        //            MenuSelectionIndex = null;
        //        }

        //    } while (MenuSelectionIndex == null);
        //    Console.WriteLine("\nPress any key to exit the simulator");
        //    Console.ReadKey(intercept: true);
        //}



        //private void DisplayPokemonList(MenuList<MenuListItem<Pokemon>, Pokemon> pokemonList)
        //{
        //    Console.Clear();
        //    Console.WriteLine("\nSelect a Pokemon from the menu");
        //    Console.WriteLine("Here are your Pokemon:\n");
        //    foreach (var listItem in _pokemonMenuList)
        //    {
        //        Console.WriteLine(listItem);
        //    }

        //    if (SimulationException != null)
        //    {
        //        DisplaySimulationException(SimulationException.Message);
        //    }

        //    MenuSelectionIndex = GetSelectionIndexFromMenuList(_pokemonMenuList);

        //    SimulationException = null;

        //    var selectedPokemon = _pokemonMenuList[(MenuSelectionIndex ?? 1) - 1].Name;
        //    if (selectedPokemon == null)
        //    {
        //        MenuSelectionIndex = null;
        //        throw new Exception($"Could not find Pokemon at index {MenuSelectionIndex}");
        //    }

        //    DisplayPokemonInfo(selectedPokemon);
        //}

        //private void DisplayPokemonInfo(Pokemon pokemon)
        //{
        //    int previousMenuSelectionIndex = MenuSelectionIndex ?? 0;
        //    do
        //    {
        //        Console.Clear();
        //        Console.WriteLine($"\n{pokemon}");
        //Console.WriteLine($"\nWhat would you like to do with {pokemon.Name}?");
        //        Console.WriteLine();

        //        List<MenuListItem<string>> pokemonInfoMenu = [];
        //        pokemonInfoMenu.Add(new MenuListItem<string>(1, "Show Attacks"));
        //        pokemonInfoMenu.Add(new MenuListItem<string>(2, "Evolve"));
        //        pokemonInfoMenu.Add(new MenuListItem<string>(3, "Go back to Pokemon list"));

        //        foreach (var item in pokemonInfoMenu)
        //        {
        //            Console.WriteLine($"\n\t{item}");
        //        }

        //        try
        //        {
        //            MenuSelectionIndex = GetSelectionIndexFromMenuList(pokemonInfoMenu);

        //            switch (MenuSelectionIndex)
        //            {
        //                case 0:
        //                    Console.WriteLine("Exiting the Simulator...");
        //                    Environment.Exit(0);
        //                    break;
        //                case 1:
        //                    DisplayAttackInfo(pokemon);
        //                    Console.WriteLine($"\nPress any key to return to continue");
        //                    Console.ReadKey(intercept: true);
        //                    MenuSelectionIndex = previousMenuSelectionIndex;
        //                    //DisplayPokemonList(PokemonList);
        //                    break;
        //                case 2:
        //                    pokemon.Evolve();
        //                    Console.WriteLine($"\nPress any key to return to continue");
        //                    Console.ReadKey(intercept: true);
        //                    MenuSelectionIndex = previousMenuSelectionIndex;
        //                    break;
        //                case 3:
        //                    MenuSelectionIndex = null;
        //                    DisplayPokemonList(_pokemonMenuList);
        //                    break;
        //                default:
        //                    MenuSelectionIndex = previousMenuSelectionIndex;
        //                    throw new NotImplementedException();
        //                    break;
        //            }
        //        }
        //        catch (Exception ex)

        //        {
        //            SimulationException = ex;
        //            MenuSelectionIndex = previousMenuSelectionIndex;
        //        }


        //    } while (MenuSelectionIndex == previousMenuSelectionIndex);

        //    //MenuSelectionIndex = GetSelectionIndexFromMenu(pokemonInfoMenu);
        //}

        private static void DisplayAttackInfo(Pokemon pokemon)
        {
            foreach (Attack attack in pokemon.Attacks)
            {
                Console.Write($"\n\t{pokemon.Name} knows ");
                Console.ForegroundColor = attack.ElementColor;
                Console.Write($"{attack}");
                Console.ResetColor();
            }
        }

        private static void DisplaySimulationException(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static int GetSelectionIndexFromMenuList<T>(List<MenuListItem<T>> menuList)
        {
            var keyPressed = ConsoleUI.ReadKey(intercept: true).KeyChar.ToString();
            var isIndex = int.TryParse(keyPressed, out int index);
            if (!isIndex)
                throw new Exception("Please use a number to make your selection");
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, menuList.Count);

            return index;
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

        //private void GeneratePokemonMenuList(int count)
        //{
            

        //    MenuList<MenuListItem<Pokemon>, Pokemon> menuList = new();

        //    for (int i = 1; i <= count; i++) {
        //        menuList.Add(new(i, charmander));
        //    }

        //    PokemonList = menuList;
        //}
    }
}