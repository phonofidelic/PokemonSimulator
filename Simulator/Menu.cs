using PokemonSimulator.Library;
using Simulator.UI;

namespace Simulator
{
    delegate void MenuIntroDisplayCallback();
    //public class Menu<T, I> : Menu<T> where I : Menu<T>
    //{

    //}
    public class Menu<T> : IDisplayable
    {
        protected int? _selectedCommand { get; private set; } = null;
        public Exception? MenuException { get; private set; } = null;
        private MenuIntroDisplayCallback? _menuIntroDisplayCallback = null;

        private MenuList<T, MenuListItem<T>> _menuList;
        internal Menu(MenuList<T, MenuListItem<T>> menuList)
        {
            _menuList = menuList;
        }
        internal Menu(MenuList<T, MenuListItem<T>> menuList, MenuIntroDisplayCallback introDisplayCallback)
        {
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
                    _selectedCommand = _menuList.GetSelectionIndexFromKeyPress();
                    MenuException = null;

                    // Display next IDisplayable
                    _menuList.Get(_selectedCommand).Display();
                }
                catch (Exception ex) {
                    MenuException = ex;
                    _selectedCommand = null;
                }
            } while (_selectedCommand == null);
        
            ConsoleUI.WriteLine("\nPress any key to exit the simulator");
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