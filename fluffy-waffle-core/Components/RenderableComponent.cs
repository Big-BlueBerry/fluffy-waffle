using System.Windows;
using System.Windows.Controls;

namespace fluffy_waffle_core.Components
{
    public abstract class RenderableComponent : IComponent
    {
        public CompObject Parent { get; set; }
        protected Panel _parent;
        protected UIElement _control;

        public void InitControls(Panel panel, UIElement control)
        {
            _parent = panel;
            _control = control;
        }
        public void Init()
        {
            if (!_parent.Children.Contains(_control))
                _parent.Children.Add(_control);
        }
    }
}
