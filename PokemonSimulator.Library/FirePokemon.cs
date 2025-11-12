namespace PokemonSimulator.Library
{
    internal class FirePokemon(string name, List<Attack> attacks) : 
        Pokemon(name, attacks)
    {
        private ElementType Type { get; } = ElementType.Fire;
    }
}
