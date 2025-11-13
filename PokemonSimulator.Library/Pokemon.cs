namespace PokemonSimulator.Library
{
    public abstract class Pokemon(ElementType type, string name, List<Attack> attacks) : IEvolvable
    {
        public string Name { get; protected set; } = name;
        public int Level { get; protected set; } = 1;
        public ElementType Type { get; private set; } = type;

        public List<Attack> Attacks { get; protected set; } = attacks;
        private readonly Random _random = new Random();

        public abstract void Evolve();

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

        public override string ToString() =>  $"{Name} is a level {Level} {Type} Pokemon.";
    }
}
