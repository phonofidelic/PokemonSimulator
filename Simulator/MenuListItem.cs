using Simulator.UI;
using System.Collections;

namespace Simulator
{
    delegate void MenuListItemDisplayCallback<T>(int command, T value);
    // ToDo: Pass in Display method as a dependency?
    internal class MenuListItem<T> : IDisplayable
    {
        public int Command { get; }
        public T Value { get; protected set; }
        private MenuListItemDisplayCallback<T>? _displayCallback = null;
        public MenuListItem(int command, T value)
        {
            Command = command;
            Value = value;
        }
        public MenuListItem(int command, T value, MenuListItemDisplayCallback<T> callback)
        {
            Command = command;
            Value = value;
            _displayCallback = callback;
        }

        public void Display()
        {
            if (_displayCallback != null)
            {
                _displayCallback(Command, Value);
            } else
            {
                ConsoleUI.WriteLine($"\n\t{this}");
            }
        }

        public override string ToString() => $"{Command}. {Value}";
    }
}
