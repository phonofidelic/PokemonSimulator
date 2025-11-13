namespace PokemonSimulator.Library
{
    public class Attack
    {
        public string Name { get; }
        public ElementType Type { get; }
        private int BasePower { get; }
        // private Dictionary<ElementType, ConsoleColor> ElementColor = new();
        public ConsoleColor ElementColor { get; private init; }

        public Attack(string name, ElementType type, int basePower)
        {
            Name = name;
            Type = type;
            BasePower = basePower;
            ElementColor = GetElementTypeColor(Type);
        }
        
        private ConsoleColor GetElementTypeColor(ElementType type) => type switch
        {
            ElementType.Fire => ConsoleColor.Red,
            ElementType.Water => ConsoleColor.Cyan,
            ElementType.Grass => ConsoleColor.Green,
            _ => throw new ArgumentOutOfRangeException(nameof(Type), $"{Type} is not a valid Element"),
        };
        public void Use(int level) {
            Console.WriteLine($"{Name} hit with a total power of {BasePower + level}");
        }

        public override string ToString() => Name;
    }
}
