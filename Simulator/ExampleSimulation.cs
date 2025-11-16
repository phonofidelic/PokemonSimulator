using PokemonSimulator.Library;
using System.Collections.Generic;
using System.Collections.Immutable;
using UI;

namespace Simulator
{
    internal class ExampleSimulation : Simulation
    {
        internal Exception? SimulationException { get; private set; } = null;
        internal int? SelectedMenuIndex { get; private set; }
        internal Pokemon? SelectedPokemon { get; private set; } = null;
        internal List<Pokemon> PokemonList { get; private set; } = [];

        // Create individual attack instances that can be copied to new Pokemon instances
        private readonly IEnumerable<Attack> AllAttacks = [
            new("Fire Fang", ElementType.Fire, 20),
            new("Heat Tackle", ElementType.Fire, 30),
            new("Ember", ElementType.Fire, 40),
            new("Water Gun", ElementType.Water, 20),
            new("Bubble", ElementType.Water, 20),
            new("Vine Whip", ElementType.Grass, 50),
            new("Bind Down", ElementType.Grass, 10),
            new("Leech Seed", ElementType.Grass, 20),
            new("Razor Leaf", ElementType.Grass, 30),
        ];

        private readonly Random _random = new();

        internal override void Start()
        {
            List<Pokemon> pokemonList =
            [
                // Charmander gets 3 fire attacks
                new Charmander(GenerateElementalAttacks(ElementType.Fire, 3)),
                // Squirtle tries to get 6 water attacks, but only 2 are available
                new Squirtle(GenerateElementalAttacks(ElementType.Water, 6)),
                // Bulbasaur gets 3 random attacks
                new Bulbasaur(GenerateRandomAttacks(3)),
                // Another Charmander with 5 random attacks
                new Charmander(GenerateRandomAttacks(5)),
            ];

            List<WaterPokemon> waterPokemon = [
                new Squirtle(GenerateRandomAttacks(3)),
                //new Charmander(GenerateRandomAttacks(3)) // <- Cannot implicitly convert Charmander to WaterPokemon
            ];

            PokemonList = pokemonList;

            DisplayPokemonList(PokemonList);
        }

        private List<Attack> GenerateRandomAttacks(int count)
        {
            // Create a copy of AllAttacks
            List<Attack> attacks = [.. AllAttacks];
            int randomIndex = _random.Next(attacks.Count);
            List<Attack> result = [];

            while (result.Count < count)
            {
                if (attacks.Count == 0) break;
                var attack = attacks[randomIndex];
                result.Add(attack);
                attacks.Remove(attack);
                randomIndex = _random.Next(attacks.Count);
            }

            return result;
        }

