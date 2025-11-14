namespace MenuSystem
{
    delegate void ViewDisplayCallback();
    delegate void ViewDisplayCommandCallback();
    internal class DisplayableView(
        int command,
        string name,
        ViewDisplayCallback displayCallback,
        ViewDisplayCommandCallback displayCommandCallback) : IDisplayable
    {
        public int Command { get; private set; } = command;
        public string Name { get; private set; } = name;
        public void Display()
        {
            displayCallback();
        }

        public void DisplayCommand()
        {
            displayCommandCallback();
        }
    }
}
