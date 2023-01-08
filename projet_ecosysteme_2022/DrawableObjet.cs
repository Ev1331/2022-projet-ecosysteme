using System;
namespace projet_ecosysteme_2022
{
    public abstract class DrawableObject
    {
        Color color;
        double x, y;
        public DrawableObject(Color color, double x = 0, double y = 0)
        {
            this.color = color;
            this.x = x;
            this.y = y;
        }

        public Color Color { get { return this.color; } set { this.color = value; } }
        public double X { get { return this.x; } set { this.x = value; } }
        public double Y { get { return this.y; } set { this.y = value; } }
    }
}