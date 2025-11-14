using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuSystem
{
    public class MenuContext : Dictionary<string, int?>
    {
        public int? SelectedAction { get; private set; } = null;
        public void Register(string contextName)
        {
            this[contextName] = null;
        }
        public void SetSelectedAction(string contextName, int? selectedAction)
        {
            this[contextName] = selectedAction;
            SelectedAction = selectedAction;
        }
        public int? GetSelectedAction(string contextName)
        {
            return this[contextName];
        }
        public override string ToString()
        {
            string result = "";
            foreach (var item in this)
            {
                result += $"{item.Key} : {item.Value}\n";
            }
            return result;
        }

        public void Debug()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{this}");
            Console.ResetColor();
        }
    }
}

