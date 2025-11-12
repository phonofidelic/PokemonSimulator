namespace PokemonSimulator.Library
{
    public abstract class Pokemon(ElementType type, string name, List<Attack> attacks) : IEvolvable
    {
        public string Name { get; private set; } = name;
        public int Level { get; private set; } = 1;
        public ElementType Type { get; private set; } = type;

        private List<Attack> Attacks { get; } = attacks;
        private readonly Random _random = new Random();

        public void Evolve()
        {
            Level += 10;
            Console.WriteLine($"\n{Name} is evolving...");
            // ToDo: Change name
            // Name = newName
            Console.WriteLine($"Now it is a {Name} and its level is {Level}");
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

        public override string ToString() {
            return $"{Name} is a level {Level} {Type} Pokemon.";
        }
    }
}
