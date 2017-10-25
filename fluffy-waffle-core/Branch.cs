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
    public class Branch : IDrawable
    {
        private TextBlock _text { get; set; }
        private Line _line{ get; set; }
        public UIElement Control => _line;
        public UIElement TextControl => _text;
        public Vector Position { get; set; }
        public Double Weight { get; set; }

        public Neuron Start { get; set; }
        public Neuron End { get; set; }
        
        public Branch(Neuron start, Neuron end)
        {
            Weight = new Random().NextDouble();
            _line = new Line();
            _text = new TextBlock();
            Start = start;
            End = end;
            // middle of line
            Position = (Start.Position + End.Position) / 2.0;

            SetLineStart();
            SetLineEnd();
            
            SetBranchColor(Colors.HotPink);
            SetText();
            SetTextColor(Colors.Black);
            TextMove();
        }

        public void SetLineStart()
        {
            _line.X1 = this.Start.Position.X;
            _line.Y1 = this.Start.Position.Y;
            TextMove();
        }

        public void SetLineEnd()
        {
            _line.X2 = this.End.Position.X;
            _line.Y2 = this.End.Position.Y;
            TextMove();
        }

        public void SetBranchColor(Color c)
        {
            SolidColorBrush branchColor = new SolidColorBrush() { Color = c };
            // 원래 색깔변하는 코드
            _line.Dispatcher.Invoke(() => { _line.Stroke = branchColor; });
        }

        public void BranchAnimation(Color c)
        {
            ColorAnimation animation = new ColorAnimation(c, new Duration(new TimeSpan(0, 0, 1)));
            animation.AutoReverse = true;
            _line.Stroke.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }


        public void SetTextColor(Color c)
        {
            SolidColorBrush textColor = new SolidColorBrush() { Color = c };
            _text.Dispatcher.Invoke(() => { _text.Foreground = textColor; });
        }

        public void SetText()
        {
            this._text.Text = String.Format("{0:0.###}", Weight);
        }

        public void TextMove()
        {
            this._text.Margin = new Thickness(
                Position.X,
                Position.Y,
                0, 0);
        }

        public void FowardPass()
        {
            this.End.Value += this.Start.Value * this.Weight;
        }
    }
}
