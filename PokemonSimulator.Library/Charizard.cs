// Ignore Spelling: Charizard
using UI;

namespace PokemonSimulator.Library
{
    internal class Charizard : FirePokemon, IEvolvable
    {
        public override Pokemon CurrentEvolution {  get; protected set; }

        public Charizard(Pokemon previousStage)
            : base("Charizard", previousStage.Attacks)
        {
            Level = previousStage.Level;
            CurrentEvolution = this;
        }
    }
}