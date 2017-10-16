using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace fluffy_waffle_core
{
    public class Neuron : IDrawable
    {
        public List<(Neuron to, float weight)> To;

        private Ellipse _shape;
        public UIElement Control => _shape;
        public Vector Position { get; set; }

        public Neuron(Vector pos)
        {
            To = new List<(Neuron to, float weight)>();
            Position = pos;
            _shape = new Ellipse();
            _shape.SetCircle(Position, 3);
        }
    }
}
