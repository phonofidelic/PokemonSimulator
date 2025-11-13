namespace PokemonSimulator.Library
{
    internal abstract class WatterPokemon(string name, List<Attack> attacks) : 
        Pokemon(ElementType.Water, name, attacks)
    {
    }
}
