namespace Simulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DynamicSimulation simulation = new();
            //simulation.Start();
            ExampleSimulation hardCodedSimulation = new();
            hardCodedSimulation.Start();
        }
    }
}
