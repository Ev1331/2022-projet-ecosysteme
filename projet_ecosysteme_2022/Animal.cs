using System;

namespace projet_ecosysteme_2022
{
    public abstract class Animal : Organism
    {
        int strength;
        bool female;
        int gestationTime;
        int fieldOfView;
        int contactRange;
        int cooldown;
        int dischargeFreq;
        Type baby;

        public Animal(Color color, double x, double y, Simulation simulation, int healthPoints, int energyPoints, int strength, bool female, int gestationTime, int fieldOfView, int contactRange, int cooldown = 5, int dischargeFreq = 15) : base(color, x, y, simulation, healthPoints, energyPoints) { }

        public int Strength { get { return this.strength; } set { this.strength = value; } }
        public bool Female { get { return this.female; } set { this.female = value; } }
        public int GestationTime { get { return this.gestationTime; } set { this.gestationTime = value; } }
        public int FieldOfView { get { return this.fieldOfView; } set { this.fieldOfView = value; } }
        public int ContactRange { get { return this.contactRange; } set { this.contactRange = value; } }
        public int Cooldown { get { return this.cooldown; } set { this.cooldown = value; } }
        public int DischargeFreq { get { return this.dischargeFreq; } set { this.dischargeFreq = value; } }
        public Type Baby { get { return this.baby; } set { this.baby = value; } }


        private void Dies()
        {
            Simu.RemoveObjet(this);
            Simu.AddObjet(new Meat(Colors.Brown, this.X, this.Y, Simu));
        }

        public void Breed(Animal subject, DrawableObject target)
        {
            if (subject.Female) //Si il s'agit de la femelle
            {
                subject.GestationTime = 20;
            }
            else if (((Animal)target).Female)
            {
                ((Animal)target).GestationTime = 20;
            }
        }
        public virtual void Birth(Animal mom)
        {
            Baby = mom.GetType();
        }

        private void Discharge()
        {
            Simu.AddObjet(new OrganicWaste(Colors.Green, this.X, this.Y, Simu));
            this.dischargeFreq = 10;
        }

        private void Move()
        {
            Random random = new Random();
            this.X += random.Next(-10, 10);
            this.Y += random.Next(-10, 10);

            if (this.X < 0)
            {
                this.X = 1000 + this.X;
            }
            else if (this.X > 1000)
            {
                this.X = this.X - 1000;
            }
            if (this.Y < 0)
            {
                this.Y = 500 + this.Y;
            }
            else if (this.Y > 500)
            {
                this.Y = this.Y - 500;
            }
        }

        private void LoseHealth()
        {
            this.HealthPoints -= 10;
            this.EnergyPoints += 10;
        }
        public override void Update()
        {
            base.Update();

            this.Move();

            this.DischargeFreq -= 1;
            this.Cooldown -= 1;

            if (this.EnergyPoints == 0)
            {
                this.LoseHealth();
            }
            if (this.HealthPoints <= 0)
            {
                this.Dies();
            }
            if (this.DischargeFreq == 0)
            {
                this.Discharge();
            }
            if (this.GestationTime >= 0)
            {
                if (this.GestationTime > 0)
                {
                    this.GestationTime -= 1;
                }
                else if (this.GestationTime == 0)
                {
                    Birth(this);
                    this.GestationTime = -1;
                }
            }
        }
    }
}
