using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace fluffy_waffle_core
{
    abstract public class DrawableComponent : Component
    {
        protected UIElement _control;
        protected Canvas _canvas;

        public void UpdateControl()
        {
            _canvas.Children.Add(_control);
        }

        public void InitCanvas(Canvas canvas, UIElement control)
        {
            _control = control;
            _canvas = canvas;
        }

        public override void Init()
        {
            if (_control == null) return;
            UpdateControl();
        }
    }
}
