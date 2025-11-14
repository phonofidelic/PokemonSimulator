using System.Collections;
using UI;

namespace MenuSystem
{
    internal class MenuList<T, I> : IEnumerable<I>, IDisplayable, ICommandable where I : MenuListItem<T>
    {
        private List<MenuListItem<T>> _list = [];
        internal MenuList() { }
        internal MenuList(List<T> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                _list.Add(new(i + 1, list[i]));
            }
        }

        public void Add(MenuListItem<T> item)
        {
            // ToDo: Turn this into a type error, maybe by typing `Command` as an enum instead of an int?
            if (_list.Exists(p => p.Command == item.Command))
                throw new Exception($"Could not add item '{item.Value}' with command '{item.Command}' in MenuList. The command already exists");

            _list.Add(item);
        }

        public MenuListItem<T> Get(int? command)
        {
            var selectedItem = _list.FirstOrDefault(item => item.Command == command);
            return selectedItem ?? throw new Exception($"'{command}' is not a valid command");
        }

        public void Display()
        {
            foreach (var item in _list)
            {
                ConsoleUI.WriteLine($"\n\t{item}");
            }
        }

        // ToDo: Remove this
        public void DisplayCommand()
        {
            ConsoleUI.WriteLine("ToDo: This should not be called from a MenuList");
        }

        public int GetCommand()
        {
            return GetSelectionIndexFromKeyPress();
        }
        private int GetSelectionIndexFromKeyPress()
        {
            var keyPressed = ConsoleUI.ReadKey(intercept: true).KeyChar.ToString();
            var isIndex = int.TryParse(keyPressed, out int index);
            if (!isIndex)
                throw new Exception("Please use a number to make your selection");
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, _list.Count);

            return index;
        }

        public IEnumerator<I> GetEnumerator()
        {
            foreach (I item in _list)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
