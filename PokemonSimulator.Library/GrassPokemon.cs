namespace PokemonSimulator.Library
{
    internal class GrassPokemon(string name, List<Attack> attacks) :
        Pokemon(name, attacks)
    {
        private ElementType Type { get; } = ElementType.Grass;
    }
}
