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
        public double Value;

        public Connector(Point from, Point to, double weight)
        {
            _shape = new Line();
            Value = weight;

            // middle of line
            SetLineColor(Colors.HotPink);
            SetTextColor(Colors.Black);
            SetAnimationColor(Colors.Honeydew);
        }

        public void SetLineStart(Point point)
        {
            From = point;
            ((Line)_shape).X1 = point.X;
            ((Line)_shape).Y1 = point.Y;
        }

        public void SetLineEnd(Point point)
        {
            To = point;
            ((Line)_shape).X2 = point.X;
            ((Line)_shape).Y2 = point.Y;
        }
    }
}
