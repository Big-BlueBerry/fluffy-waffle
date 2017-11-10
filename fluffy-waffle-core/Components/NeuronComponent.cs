using System;
using System.Windows.Shapes;
using fluffy_waffle_core.Components;

namespace fluffy_waffle_core
{
    public class NeuronComponent : RenderableComponent,IConnectable
    {
        public Ellipse Shape;
        public double Value;

        CompObject IComponent.Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public NeuronComponent(Ellipse ellipse, Board board, double value, string name) : base(ellipse, board, name)
        {
            Shape = ellipse;
            Value = value;
        }

        public void Connect(IConnectable target)
        {
            throw new NotImplementedException();
        }

        bool IConnectable.IsConnected(IConnectable target)
        {
            throw new NotImplementedException();
        }
        
        void IComponent.Init()
        {
            throw new NotImplementedException();
        }
    }
}
