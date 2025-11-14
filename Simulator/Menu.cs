using PokemonSimulator.Library;
using Simulator.UI;

namespace Simulator
{
    delegate void MenuIntroDisplayCallback();

    public class Menu<T> : IDisplayable
    {
        public string Name { get; private set; }
        protected int? _selectedCommand { get; private set; } = null;
        public Exception? MenuException { get; private set; } = null;
        private MenuIntroDisplayCallback? _menuIntroDisplayCallback = null;

        private MenuList<T, MenuListItem<T>> _menuList;
        internal Menu(string name, MenuList<T, MenuListItem<T>> menuList)
        {
            Name = name;
            _menuList = menuList;
        }
        internal Menu(string name, MenuList<T, MenuListItem<T>> menuList, MenuIntroDisplayCallback introDisplayCallback)
        {
            Name = name;
            _menuList = menuList;
            _menuIntroDisplayCallback = introDisplayCallback;
        }

        public void Display()
        {
            do
            {
                ConsoleUI.Clear();
                _menuIntroDisplayCallback?.Invoke();

                try
                {
                    if (MenuException != null)
                    {
                        DisplayMenuException(MenuException.Message);
                    }
                    _menuList.Display();
                    _selectedCommand = _menuList.GetCommand();
                    MenuException = null;

                    // Display next IDisplayable
                    _menuList.Get(_selectedCommand).Display();
                }
                catch (Exception ex) {
                    MenuException = ex;
                    _selectedCommand = null;
                }
            } while (_selectedCommand == null);

            ConsoleUI.WriteLine($"\nPress any key to exit the {Name} menu");
            ConsoleUI.ReadKey(intercept: true);
        }

        private static void DisplayMenuException(string menuExceptionMessage)
        {
            ConsoleUI.ForegroundColor = ConsoleColor.Red;
            ConsoleUI.WriteLine(menuExceptionMessage);
            ConsoleUI.ResetColor();
        }
    }
}