using System;
using System.Collections.Generic;

namespace projet_ecosysteme_2022
{
    public class Simulation : IDrawable
    {
        List<DrawableObject> objects;
        List<DrawableObject> nextState;
        int i;
        double distance;

        public Simulation()
        {
            objects = new List<DrawableObject>();
            nextState = new List<DrawableObject>();

            Random random = new Random();
            
                        for (i = 0; i < 100; i++)
                        {
                            nextState.Add(new Stegosaurus(Colors.DarkBlue, random.Next(1000), random.Next(500), this, 100, 100, random.Next(0, 30), Convert.ToBoolean(random.Next(2)), -1, 150, 20));
                        }
                        for (i = 0; i < 10; i++)
                        {
                            nextState.Add(new Tyrannosaurus(Colors.Red, random.Next(1000), random.Next(500), this, 100, 100, random.Next(60, 95), Convert.ToBoolean(random.Next(2)), -1, 150, 20));
                        }
                        for (i = 0; i < 300; i++)
                        {
                            nextState.Add(new Fern(Colors.LightGreen, random.Next(1000), random.Next(500), this, 10, 20, 50, 15));
                        }
            /*
            nextState.Add(new Fern(Colors.LightGreen, 100, 100, this, 10, 20, 50, 150));
            nextState.Add(new OrganicWaste(Colors.Green, 110, 100, this));
            nextState.Add(new OrganicWaste(Colors.Green, 95, 100, this));
            nextState.Add(new OrganicWaste(Colors.Green, 105, 100, this));
            nextState.Add(new OrganicWaste(Colors.Green, 100, 110, this));
            nextState.Add(new OrganicWaste(Colors.Green, 100, 90, this));
            */

            //nextState.Add(new Stegosaurus(Colors.DarkBlue, 100, 100, this, 100, 100, random.Next(0, 30), true, -1, 150, 20));
            //nextState.Add(new Stegosaurus(Colors.DarkBlue, 110, 100, this, 100, 100, random.Next(0, 30), false, -1, 150, 20));
            //nextState.Add(new Meat(Colors.Brown, 110, 100, this));
            //nextState.Add(new Meat(Colors.Brown, 90, 100, this));

        }

        public void AddObjet(DrawableObject obj)
        {
            nextState.Add(obj);
        }

        public void RemoveObjet(DrawableObject obj)
        {
            nextState.Remove(obj);
        }

        public (List<DrawableObject>, List<DrawableObject>) NearbyObject(DrawableObject subject, double fieldOfView, double contactRange)
        {
            List<DrawableObject> visibleObjects = new List<DrawableObject>();
            List<DrawableObject> listContact = new List<DrawableObject>(); //Liste d'objets trouvés
            foreach (DrawableObject target in objects) //Pour chaque organisme du monde,
            {
                if (!(target == subject))
                {

                    distance = Math.Pow((Math.Pow((target.X - subject.X), 2) + Math.Pow((target.Y - subject.Y), 2)), 0.5);

                    if (distance <= fieldOfView)
                    {
                        visibleObjects.Add(target);
                    }
                    if (distance <= contactRange)
                    {
                        listContact.Add(target);
                    }
                }
            }

            return (listContact, visibleObjects);
        }
        public void Update()
        {
            objects.Clear();
            foreach (SimulationObject obj in nextState)
            {
                objects.Add(obj);
            }
            foreach (SimulationObject drawable in objects)
            {
                drawable.Update();
            }
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            foreach (SimulationObject drawable in objects)
            {
                canvas.FillColor = drawable.Color;
                canvas.FillCircle(new Point(drawable.X, drawable.Y), 5.0);
            }
        }
    }
}