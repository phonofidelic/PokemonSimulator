namespace Simulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DynamicSimulation simulation = new();
            //simulation.Start();
            HardCodedSimulation hardCodedSimulation = new();
            hardCodedSimulation.Start();
        }
    }
}
