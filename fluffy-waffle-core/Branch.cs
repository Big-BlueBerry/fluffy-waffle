using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace fluffy_waffle_core
{
    public class Branch : Drawing, IConnectable
    {
        protected override Shape _shape { get; set; }
        public Double Weight { get; set; }

        public Neuron From { get; set; }
        public Neuron To { get; set; }
        IValuable IConnectable.From { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IValuable IConnectable.To { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Branch()
        {
            Weight = new Random(unchecked((int)DateTime.Now.Ticks)).NextDouble() * 2 - 1;
            _shape = new Line();

            // middle of line
            SetLineColor(Colors.HotPink);
            SetTextColor(Colors.Black);
        }
        
        public void SetLineStart()
        {
            ((Line)_shape).X1 = this.From.Position.X;
            ((Line)_shape).Y1 = this.From.Position.Y;
        }

        public void SetLineEnd()
        {
            ((Line)_shape).X2 = this.To.Position.X;
            ((Line)_shape).Y2 = this.To.Position.Y;
        }
        
        public double FowardPass()
        {
            return Weight * From.Value;
        }

        public void Connect(IValuable from, IValuable to)
        {
            throw new NotImplementedException();
        }
    }
}
