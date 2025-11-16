using UI;

namespace PokemonSimulator.Library
{
    public class Attack
    {
        public string Name { get; }
        public ElementType Type { get; }
        public int BasePower { get; private set; }
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
            ConsoleUI.ForegroundColor = ElementColor;
            ConsoleUI.Write($"{Name}");
            ConsoleUI.ResetColor();
            ConsoleUI.Write($" with a total power of {BasePower + level}!");
        }

        public override string ToString() => Name;
    }
}
