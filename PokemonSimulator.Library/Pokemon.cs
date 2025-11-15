using UI;

namespace PokemonSimulator.Library
{
    public abstract class Pokemon : IEvolvable
    {
        public virtual Pokemon CurrentEvolution { get; protected set; }
        public virtual string Name { get; protected set; }
        public int Level { get; protected set; } = 1;
        public ElementType Type { get; private set; }

        public List<Attack> Attacks { get; protected set; }

        public Pokemon(ElementType type, string name, List<Attack> attacks)
        {
            CurrentEvolution = this;
            Name = name;
            Type = type;
            Attacks = attacks;
        }

        private readonly Random _random = new Random();

        public void Evolve()
        {
            CurrentEvolution._Evolve();
        }
        protected virtual void _Evolve()
        {
            ConsoleUI.WriteLine($"{Name} has reached its final stage of evolution!");
        }

        public void RandomAttack()
        {
            int randomIndex = _random.Next(0, Attacks.Count);
            Attacks[randomIndex].Use(Level);
        }

        public void Attack() {
            // ToDo: Get attack by name
        }

        public void RaiseLevel() {
            Level++;
            Console.WriteLine($"\n{Name} has leveled up! {Name} is now at level {Level}.");
        }        

        public override string ToString() =>  Name;
    }
}
