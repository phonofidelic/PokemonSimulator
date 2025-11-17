using UI;

namespace PokemonSimulator.Library
{
    public abstract class Pokemon : IEvolvable, ICloneable
    {
        public virtual Evolution CurrentEvolution { get; protected set; }
        //public virtual Pokemon CurrentEvolution { get; protected set; }
        public virtual string Name { get; protected set; }

        private int _level = 1;
        public int Level {  
            get { return _level + (EvolutionLevel * 10); }
            protected set => _level = value; 
        }

        //private int _level = 1;
        //private int Level;

        //public int GetLevel()
        //{
        //    return _level + (EvolutionLevel * 10);
        //}

        //public void SetLevel(int value)
        //{
        //    Level = value;
        //}

        public ElementType Type { get; private init; }

        public List<Attack> Attacks { get; protected set; }
        public int EvolutionLevel { get; private set; } = 0;

        public Pokemon(ElementType type, string name, List<Attack> attacks)
        {
            //var baseEvolution = new Evolution(Type, Name, Attacks);
            CurrentEvolution = new Evolution((Pokemon)Clone());
            Name = name;
            Type = type;
            Attacks = attacks;
            _level = 1;
        }
        public Pokemon(ElementType type, string name, List<Attack> attacks, int level)
        {
            //CurrentEvolution = new Evolution((Pokemon)Clone());
            Name = name;
            Type = type;
            Attacks = attacks;
            _level = level;
        }

        private readonly Random _random = new Random();

        public void RaiseLevel()
        {
            var nextEvolution = CurrentEvolution.CurrentEvolution;
            ConsoleUI.Debug("BEFORE RAISE lEVEL");
            ConsoleUI.Debug($"EvolutionLevel: {EvolutionLevel}");
            ConsoleUI.Debug($"Level: {Level}");
            ConsoleUI.Debug($"CurrentEvolution.Level: {CurrentEvolution.Level}");
            ConsoleUI.Debug($"nextEvolution.Level: {nextEvolution.Level}");
            CurrentEvolution.Level += 1;
            ConsoleUI.Debug("\n\nAFTER RAISE lEVEL");
            ConsoleUI.Debug($"EvolutionLevel: {EvolutionLevel}");
            ConsoleUI.Debug($"Level: {Level}");
            ConsoleUI.Debug($"CurrentEvolution.Level: {CurrentEvolution.Level}");
            ConsoleUI.Debug($"nextEvolution.Level: {nextEvolution.Level}");

            ConsoleUI.WriteLine($"\n{CurrentEvolution.Name} has leveled up! {CurrentEvolution.Name} is now at level {CurrentEvolution.Level + (EvolutionLevel * 10)}.");
            if (CurrentEvolution._level - EvolutionLevel > 9)
            {
                CurrentEvolution._Evolve();
                CurrentEvolution.Level += 1;
                EvolutionLevel++;
            }
        }

        public void Evolve()
        {
            //Level += 10;
            _Evolve();
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

        public void Attack(int attackIndex) {
            Attacks[attackIndex].Use(Level);
        }

        
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString() =>  Name;
    }
}
