using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_ecosysteme_2022
{
    internal class Tyrannosaurus : Carnivorous
    {

        public Tyrannosaurus(Color color, double x, double y, Simulation simulation, int healthPoints, int energyPoints, int strength, bool female, int gestationTime, int fieldOfView, int contactRange, int cooldown = 5, int dischargeFreq = 15) : base(color, x, y, simulation, healthPoints, energyPoints, strength, female, gestationTime, fieldOfView, contactRange, cooldown, dischargeFreq)
        {
            this.Color = color;
            this.X = x;
            this.Y = y;
            this.HealthPoints = healthPoints;
            this.EnergyPoints = energyPoints;
            this.Female = female;
            this.GestationTime = gestationTime;
            this.FieldOfView = fieldOfView;
            this.ContactRange = contactRange;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Birth(Animal mom)
        {
            base.Birth(mom);
            Random random = new Random();
            Simu.AddObjet(new Tyrannosaurus(Colors.Red, mom.X, mom.Y, Simu, 100, 100, random.Next(60, 95), Convert.ToBoolean(random.Next(2)), -1, 100, 20));
        }

    }
}
