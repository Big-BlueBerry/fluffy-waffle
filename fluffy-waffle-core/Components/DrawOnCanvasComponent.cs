using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;

namespace fluffy_waffle_core.Components
{
    public class DrawOnCanvasComponent : DrawableComponent
    {
        protected Shape _shape;

        public void InitShape(Shape shape, Canvas canvas, UIElement control)
        {
            _shape = shape;
            _control = _shape;
            base.InitCanvas(canvas, control);
        }

        public void UpdatePosition(Point position)
        {
            _shape.Margin = new Thickness(position.X, position.Y, 0, 0);
            UpdateControl();
        }
    }    
}
