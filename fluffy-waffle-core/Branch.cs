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
    public class Branch : Drawing
    {
        private Line _line{ get; set; }
        public Double Weight { get; set; }

        public Neuron Start { get; set; }
        public Neuron End { get; set; }

        public Branch()
        {
            Weight = new Random(unchecked((int)DateTime.Now.Ticks)).NextDouble() * 2 - 1;
            _line = new Line();

            // middle of line
            SetBranchColor(Colors.HotPink);
            SetTextColor(Colors.Black);
        }

        public bool Connect(IValuable from, IValuable to)
        {
            
            throw new NotImplementedException();
        }

        public void SetLineStart()
        {
            _line.X1 = this.Start.Position.X;
            _line.Y1 = this.Start.Position.Y;
        }

        public void SetLineEnd()
        {
            _line.X2 = this.End.Position.X;
            _line.Y2 = this.End.Position.Y;
        }

        public void SetBranchColor(Color c)
        {
            SolidColorBrush branchColor = new SolidColorBrush() { Color = c };
            // 원래 색깔변하는 코드
            _line.Dispatcher.Invoke(() => { _line.Stroke = branchColor; });
        }
        
        public void SetTextColor(Color c)
        {
            SolidColorBrush textColor = new SolidColorBrush() { Color = c };
            _text.Dispatcher.Invoke(() => { _text.Foreground = textColor; });
        }
        

        public void FowardPass()
        {
            this.End.Value += this.Start.Value * this.Weight;
        }
    }
}
