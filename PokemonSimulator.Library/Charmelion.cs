// Ignore Spelling: Charmelion
using UI;

namespace PokemonSimulator.Library
{
    internal class CharmelionEvolution : Charmelion
    {
        public CharmelionEvolution(Evolution currentEvolution)
            : base(currentEvolution.CurrentEvolution)
        { }

        public class NextEvolution(Evolution previousStage)
            : CharizardEvolution(previousStage)
        { };

        protected override void _Evolve()
        {
            ConsoleUI.WriteLine($"\n{CurrentEvolution.Name} is evolving...\n");
            List<Attack> newAttacks = [
                new Attack("Flamethrower", Type, 80)
            ];
            foreach (Attack attack in newAttacks)
            {
                Attacks.Add(attack);
            }
            //Level += 1;
            CurrentEvolution = new NextEvolution(this.CurrentEvolution);
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
    public class Charmelion : FirePokemon, IEvolvable
    {
        public override Evolution CurrentEvolution { get; protected set; }

        public Charmelion(Evolution previousStage)
            : base("Charmelion", previousStage.Attacks)
        {
            Level = previousStage.Level;
            //CurrentEvolution = new Evolution(Type, Name, Attacks);
            CurrentEvolution = previousStage;
        }
        private class NextEvolution(Evolution previousStage) 
            : CharizardEvolution(previousStage) {};

        //protected override void _Evolve()
        //{
        //    ConsoleUI.WriteLine($"\n{CurrentEvolution.Name} is evolving...\n");
        //    List<Attack> newAttacks = [
        //        new Attack("Explosive Vortex", Type, 330),
        //        new Attack("Crimson Storm", Type, 300)
        //    ];
        //    foreach (Attack attack in newAttacks) { 
        //        Attacks.Add(attack);
        //    }
        //    //Level += 1;
        //    CurrentEvolution = new NextEvolution(CurrentEvolution);
        //    Level = CurrentEvolution.Level;
        //    Name = CurrentEvolution.Name;
        //    ConsoleUI.WriteLine($"\n\tNow it is a {CurrentEvolution.Name} and its level is {Level}!");
        //    foreach (Attack attack in newAttacks) {
        //        ConsoleUI.Write($"\t{CurrentEvolution.Name} now knows ");
        //        ConsoleUI.ForegroundColor = attack.ElementColor;
        //        ConsoleUI.WriteLine(attack.ToString());
        //        ConsoleUI.ResetColor();
        //    }
        //}
    }
}