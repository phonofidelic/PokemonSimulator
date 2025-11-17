// Ignore Spelling: Charmelion
using UI;
using static PokemonSimulator.Library.CharmelionEvolution;

namespace PokemonSimulator.Library
{
    public class CharizardEvolution : Evolution
    {
        public CharizardEvolution(Evolution currentEvolution)
            : base(currentEvolution)
        { }

        protected override void _Evolve()
        {
            ConsoleUI.WriteLine($"\n{CurrentEvolution.Name} is evolving...\n");
            List<Attack> newAttacks = [
                new Attack("Explosive Vortex", Type, 330),
                new Attack("Crimson Storm", Type, 300)
            ];
            foreach (Attack attack in newAttacks)
            {
                Attacks.Add(attack);
            }
            //Level += 1;
            CurrentEvolution = new NextEvolution(CurrentEvolution);
            Level = CurrentEvolution.Level;
            Name = CurrentEvolution.Name;
            ConsoleUI.WriteLine($"\n\tNow it is a {CurrentEvolution.Name} and its level is {Level}!");
            foreach (Attack attack in newAttacks)
            {
                ConsoleUI.Write($"\t{CurrentEvolution.Name} now knows ");
                ConsoleUI.ForegroundColor = attack.ElementColor;
                ConsoleUI.WriteLine(attack.ToString());
                ConsoleUI.ResetColor();
            }
        }
    }
}