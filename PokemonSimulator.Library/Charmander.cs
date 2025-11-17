// Ignore Spelling: Charmander
using System.Security.Cryptography.X509Certificates;
using UI;

namespace PokemonSimulator.Library
{
    public class Evolution : Pokemon
    {
        //Evolution NextEvolution;
        public Evolution(ElementType type, String name, List<Attack> attacks)
            : base(type, name, attacks)
        { }
        public Evolution(Pokemon currentEvolution)
            : base(currentEvolution.Type, currentEvolution.Name, currentEvolution.Attacks, currentEvolution.Level)
        { }

        //internal abstract class NextEvolution(Evolution<T> previousEvolution) : Evolution<T>(previousEvolution);
        
        //private class NextEvolution : Evolution<T>
        //{
        //    private NextEvolution(Evolution<T> thisEvolution) 
        //        : base(thisEvolution.Type, thisEvolution.Name, thisEvolution.Attacks)
        //    {

        //    }
        //} 
    }

    internal class CharmanderEvolution : Evolution
    {
        public CharmanderEvolution(Charmander currentEvolution)
            : base(currentEvolution)
        {
            CurrentEvolution = this;
        }

        public class NextEvolution(Evolution previousStage)
            : CharmelionEvolution(previousStage)
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
            CurrentEvolution = new NextEvolution(CurrentEvolution).CurrentEvolution;
            Level = CurrentEvolution.Level;
            Name = CurrentEvolution.Name;
            ConsoleUI.WriteLine($"\n\tNow it is a {CurrentEvolution.Name} and its level is {CurrentEvolution.Level}!");
            foreach (Attack attack in newAttacks)
            {
                ConsoleUI.Write($"\t{CurrentEvolution.Name} now knows ");
                ConsoleUI.ForegroundColor = attack.ElementColor;
                ConsoleUI.WriteLine(attack.ToString());
                ConsoleUI.ResetColor();
            }
        }
    }

    public class Charmander : FirePokemon, IEvolvable
    {
        public override Evolution CurrentEvolution { get; protected set; }
        //public override Pokemon CurrentEvolution { get; protected set; }
        
        public Charmander(List<Attack> initialAttacks)
            : base("Charmander", initialAttacks)
        {
            // ToDo: Create the base Evolution stage
            var baseEvolution = new Evolution(Type, Name, Attacks);
            CurrentEvolution = new CharmanderEvolution((Charmander)Clone());
        }
        //private class NextEvolution(Pokemon previousStage) 
        //    : Charmelion(previousStage) {};
        //private class NextEvolution(Evolution previousStage) : CharmelionEvolution(previousStage);

        //protected void _Evolve()
        //{

        //}
        //protected override void _Evolve()
        //{
        //    ConsoleUI.WriteLine($"\n{CurrentEvolution.Name} is evolving...\n");
        //    List<Attack> newAttacks = [
        //        new Attack("Flamethrower", Type, 80)
        //    ];
        //    foreach (Attack attack in newAttacks)
        //    {
        //        Attacks.Add(attack);
        //    }
        //    //Level += 1;
        //    CurrentEvolution = new NextEvolution(CurrentEvolution);
        //    Level = CurrentEvolution.Level;
        //    Name = CurrentEvolution.Name;
        //    ConsoleUI.WriteLine($"\n\tNow it is a {CurrentEvolution.Name} and its level is {Level}!");
        //    foreach (Attack attack in newAttacks)
        //    {
        //        ConsoleUI.Write($"\t{CurrentEvolution.Name} now knows ");
        //        ConsoleUI.ForegroundColor = attack.ElementColor;
        //        ConsoleUI.WriteLine(attack.ToString());
        //        ConsoleUI.ResetColor();
        //    }
        //}
        public override string ToString() => CurrentEvolution.Name;
    }
}
