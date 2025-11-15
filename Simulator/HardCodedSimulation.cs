using PokemonSimulator.Library;
using UI;

namespace Simulator
{
    internal class HardCodedSimulation : Simulation
    {
        internal Exception? SimulationException { get; private set; } = null;
        internal int? SelectedMenuIndex { get; private set; }
        internal Pokemon? SelectedPokemon { get; private set; } = null;
        internal List<Pokemon> PokemonList { get; private set; } = [];

        internal override void Start()
        {
            Attack fireFang = new("Fire Fang", ElementType.Fire, 20);
            Attack heatTackle = new("Heat Tackle", ElementType.Fire, 30);
            Attack ember = new("Ember", ElementType.Fire, 40);
            Attack waterGun = new("Water Gun", ElementType.Water, 20);
            Attack bubble = new("Bubble", ElementType.Water, 20);
            Attack vineWhip = new("Vine Whip", ElementType.Grass, 50);
            Attack bindDown = new("Bind Down", ElementType.Grass, 10);
            Attack leechSeed= new("Leech Seed", ElementType.Grass, 20);
            Attack razorLeaf= new("Razor Leaf", ElementType.Grass, 30);

            List<Attack> fireAttacks = [
                fireFang,
                heatTackle,
                ember,
                //waterGun, //<- Should throw type error when added to Charmander?
                //vineWhip, //<- Should throw type error when added to Charmander?
            ];
            List<Attack> waterAttacks = [
                waterGun,
                bubble,
            ];

            List<Attack> grassAttacks = [
                vineWhip,
                bindDown,
                leechSeed,
                razorLeaf,
            ];

            

            //var charmander = new Charmander(fireAttacks);

            List<Pokemon> pokemonList = new();
            pokemonList.Add(new Charmander(fireAttacks));
            pokemonList.Add(new Squirtle(waterAttacks));
            pokemonList.Add(new Bulbasaur(grassAttacks));

            PokemonList = pokemonList;

            DisplayPokemonList(PokemonList);
        }

        private void DisplayPokemonList(List<Pokemon> pokemonList)
        {
            do
            {
                ConsoleUI.Clear();
                ConsoleUI.WriteLine("Welcome to the Pokemon Simulator!");
                ConsoleUI.WriteLine($"\nYou have {pokemonList.Count} Pokemon:");
                try
                {
                    // Reserve the first menu position for the Exit command
                    List<string> pokemonMenu = [String.Empty];

                    for (var i = 0; i < pokemonList.Count; i++)
                    {
                        var pokemon = pokemonList[i];
                        pokemonMenu.Add($"\t{i + 1}. {pokemon}");
                    }

                    foreach(var item in pokemonMenu)
                    {
                        ConsoleUI.WriteLine(item);
                    }

                    if (SimulationException != null)
                    {
                        DisplayMenuException(SimulationException.Message);
                    }
                    SimulationException = null;

                    // GetMenuSelectionFromKeyPress reads the Escape key as 0
                    int pokemonIndex = GetMenuSelectionFromKeyPress(pokemonMenu);
                    if (pokemonIndex == 0)
                    {
                        ConsoleUI.WriteLine("Exiting the program...");
                        Environment.Exit(0);
                    }
                    SelectedPokemon = PokemonList[pokemonIndex - 1];

                    if (SelectedPokemon != null)
                        DisplayPokemonInfo(SelectedPokemon);
                }
                catch (Exception ex) {
                    SimulationException = ex;
                    SelectedPokemon = null;
                }

            } while (SelectedPokemon == null);
        }

        private void DisplayPokemonInfo(Pokemon pokemon)
        {
            SelectedPokemon = null;
            int? previousSelectedCommand = SelectedMenuIndex;
            do
            {
                ConsoleUI.Clear();
                ConsoleUI.WriteLine($"{pokemon.Name} is a level {pokemon.Level} {pokemon.Type} Pokemon.");
                ConsoleUI.WriteLine($"What would you like to do with {pokemon.Name}?");
                ConsoleUI.WriteLine("");

                if (SimulationException != null)
                {
                    DisplayMenuException(SimulationException.Message);
                }
                SimulationException = null;

                List<string> menu = [
                    "", // Map Keys.Escape to 0 to return to the main menu
                    $"\t1. Show Attacks",
                    $"\t2. Evolve"
                ];



                foreach (var item in menu)
                {
                    ConsoleUI.WriteLine(item);
                }

                try
                {
                    SelectedMenuIndex = GetMenuSelectionFromKeyPress(menu);

                    switch (SelectedMenuIndex)
                    {
                        case 0:
                            SelectedMenuIndex = null;
                            DisplayPokemonList(PokemonList);
                            break;
                        case 1:
                            DisplayAttackInfo(pokemon);
                            ConsoleUI.WriteLine($"\nPress any key to return to continue");
                            ConsoleUI.ReadKey(intercept: true);
                            SelectedMenuIndex = previousSelectedCommand;
                            //DisplayPokemonList(PokemonList);
                            break;
                        case 2:
                            pokemon.Evolve();
                            ConsoleUI.WriteLine($"\nPress any key to return to continue");
                            ConsoleUI.ReadKey(intercept: true);
                            SelectedMenuIndex = previousSelectedCommand;
                            break;
                        default:
                            SelectedMenuIndex = previousSelectedCommand;
                            throw new NotImplementedException();
                    }
                }
                catch (Exception ex)

                {
                    SimulationException = ex;
                    SelectedMenuIndex = previousSelectedCommand;
                }


            } while (SelectedMenuIndex == previousSelectedCommand);

            //MenuSelectionIndex = GetSelectionIndexFromMenu(pokemonInfoMenu);
        }

        private static void DisplayAttackInfo(Pokemon pokemon)
        {
            ConsoleUI.WriteLine($"{pokemon.Name} knows {pokemon.Attacks.Count} attacks:");
            foreach (Attack attack in pokemon.Attacks)
            {
                ConsoleUI.Write($"\n\t Level {attack.BasePower} ");
                ConsoleUI.ForegroundColor = attack.ElementColor;
                ConsoleUI.Write($"{attack}");
                ConsoleUI.ResetColor();
                ConsoleUI.Write(" attack.");
            }
        }

        private static void DisplayMenuException(string menuExceptionMessage)
        {
            ConsoleUI.ForegroundColor = ConsoleColor.Red;
            ConsoleUI.WriteLine(menuExceptionMessage);
            ConsoleUI.ResetColor();
        }

        private int GetMenuSelectionFromKeyPress<T>(List<T> list)
        {
            var keyInfo = ConsoleUI.ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.Escape)
                return 0;
                
            var keyPressed = keyInfo.KeyChar.ToString();
            var isNumber = int.TryParse(keyPressed, out int key);
            if (!isNumber)
                throw new Exception("Please use a number to make your selection");
            int index = key;
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, list.Count);

            return index;
        }
    }
}