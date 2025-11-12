using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSimulator.Library
{
    internal abstract class Pokemon(string name, List<Attack> attacks) : IEvolvable
    {
        public string Name { get; private set; } = name;
        public int Level { get; private set; } = 1;

        private List<Attack> Attacks { get; } = attacks;
        private readonly Random _random = new Random();

        public void Evolve()
        {
            Level += 10;
            Console.WriteLine($"{Name} is evolving...");
            // ToDo: Change name
            // Name = newName
            Console.WriteLine($"Now it is a{Name}and its level is {Level}");
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
            Console.WriteLine($"{Name} has leveled up! {Name} is now at level {Level}.");
        }
    }
}
