namespace PokemonSimulator.Library
{
    public abstract class GrassPokemon(string name, List<Attack> attacks) :
        Pokemon(ElementType.Grass, name, attacks)
    {
    }
}
