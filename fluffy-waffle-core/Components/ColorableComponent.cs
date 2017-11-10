using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace fluffy_waffle_core.Components
{
    public abstract class ColorableComponent : RenderableComponent
    {
        public ColorAnimation Animation;

        public void ChangeLineColor(Color c) => Control.Dispatcher.Invoke(() => { ((Shape)Control).Stroke = new SolidColorBrush(c); });

        public void SetAnimation(Color color, Duration duration) => Animation = new ColorAnimation(color, duration) { AutoReverse = true};
    }
}
