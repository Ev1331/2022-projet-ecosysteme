using System;
namespace projet_ecosysteme_2022
{
    public abstract class Plant : Organism
    {
        int rootRadius;
        int sowingRadius;
        Type seed;

        public Plant(Color color, double x, double y, Simulation simulation, int healthPoints, int energyPoints, int rootRadius, int sowingRadius) : base(color, x, y, simulation, healthPoints, energyPoints)
        {
            this.Color = color;
            this.X = x;
            this.Y = y;
            this.HealthPoints = healthPoints;
            this.EnergyPoints = energyPoints;
            this.rootRadius = rootRadius;
            this.sowingRadius = sowingRadius;
        }

        public int RootRadius { get { return this.rootRadius; } set { this.rootRadius = value; } }
        public int SowingRadius { get { return this.sowingRadius; } set { this.sowingRadius = value; } }
        public Type Seed { get { return this.seed; } set { this.seed = value; } }
        private void Eat(DrawableObject obj)
        {
            Simu.RemoveObjet((OrganicWaste)obj);
            this.EnergyPoints += 10;
        }

        public virtual void Expand(DrawableObject obj)
        {
            Seed = obj.GetType();

        }

        public override void Update()
        {
            base.Update();

            if (this.EnergyPoints == 0)
            {
                this.HealthPoints -= 5;
                this.EnergyPoints += 5;
            }
            if (this.HealthPoints <= 0)
            {
                Simu.AddObjet(new OrganicWaste(Colors.Green, this.X, this.Y, Simu));
                Simu.RemoveObjet(this);
            }


            Random random = new Random();
            if (random.Next(0, 75) == 5)
            {
                Expand(this);
            }



            (var listContact, var visibleObjects) = Simu.NearbyObject(this, this.rootRadius, this.rootRadius);
            foreach (DrawableObject obj in listContact)
            {
                if (obj is OrganicWaste)
                {
                    Eat(obj);
                    break;
                }
            }
        }
    }
}

