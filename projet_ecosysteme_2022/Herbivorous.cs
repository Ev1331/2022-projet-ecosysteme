using System;
namespace projet_ecosysteme_2022
{
    public abstract class Herbivorous : Animal
    {
        int visibleObjectsLength;
        int listContactLength;
        DrawableObject obj;
        public Herbivorous(Color color, double x, double y, Simulation simulation, int healthPoints, int energyPoints, int strength, bool female, int gestationTime, int fieldOfView, int contactRange, int cooldown, int dischargeFreq) : base(color, x, y, simulation, healthPoints, energyPoints, strength, female, gestationTime, fieldOfView, contactRange, cooldown, dischargeFreq)
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
            this.Cooldown = cooldown;
            this.DischargeFreq = dischargeFreq;
        }

        private void Eat(Plant obj)
        {
            Simu.RemoveObjet(obj);
            this.EnergyPoints += 5;
            this.Cooldown = 2;

        }

        public override void Update()
        {
            base.Update();

            Random random = new Random();

            (var listContact, var visibleObjects) = Simu.NearbyObject(this, this.FieldOfView, this.ContactRange);

            visibleObjectsLength = listContact.Count;

            if (visibleObjectsLength > 0)
            {
                obj = listContact[random.Next(0, visibleObjectsLength - 1)];

                if (obj is Plant && Cooldown <= 0)
                {
                    Eat((Plant)obj);
                }
                else if (obj is Herbivorous)
                {
                    if (((Herbivorous)obj).Female == true && ((Herbivorous)obj).GestationTime == -1 && this.Female == false) // Si c'est une femelle non enceinte
                    {
                        Breed(this, obj); //Reproduction
                    }
                    else if (this.Female == true && this.GestationTime == -1 && ((Herbivorous)obj).Female == false)

                    {
                        Breed(this, obj); //Reproduction
                    }
                }
            }


            listContactLength = visibleObjects.Count;

            if (listContactLength > 0)
            {
            obj = visibleObjects[random.Next(0, listContactLength - 1)];

                if (obj is Plant)
                {
                    if (obj.X < this.X)
                    {
                        this.X -= 10;
                    }
                    else
                    {
                        this.X += 10;
                    }
                    if (obj.Y < this.Y)
                    {
                        this.Y -= 10;
                    }
                    else
                    {
                        this.Y += 10;
                    }
                }
            }
        }
    }
}

