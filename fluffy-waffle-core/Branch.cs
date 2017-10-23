using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace fluffy_waffle_core
{
    public class Branch
    {
        public TextBlock Text { get; set; }
        public Line Line { get; set; }
        public Double Weight { get; set; }
        public SolidColorBrush BranchColor { get; set; }
        public SolidColorBrush TextColor { get; set; }

        public Vector Start { get; set; }
        public Vector End { get; set; }
        
        public Branch(Vector start, Vector end)
        {
            Weight = new Random().NextDouble();
            Line = new Line();
            Text = new TextBlock();
            SetStart(start);
            SetEnd(end);
            SetBranchColor(Brushes.HotPink);
            SetText();
            SetTextColor(Brushes.Black);
            TextMove();
        }

        public void SetStart(Vector start)
        {
            this.Start = start;
            Line.X1 = start.X;
            Line.Y1 = start.Y;
            TextMove();
        }

        public void SetEnd(Vector end)
        {
            this.End = end;
            Line.X2 = end.X;
            Line.Y2 = end.Y;
            TextMove();
        }

        public void SetBranchColor(SolidColorBrush brush)
        {
            this.BranchColor = brush;
            Line.Dispatcher.Invoke(() => { Line.Stroke = brush; });
        }

        public void SetTextColor(SolidColorBrush brush)
        {
            this.TextColor = brush;
            Text.Dispatcher.Invoke(() => { Text.Foreground = brush; });
        }

        public void SetText()
        {
            this.Text.Text = this.Weight.ToString();
        }

        public void TextMove()
        {
            this.Text.Margin = new Thickness(
                (this.Start.X + this.End.X) / 2,
                (this.Start.Y + this.End.Y) / 2,
                0, 0);
        }
    }
}
