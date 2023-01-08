

namespace projet_ecosysteme_2022
{
    internal class Fern : Plant
    {
        public Fern(Color color, double x, double y, Simulation simulation, int healthPoints, int energyPoints, int rootRadius, int sowingRadius) : base(color, x, y, simulation, healthPoints, energyPoints, rootRadius, sowingRadius)
        {
            this.Color = color;
            this.X = x;
            this.Y = y;
            this.HealthPoints = healthPoints;
            this.EnergyPoints = energyPoints;
            this.RootRadius = rootRadius;
            this.SowingRadius = sowingRadius;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Expand(DrawableObject obj)
        {
            base.Expand(obj);
            Random random = new Random();
            Simu.AddObjet(new Fern(Colors.LightGreen, this.X + random.Next(-this.SowingRadius, this.SowingRadius), this.Y + random.Next(-this.SowingRadius, this.SowingRadius), Simu, 50, 20, 50, 15));
        }
    }
}
