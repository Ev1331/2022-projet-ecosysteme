using System;
namespace projet_ecosysteme_2022
{
    public class Meat : SimulationObject
    {
        int lifeExpectancy;

        public Meat(Color color, double x, double y, Simulation simulation) : base(color, x, y, simulation)
        {
            this.Color = color;
            this.X = x;
            this.Y = y;
            this.lifeExpectancy = 10;
        }

        public override void Update()
        {
            this.lifeExpectancy -= 1;
            if (lifeExpectancy <= 0)
            {
                Simu.AddObjet(new OrganicWaste(Colors.Green, this.X, this.Y, Simu));
                Simu.RemoveObjet(this);
            }
        }
    }
}


