using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using fluffy_waffle_core.Components;

namespace fluffy_waffle_core
{
    public class SynapseComponent : RenderableComponent, IConnect
    {
        public double Weight;

        public void Connect(IConnectable from, IConnectable to)
        {
            var pos = ((NeuronComponent)from).Pos;
            SetFrom(pos);
            pos = ((NeuronComponent)to).Pos;
            SetTo(pos);
        }

        public void SetFrom(Vector vector)
        {
            var line = (Line)Control;
            line.X1 = vector.X;
            line.Y1 = vector.Y;
        }
        public void SetTo(Vector vector)
        {
            var line = (Line)Control;
            line.X2 = vector.X;
            line.Y2 = vector.Y;
        }

        public void InitControl(Panel panel, UIElement control, IConnectable from, IConnectable to, double weight)
        {
            base.InitControls(panel, control);
            if (weight != 0)
                Weight = new Random().NextDouble();
            else
                Weight = weight;
            Connect(from, to);
        }
    }
}
