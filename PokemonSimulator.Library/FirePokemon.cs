namespace PokemonSimulator.Library
{
    public abstract class FirePokemon(string name, List<Attack> attacks) : 
        Pokemon(ElementType.Fire, name, attacks)
    {
    }
}
