namespace PokemonSimulator.Library
{
    public class Attack(string name, ElementType type, int basePower)
    {
        public string Name { get; } = name;
        public ElementType Type { get; } = type;
        private int BasePower { get; } = basePower;

        public void Use(int level) {
            Console.WriteLine($"{Name} hit with a total power of {BasePower + level}");
        }
    }
}
