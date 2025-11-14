namespace PokemonSimulator.Library
{
    public abstract class WaterPokemon(string name, List<Attack> attacks) : 
        Pokemon(ElementType.Water, name, attacks)
    {
    }
}

