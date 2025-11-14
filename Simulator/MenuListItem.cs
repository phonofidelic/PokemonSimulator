using Simulator.UI;
using System.Collections;

namespace Simulator
{
    //delegate void MenuListItemDisplayCallback<T>(int command, T value);
    delegate void MenuListItemDisplayCallback();
    internal class MenuListItem<T> : IDisplayable
    {
        public int Command { get; }
        public T Value { get; protected set; }
        private MenuListItemDisplayCallback? _displayCallback = null;
        public MenuListItem(int command, T value)
        {
            Command = command;
            Value = value;
        }
        public MenuListItem(int command, T value, MenuListItemDisplayCallback callback)
        {
            Command = command;
            Value = value;
            _displayCallback = callback;
        }

        public void Display()
        {
            if (_displayCallback != null)
            {
                _displayCallback();
            } else
            {
                ConsoleUI.WriteLine($"\n\t{this}");
            }
        }

        public override string ToString() => $"{Command}. {Value}";
    }
}
