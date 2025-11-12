namespace Simulator
{
    internal class MenuListItem<T>(int index, T value)
    {
        public int Index { get; } = index;
        public T Value { get; } = value;
    }
}
