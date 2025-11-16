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

            List<Pokemon> pokemonList =
            [
                new Charmander(fireAttacks),
                new Squirtle(waterAttacks),
                new Bulbasaur(grassAttacks),
            ];

            PokemonList = pokemonList;

            DisplayPokemonList(PokemonList);
        }

        private void DisplayPokemonList(List<Pokemon> pokemonList)
        {
            do
            {
                ConsoleUI.Clear();
                ConsoleUI.WriteLine("Welcome to the Pokemon Simulator!");
                ConsoleUI.WriteLine($"\nYou have {pokemonList.Count} Pokemon:\n");
                try
                {
                    for (var i = 0; i < pokemonList.Count; i++)
                    {
                        var pokemon = pokemonList[i];
                        ConsoleUI.WriteLine($"\t{i + 1}. {pokemon}");
                    }

                    if (SimulationException != null)
                    {
                        ConsoleUI.WriteLine();
                        DisplayMenuException(SimulationException.Message);
                    }
                    SimulationException = null;

                    // GetMenuSelectionFromKeyPress reads the Escape key as 0
                    ConsoleUI.WriteInfo($"\n(Esc.) to exit the Simulator.");
                    int pokemonIndex = GetListSelectionFromKeyPress(pokemonList);
                    if (pokemonIndex == 0)
                    {
                        ConsoleUI.Clear();
                        ConsoleUI.WriteLine("\n\tAre you sure you want to quit?");
                        ConsoleUI.WriteLine("\n\t\tY (yes)\tN (no)");
                        var answer = ConsoleUI.ReadKey(intercept: true).Key;
                        while (answer != ConsoleKey.Y && answer != ConsoleKey.N)
                        {
                            ConsoleUI.Clear();
                            ConsoleUI.WriteLine("\n\tAre you sure you want to quit?");
                            ConsoleUI.WriteWarning("\n\t\tY (yes) to quit\n\n\t\tN (no) to Cancel");
                            answer = ConsoleUI.ReadKey(intercept: true).Key;
                        }

                        if (answer == ConsoleKey.Y)
                        {
                            ConsoleUI.WriteLine("Exiting the program...");
                            Environment.Exit(0);
                        }
                    }

                    if (pokemonIndex > 0)
                        SelectedPokemon =  PokemonList[pokemonIndex - 1];

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
                ConsoleUI.Write($"{pokemon.CurrentEvolution.Name} is a level {pokemon.CurrentEvolution.Level} ");
                // ToDo: Create Element class
                // ConsoleUI.ForegroundColor = pokemon.Element.Color.
                ConsoleUI.Write($"{pokemon.Type}");
                ConsoleUI.Write($" Pokemon.");
                
                ConsoleUI.WriteLine($"\nWhat would you like to do with {pokemon.CurrentEvolution.Name}?");
                ConsoleUI.WriteLine("");

                if (SimulationException != null)
                {
                    DisplayMenuException(SimulationException.Message);
                }
                SimulationException = null;

                List<string> menu = [
                    $"\t1. Show Attacks",
                    $"\t2. Evolve",
                    $"\t3. Select Attack",
                    $"\t4. Random Attack",
                ];

                foreach (var item in menu)
                {
                    ConsoleUI.WriteLine(item);
                }
                ConsoleUI.WriteInfo($"\n(Esc.) to return to the Pokemon list.");

                try
                {
                    SelectedMenuIndex = GetListSelectionFromKeyPress(menu);

                    switch (SelectedMenuIndex)
                    {
                        case 0:
                            SelectedMenuIndex = null;
                            DisplayPokemonList(PokemonList);
                            break;
                        case 1:
                            ConsoleUI.Clear();
                            DisplayAttacks(pokemon);
                            break;
                        case 2:
                            ConsoleUI.Clear();
                            pokemon.CurrentEvolution.Evolve();
                            break;
                        case 3:
                            // Select Attack
                            ConsoleUI.Clear();
                            SelectAttack(pokemon);
                            DisplayPokemonInfo(pokemon);
                            break;
                        case 4:
                            // Random Attack
                            ConsoleUI.Clear();
                            ConsoleUI.WriteLine("\n\n");
                            pokemon.RandomAttack();
                            break;
                        default:
                            SelectedMenuIndex = previousSelectedCommand;
                            throw new NotImplementedException();
                    }
                    ConsoleUI.WriteInfo($"\n\nPress any key to continue");
                    ConsoleUI.ReadKey(intercept: true);
                    SelectedMenuIndex = previousSelectedCommand;
                }
                catch (Exception ex)
                {
                    SimulationException = ex;
                    SelectedMenuIndex = previousSelectedCommand;
                }
            } while (SelectedMenuIndex == previousSelectedCommand);
        }

        private void SelectAttack(Pokemon pokemon)
        {
            bool goBack = false;
            int selectedAttackMenuIndex;
            Exception? localException = null;

            do
            {
                try {
                    ConsoleUI.Clear();
                    Console.WriteLine($"Which attack would you like {pokemon.CurrentEvolution.Name} to use?\n\n");
                    for (int i = 0; i < pokemon.Attacks.Count; i++)
                    {
                        Attack attack = pokemon.Attacks[i];
                        Console.Write($"\n\t{i + 1}. ");
                        DisplayAttackInfo(attack);
                    }
                    ConsoleUI.WriteInfo("\n\nSelect an Attack from the list. (Esc) to go back");
                    
                    if (localException != null)
                    {
                        DisplayMenuException($"\n{localException.Message}"); 
                    }
                    
                    selectedAttackMenuIndex = GetListSelectionFromKeyPress(pokemon.Attacks);
                    
                    if (selectedAttackMenuIndex == 0)
                    {
                        goBack = true;
                        break;
                    }
                    
                    if (selectedAttackMenuIndex > 0)
                    {
                        ConsoleUI.Clear();
                        ConsoleUI.WriteLine("\n\n");
                        pokemon.Attack(selectedAttackMenuIndex - 1);
                    }

                    localException = null;
                    ConsoleUI.WriteInfo($"\n\n\tPress any key to continue");
                    ConsoleUI.ReadKey(intercept: true);
                } catch (Exception ex)
                {
                    localException = ex;
                }
            } while (!goBack);
        }

        private static void DisplayAttacks(Pokemon pokemon)
        {
            ConsoleUI.WriteLine($"{pokemon.CurrentEvolution.Name} knows {pokemon.Attacks.Count} attacks:");
            foreach ((Attack attack, int index) in pokemon.Attacks.Select((Attack, index) => (Attack, index)))
            {
                Console.Write("\n\t");
                DisplayAttackInfo(attack);
            }
        }

        private static void DisplayAttackInfo(Attack attack)
        {
            ConsoleUI.Write($"Level {attack.BasePower} ");
            ConsoleUI.ForegroundColor = attack.ElementColor;
            ConsoleUI.Write($"{attack}");
            ConsoleUI.ResetColor();
            ConsoleUI.Write(" attack.");
        }

        private static void DisplayMenuException(string menuExceptionMessage)
        {
            ConsoleUI.WriteError(menuExceptionMessage);
        }

        private static int GetListSelectionFromKeyPress<T>(List<T> list)
        {
            var keyInfo = ConsoleUI.ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.Escape)
                return 0;
                
            var keyPressed = keyInfo.KeyChar.ToString();
            var isNumber = int.TryParse(keyPressed, out int key);
            if (!isNumber)
                throw new Exception("Please use a number to make your selection");
            int selection = key;
            ArgumentOutOfRangeException.ThrowIfGreaterThan(selection, list.Count);

            return selection;
        }
    }
}