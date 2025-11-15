using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSimulator.Library
{
    internal class EvolutionStage<T>() where T : Enum
    {
        public int Name { get; private set; }
        public Pokemon Pokemon { get; private set; }
    }
}
