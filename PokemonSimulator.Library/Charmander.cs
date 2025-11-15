// Ignore Spelling: Charmander
using UI;

namespace PokemonSimulator.Library
{
    public class Charmander : FirePokemon, IEvolvable
    {
        public override Pokemon CurrentEvolution { get; protected set; }
        
        public Charmander(List<Attack> initialAttacks)
            : base("Charmander", initialAttacks)
        {
            CurrentEvolution = this;
        }
        private class NextEvolution(Pokemon previousStage) 
            : Charmelion(previousStage) {};

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
            Level += 10;
            CurrentEvolution = new NextEvolution(this);
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
        public override string ToString() => CurrentEvolution.Name;
    }
}
