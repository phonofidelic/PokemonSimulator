// Ignore Spelling: Charizard

using UI;

namespace PokemonSimulator.Library
{
    internal class Charizard : FirePokemon, IEvolvable
    {
        public override Pokemon CurrentEvolution {  get; protected set; }
        private readonly FirePokemon? NextEvolution = null;

        //public Charizard(List<Attack> initialAttacks)
        public Charizard(Pokemon previousStage)
            : base("Charizard", previousStage.Attacks)
        {
            Level = previousStage.Level;
            CurrentEvolution = this;
        }

        protected override void _Evolve()
        {
            ConsoleUI.WriteLine($"{Name} has reached its final stage of evolution!");
        }
    }
}