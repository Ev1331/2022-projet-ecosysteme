using System;
namespace projet_ecosysteme_2022
{
    public class OrganicWaste : SimulationObject
    {
        int durability;
        public OrganicWaste(Color color, double x, double y, Simulation simulation) : base(color, x, y, simulation)
        {
            this.Color = color;
            this.X = x;
            this.Y = y;
            this.durability = 10;
        }

        public override void Update()
        {
            this.durability -= 1;
            if (this.durability == 0)
            {
                Simu.RemoveObjet(this);

                Random random = new Random();
                if (random.Next(0, 50) == 1)
                {
                    Simu.AddObjet(new Fern(Colors.LightGreen, this.X, this.Y, Simu, 10, 20, 30, 15));
                }
            }
        }
    }
}