        private List<Attack> GenerateElementalAttacks(ElementType elementType, int count)
        {
            List<Attack> result = [];
            var fireAttacksQuery = AllAttacks.Where(attack => (attack.Type == elementType)).Take(count);

            foreach (var fireAttack in fireAttacksQuery)
            {
                result.Add(fireAttack);
            }

            return result;
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
                    List<string> mainMenu = [];
                    for (var i = 0; i < pokemonList.Count; i++)
                    {
                        var pokemon = pokemonList[i];
                        mainMenu.Add($"\t{i + 1}. {pokemon}");
                    }
                    mainMenu.Add($"\n\t{pokemonList.Count + 1}. Attack with all Pokemon");
                    mainMenu.Add($"\t{pokemonList.Count + 2}. Select a group attack");

                    foreach(var item in mainMenu)
                    {
                        ConsoleUI.WriteLine(item);
                    }

                    if (SimulationException != null)
                    {
                        ConsoleUI.WriteLine();
                        DisplayMenuException(SimulationException.Message);
                    }
                    SimulationException = null;

                    // GetMenuSelectionFromKeyPress reads the Escape key as 0
                    ConsoleUI.WriteInfo($"\n(Esc.) to exit the Simulator.");
                    SelectedMenuIndex = GetListSelectionFromReadKey(mainMenu);
                    int pokemonIndex;
                    
                    // ToDo: Improve menu selection...
                    // Second last list item
                    if(SelectedMenuIndex == mainMenu.Count -1)
                    {
                        RandomGroupAttack();
                    }
                    // Last list item
                    if (SelectedMenuIndex == mainMenu.Count)
                    {
                        DisplaySelectGroupAttackMenu();
                    }

                    // Exit command
                    if (SelectedMenuIndex == 0)
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

                    pokemonIndex = (int)SelectedMenuIndex;
                    if (pokemonIndex> 0 && pokemonIndex <= PokemonList.Count)
                        SelectedPokemon =  PokemonList[pokemonIndex - 1];

                    if (SelectedPokemon != null)
                        DisplayPokemonInfo(SelectedPokemon);

                    SelectedMenuIndex = null;
                }
                catch (Exception ex) {
                    SimulationException = ex;
                    SelectedPokemon = null;
                    SelectedMenuIndex = null;
                }
            } while (SelectedMenuIndex == null);
        }

        private void RandomGroupAttack()
        {
            Console.Clear();
            ConsoleUI.WriteLine("ATTAAAAACK!!!\n\n");
            foreach (var pokemon in PokemonList)
            {
                ConsoleUI.Write($"\n\t{pokemon.CurrentEvolution.Name} used ");
                pokemon.RandomAttack();
                ConsoleUI.WriteLine();
            }
            ConsoleUI.PromptForContinue();
        }

        private void DisplaySelectGroupAttackMenu()
        {
            bool goBack = false;
            Exception? localException = null;

            HashSet<Attack> uniqueAttacks = [];
            foreach (var pokemon in PokemonList)
            {
                foreach (var attack in pokemon.Attacks)
                {
                    uniqueAttacks.Add(attack);
                }
            }

            var uniqueAttacksList = uniqueAttacks.ToList();

            int? selextionIndex = null;
            Attack? selectedAttack = null;
            do
            {
                Console.Clear();
                ConsoleUI.WriteLine("Which attack would you like your Pokemon to use?");

                foreach ((var attack, int index) in uniqueAttacksList.Select((attack, index) => (attack, index)))
                {
                    var count = PokemonList.Where(pokemon => pokemon.Attacks.Contains(attack)).Count();
                    Console.Write($"\n\t{index + 1}. ");
                    DisplayAttack(attack);
                    ConsoleUI.Write($" attack with {count} pokemon");
                }

                ConsoleUI.WriteLine();

                if (localException != null)
                {
                    DisplayMenuException($"\n{localException.Message}");
                }

                try {
                    int selectionIndex;
                    /* If there are more than 9 unique attacks, 
                     * we need to use ReadLine to get the input.
                     */
                    if (uniqueAttacksList.Count > 9)
                    {
                        ConsoleUI.WriteInfo("\n\nEnter an Attack from the list. (0) to go back");
                        selectionIndex = GetListSelectionFromReadLine(uniqueAttacksList);
                    } else
                    {
                        ConsoleUI.WriteInfo("\n\nSelect an Attack from the list. (Esc) to go back");
                        selectionIndex = GetListSelectionFromReadKey(uniqueAttacksList);
                    }

                    if (selectionIndex == 0)
                    {
                        goBack = true;
                        break;
                    }

                    selectedAttack = uniqueAttacksList[selectionIndex - 1];
                    var pokemonToAtatack = PokemonList
                        .Where(pokemon => pokemon.Attacks.Contains(selectedAttack))
                        .Select(pokemon => pokemon);

                    ConsoleUI.Clear();
                    ConsoleUI.WriteLine("\n");
                    foreach (var pokemon in pokemonToAtatack)
                    {
                        var attackIndex = pokemon.Attacks.IndexOf(selectedAttack);

                        ConsoleUI.Write($"\n\t{pokemon.CurrentEvolution.Name} used ");
                        pokemon.Attack(attackIndex);
                        //ConsoleUI.Write("\n");
                    }

                    ConsoleUI.PromptForContinue();
                } catch (Exception ex)
                {
                    localException = ex;
                }
                
            
            } while (!goBack);


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
                    SelectedMenuIndex = GetListSelectionFromReadKey(menu);

                    switch (SelectedMenuIndex)
                    {
                        case 0:
                            SelectedMenuIndex = null;
                            DisplayPokemonList(PokemonList);
                            break;
                        case 1:
                            ConsoleUI.Clear();
                            ConsoleUI.WriteLine($"{pokemon.CurrentEvolution.Name} knows {pokemon.Attacks.Count} attacks:");
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
                            ConsoleUI.Write($"\n\t{pokemon.CurrentEvolution.Name} used ");
                            pokemon.RandomAttack();
                            break;
                        default:
                            SelectedMenuIndex = previousSelectedCommand;
                            throw new NotImplementedException();
                    }
                    ConsoleUI.PromptForContinue();
                    SelectedMenuIndex = previousSelectedCommand;
                }
                catch (Exception ex)
                {
                    SimulationException = ex;
                    SelectedMenuIndex = previousSelectedCommand;
                }
            } while (SelectedMenuIndex == previousSelectedCommand);
        }

        private static void SelectAttack(Pokemon pokemon)
        {
            bool goBack = false;
            int selectedAttackMenuIndex;
            Exception? localException = null;

            do
            {
                try {
                    ConsoleUI.Clear();
                    Console.WriteLine($"Which attack would you like {pokemon.CurrentEvolution.Name} to use?");
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
                    
                    selectedAttackMenuIndex = GetListSelectionFromReadKey(pokemon.Attacks);
                    
                    if (selectedAttackMenuIndex == 0)
                    {
                        goBack = true;
                        break;
                    }
                    
                    if (selectedAttackMenuIndex > 0)
                    {
                        ConsoleUI.Clear();
                        ConsoleUI.WriteLine("\n");
                        ConsoleUI.Write($"\n\t{pokemon.CurrentEvolution.Name} used ");
                        pokemon.Attack(selectedAttackMenuIndex - 1);
                    }

                    localException = null;
                    ConsoleUI.PromptForContinue();
                } catch (Exception ex)
                {
                    localException = ex;
                }
            } while (!goBack);
        }

        private static void DisplayAttacks(Pokemon pokemon)
        {
            foreach ((Attack attack, int index) in pokemon.Attacks.Select((Attack, index) => (Attack, index)))
            {
                Console.Write("\n\t");
                DisplayAttackInfo(attack);
            }
        }

        private static void DisplayAttackInfo(Attack attack)
        {
            ConsoleUI.Write($"Level {attack.BasePower} ");
            DisplayAttack(attack);
            ConsoleUI.Write(" attack.");
        }

        private static void DisplayAttack(Attack attack)
        {
            ConsoleUI.ForegroundColor = attack.ElementColor;
            ConsoleUI.Write($"{attack}");
            ConsoleUI.ResetColor();
            
        }

        private static void DisplayMenuException(string menuExceptionMessage)
        {
            ConsoleUI.WriteError(menuExceptionMessage);
        }

        private static int GetListSelectionFromReadKey<T>(List<T> list)
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

        private int GetListSelectionFromReadLine<T>(List<T> list)
        {

            var rawInput = ConsoleUI.ReadLine();
            var isNumber = int.TryParse(rawInput, out int index);
            if (!isNumber)
                throw new Exception("Please use a number to make your selection");
            int selection = index;
            ArgumentOutOfRangeException.ThrowIfGreaterThan(selection, list.Count);

            return selection;
        }
    }
}