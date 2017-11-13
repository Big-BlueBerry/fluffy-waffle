using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace fluffy_waffle_core.Components
{
    public abstract class RenderableComponent : IComponent
    {
        public CompObject Parent { get; set; }
        private Vector _pos= new Vector();
        public Vector Pos
        {
            get => _pos;
            set
            {
                _pos = value;
                Canvas.SetLeft(Control, _pos.X);
                Canvas.SetTop(Control, _pos.Y);
            }
        }
        public Panel ParentPanel { get; private set; }
        public UIElement Control { get; private set; }
        
        public void InitControls(Panel panel, UIElement control)
        {
            ParentPanel = panel;
            Control = control;
        }
        public void Init()
        {
            if (!ParentPanel.Children.Contains(Control))
                ParentPanel.Children.Add(Control);
        }
    }
}
