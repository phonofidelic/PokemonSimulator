// Ignore Spelling: Charmelion
using UI;

namespace PokemonSimulator.Library
{
    public class Charmelion : FirePokemon, IEvolvable
    {
        public override Pokemon CurrentEvolution { get; protected set; }

        public Charmelion(Pokemon previousStage)
            : base("Charmelion", previousStage.Attacks)
        {
            Level = previousStage.Level;
            CurrentEvolution = this;
        }
        private class NextEvolution(Pokemon previousStage) 
            : Charizard(previousStage) {};

        protected override void _Evolve()
        {
            ConsoleUI.WriteLine($"\n{CurrentEvolution.Name} is evolving...\n");
            List<Attack> newAttacks = [
                new Attack("Explosive Vortex", Type, 330),
                new Attack("Crimson Storm", Type, 300)
            ];
            foreach (Attack attack in newAttacks) { 
                Attacks.Add(attack);
            }
            Level += 10;
            CurrentEvolution = new NextEvolution(this);
            Name = CurrentEvolution.Name;
            ConsoleUI.WriteLine($"\n\tNow it is a {CurrentEvolution.Name} and its level is {Level}!");
            foreach (Attack attack in newAttacks) {
                ConsoleUI.Write($"\t{CurrentEvolution.Name} now knows ");
                ConsoleUI.ForegroundColor = attack.ElementColor;
                ConsoleUI.WriteLine(attack.ToString());
                ConsoleUI.ResetColor();
            }
        }
    }
}