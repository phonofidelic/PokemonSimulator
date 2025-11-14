using UI;

namespace MenuSystem
{
    public delegate void MenuIntroDisplayCallback();
    public abstract class DisplayableCommand : IDisplayable, ICommandable
    {
        public abstract int? Command { get; }
        public abstract void Display();
        public abstract void DisplayCommand();
        public abstract int GetCommand();
    };
    //internal class Menu<T> : DisplayableCommand
    public class Menu : DisplayableCommand
    {
        public override int? Command { get; } = 0;
        public string Name { get; private set; }
        protected int? _selectedCommand { get; private set; } = null;
        public Exception? MenuException { get; private set; } = null;
        private MenuIntroDisplayCallback? _menuIntroDisplayCallback = null;
        private MenuIntroDisplayCallback? _menuDisplayCallback = null;


        //private MenuList<T, MenuListItem<T>> _menuList;
        private List<Menu> _subMenus = [];
        //internal Menu(string name, MenuList<T, MenuListItem<T>> menuList)
        //{
        //    Name = name;
        //    _menuList = menuList;
        //}
        //internal Menu(string name, MenuList<T, MenuListItem<T>> menuList, MenuIntroDisplayCallback introDisplayCallback)
        //{
        //    Name = name;
        //    _menuList = menuList;
        //    _menuIntroDisplayCallback = introDisplayCallback;
        //}

        /* Composite */
        public Menu(
            int command,
            string name,
            List<Menu> subMenus,
            MenuIntroDisplayCallback introDisplayCallback
        )
        {
            Name = name;
            Command = command;
            //_menuList = [];
            _subMenus = subMenus;
            _menuIntroDisplayCallback = introDisplayCallback;
        }

        /* Leaf */
        public Menu(
            int command,
            string name,
            MenuIntroDisplayCallback displayCallback
        )
        {
            Name = name;
            Command = command;
            //_menuList = [];
            //_subMenus = [];
            _menuDisplayCallback = displayCallback;
        }

        private int GetSelectionIndexFromKeyPress()
        {
            var keyPressed = ConsoleUI.ReadKey(intercept: true).KeyChar.ToString();
            var isIndex = int.TryParse(keyPressed, out int index);
            if (!isIndex)
                throw new Exception("Please use a number to make your selection");
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, _subMenus.Count);

            return index;
        }

        public override int GetCommand()
        {
            return GetSelectionIndexFromKeyPress();
        }

        public override void DisplayCommand()
        {
            ConsoleUI.WriteLine($"{Command}. {Name}");
        }

        public override void Display()
        {
            do
            {
                //if (_selectedCommand == 0)
                //    break;
                //if (_menuDisplayCallback == null)
                ConsoleUI.Clear();
                ConsoleUI.Debug($"{Name} _selectedCommand: {_selectedCommand}");
                ConsoleUI.WriteLine(Name);
                _menuIntroDisplayCallback?.Invoke();

                try
                {
                    if (MenuException != null)
                    {
                        DisplayMenuException(MenuException.Message, Name);
                    }

                    // If the menu has sub-menus, list them
                    if (_subMenus.Count > 0)
                    {
                        foreach (var subMenu in _subMenus)
                        {
                            subMenu.DisplayCommand();
                        }
                        // Prompt for new command selection
                        _selectedCommand = GetCommand();
                        MenuException = null;
                        if (_selectedCommand > 0)
                        {
                            // Display the subMenu
                            _subMenus.FirstOrDefault(subMenu => subMenu.Command == _selectedCommand)?.Display();
                        }
                        // Reset command
                        _selectedCommand = null;
                    }

                    _menuDisplayCallback?.Invoke();
                    ConsoleUI.WriteLine($"\n\tPress 0 to exit {Name}");
                    //var nextCommand = GetCommand();
                    //_selectedCommand = (nextCommand == 0) ? 0 : null;
                    ConsoleUI.ReadKey(intercept: true);
                    _selectedCommand = 0;


                    //_menuList.Display();

                    //_selectedCommand = _menuList.GetCommand();
                    //MenuException = null;

                    ////// Check if command is to exit the current menu
                    //ConsoleUI.WriteLine($"_selectedCommand from {Name}: {_selectedCommand}");
                    //if (_selectedCommand > 0)
                    //{
                    //    // Display next IDisplayable
                    //    _menuList.Get(_selectedCommand).Display();
                    //}
                    ////_selectedCommand = _menuList.GetCommand();
                }
                catch (Exception ex)
                {
                    MenuException = ex;
                    _selectedCommand = null;
                }
                _selectedCommand = null;
            } while (_selectedCommand == null);
            //ConsoleUI.WriteLine($"\nPress any key to exit the {Name} menu");
            //ConsoleUI.ReadKey(intercept: true);
            //ConsoleUI.WriteLine("Exiting the program...");
        }

        //public void Return()
        //{
        //    _selectedCommand = null;
        //}

        private static void DisplayMenuException(string menuExceptionMessage, string name)
        {
            ConsoleUI.ForegroundColor = ConsoleColor.Red;
            ConsoleUI.WriteLine($"{name} error: " + menuExceptionMessage);
            ConsoleUI.ResetColor();
        }
    }
}