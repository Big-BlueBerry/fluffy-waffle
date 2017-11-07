using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using Vector = System.Windows.Vector;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace fluffy_waffle_core.Components
{
    public interface ILeftDragableComponent : ILeftMouseComponent
    {
        void LeftMouseUp(object sender, MouseEventArgs e);
        void MouseLeave(object sender, MouseEventArgs e);
        void MouseMove(object sender, MouseEventArgs e);
    }

    public class ShapeDragComponent : RenderableComponent, ILeftDragableComponent
    {
        private bool _isClicked;
        private Point _firstPosition;
        
        void ILeftMouseComponent.LeftMouseDown(object sender, MouseEventArgs e)
        {
            _firstPosition = e.GetPosition(_control);
            _isClicked = true;
        }

        void ILeftDragableComponent.LeftMouseUp(object sender, MouseEventArgs e)
        {
            _isClicked = false;
        }

        void ILeftDragableComponent.MouseLeave(object sender, MouseEventArgs e)
        {
            if (_isClicked) _isClicked = false;
        }

        void ILeftDragableComponent.MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isClicked) return;

            var pos = e.GetPosition(_parent);
            
            Canvas.SetLeft(_control, pos.X - _firstPosition.X);
            Canvas.SetTop(_control, pos.Y - _firstPosition.Y);
        }
    }
}

