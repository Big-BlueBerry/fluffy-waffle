using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Distributions;

namespace fluffy_waffle_core
{
    public class Connector : Drawing
    {
        protected override Shape _shape { get; set; }
        Point From { get; set; }
        Point To { get; set; }
        double Weight;

        public Connector(Point from, Point to, double weight)
        {
            _shape = new Line();
            Weight = weight;

            // middle of line
            SetLineColor(Colors.HotPink);
            SetTextColor(Colors.Black);
            SetAnimationColor(Colors.Honeydew);
        }

        public void SetLineStart()
        {
            ((Line)_shape).X1 = this.From.X;
            ((Line)_shape).Y1 = this.From.Y;
        }

        public void SetLineEnd()
        {
            ((Line)_shape).X2 = this.To.X;
            ((Line)_shape).Y2 = this.To.Y;
        }
    }
}
