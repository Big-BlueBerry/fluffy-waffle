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
        public Color FillColor;
        protected TextBlock _text;
        protected Shape _shape;

        public Vector Position { get; set; }
        public UIElement Control => _shape;
        public UIElement TextControl => _text; 

        public Drawing()
        {
            FillColor = Colors.Aqua;
            AnimationColor = Colors.LightBlue;
            ChangeAnimationColor(AnimationColor);
            _text = new TextBlock();
        }

        protected void MoveText(double x, double y)
        {
            _text.Margin = new Thickness(x, y, 0, 0);
        }

        protected void SetText(double value)
        {
            _text.Text = String.Format("{0:0.###}", value);
        }

        protected void BeginAnimation(DependencyProperty property)
        {
            _shape.BeginAnimation(property, Animation);
        }

        public void ChangeAnimationColor(Color color)
        {
            AnimationColor = color;
            Animation = new ColorAnimation(AnimationColor, new Duration(new TimeSpan(0, 0, 0, 0, 500)))
            {
                AutoReverse = true
            };
        }
    }
}
