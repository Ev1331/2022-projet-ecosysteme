using System;
namespace projet_ecosysteme_2022
{
    public abstract class SimulationObject : DrawableObject
    {
        Simulation simulation;

        public Simulation Simu { get { return simulation; } }
        public SimulationObject(Color color, double x, double y, Simulation simulation) : base(color, x, y)
        {
            this.simulation = simulation;
        }

        abstract public void Update();
    }
}
