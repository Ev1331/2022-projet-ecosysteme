using System;
namespace projet_ecosysteme_2022
{
    public abstract class Carnivorous : Animal
    {
        public Carnivorous(Color color, double x, double y, Simulation simulation, int healthPoints, int energyPoints, int strength, bool female, int gestationTime, int fieldOfView, int contactRange, int cooldown, int dischargeFreq) : base(color, x, y, simulation, healthPoints, energyPoints, strength, female, gestationTime, fieldOfView, contactRange, cooldown, dischargeFreq)
        {
            this.Color = color;
            this.X = x;
            this.Y = y;
            this.HealthPoints = healthPoints;
            this.EnergyPoints = energyPoints;
            this.Strength = strength;
            this.Female = female;
            this.GestationTime = gestationTime;
            this.FieldOfView = fieldOfView;
            this.ContactRange = contactRange;
            this.Cooldown = cooldown;
            this.DischargeFreq = dischargeFreq;
        }


        private void Attack(Carnivorous subject, DrawableObject target) //subject = le prédateur (toujours carnivore) et target = la proie
        {
            ((Animal)target).HealthPoints -= subject.Strength;
            subject.HealthPoints -= ((Animal)target).Strength;
            subject.Cooldown = 5;
        }

        private void Eat(DrawableObject obj)
        {
            Simu.RemoveObjet(obj);
            this.EnergyPoints += 5;
        }

        public override void Update()
        {
            base.Update();

            (var listContact, var visibleObjects) = Simu.NearbyObject(this, this.FieldOfView, this.ContactRange);

            foreach (DrawableObject obj in listContact)
            {
                if (obj is Meat)
                {
                    Eat(obj);

                    break;
                }
                else if (obj is Herbivorous)
                {
                    Attack(this, obj);

                    break;
                }
                else if (obj is Carnivorous)
                {
                    if (((Carnivorous)obj).Female == true && ((Carnivorous)obj).GestationTime == -1 && this.Female == false) // Si c'est une femelle non enceinte
                    {
                        Breed(this, obj); //Reproduction
                        break;
                    }
                    else if (this.Female == true && this.GestationTime == -1 && ((Carnivorous)obj).Female == false)

                    {
                        Breed(this, obj); //Reproduction
                        break;
                    }

                    break;
                }
            }

            foreach (DrawableObject obj in visibleObjects)
            {

                if (obj is Herbivorous)
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
                    break;
                }
                else if (obj is Meat)
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
                    break;
                }
            }
        }
    }
}