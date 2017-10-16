using System;
using System.Windows.Shapes;
using System.Windows;

namespace fluffy_waffle_core
{
    public static class ShapeHelper
    {
        public static void SetCircle(this Ellipse ellipse, Vector center, Vector another)
        {
            var radius = Vector.Subtract(center, another).Length;
            SetCircle(ellipse, center, radius);
        }

        public static void SetCircle(this Ellipse ellipse, Vector center, double radius)
        {
            var x = center.X - radius;
            var y = center.Y - radius;
            ellipse.Width = ellipse.Height = radius * 2;
            ellipse.Margin = new Thickness(x, y, 0, 0);
        }
    }
}
