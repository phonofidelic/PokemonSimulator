namespace PokemonSimulator.Library
{
    internal class WatterPokemon(string name, List<Attack> attacks) : 
        Pokemon(name, attacks)
    {
        private ElementType Type { get; } = ElementType.Watter;
    }
}
