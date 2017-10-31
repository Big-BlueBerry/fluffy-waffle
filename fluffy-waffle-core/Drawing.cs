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
    public abstract class Drawing : IDrawable
    {
        public ColorAnimation Animation;
        public Color AnimationColor;
        protected TextBlock _text;
        abstract protected Shape _shape { get; set; }

        public Vector Position { get; set; }
        public UIElement Control => _shape;
        public UIElement TextControl => _text;

        public Drawing()
        {
            _text = new TextBlock();
        }

        public void Move(double left, double top, double right, double bottom)
        {
            _shape.Margin = new Thickness(
                        _shape.Margin.Left + left,
                        _shape.Margin.Top + top,
                        _shape.Margin.Right + right,
                        _shape.Margin.Bottom + bottom
                    );
        }

        public void MoveText(Vector position)
        {
            _text.Margin = new Thickness(position.X, position.Y, 0, 0);
        }

        public void SetText(double value)
        {
            _text.Text = String.Format("{0:0.###}", value);
        }

        public void SetTextColor(Color c)
        {
            _text.Dispatcher.Invoke(() => { _text.Foreground = new SolidColorBrush(c); });

        }

        public void SetLineColor(Color c)
        {
            _shape.Dispatcher.Invoke(() => { _shape.Stroke = new SolidColorBrush(c); });

        }

        public void SetFillColor(Color color)
        {
            _shape.Fill = new SolidColorBrush(color);
        }

        public void SetAnimationColor(Color color)
        {
            AnimationColor = color;
            Animation = new ColorAnimation(AnimationColor, new Duration(new TimeSpan(0, 0, 0, 0, 500)))
            {
                AutoReverse = true
            };
        }

        protected void BeginAnimation(DependencyProperty property)
        {
            _shape.BeginAnimation(property, Animation);
        }
    }
}
