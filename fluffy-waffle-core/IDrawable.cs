using System.Windows;

namespace fluffy_waffle_core
{
    public interface IDrawable
    {
        Vector Position { get; set; }
        UIElement Control { get; }
    }
}
