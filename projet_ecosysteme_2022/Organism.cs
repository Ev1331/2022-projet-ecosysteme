using System;
namespace projet_ecosysteme_2022
{
    public abstract class Organism : SimulationObject
    {
        int healthPoints;
        int energyPoints;

        public Organism(Color color, double x, double y, Simulation simulation, int healthPoints, int energyPoints) : base(color, x, y, simulation) 
        {
        }

        public int HealthPoints { get { return this.healthPoints; } set { this.healthPoints = value; } }
        public int EnergyPoints { get { return this.energyPoints; } set { this.energyPoints = value; } }

        public override void Update()
        {
            this.EnergyPoints -= 10;
        }
    }

}
