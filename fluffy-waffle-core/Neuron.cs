using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace fluffy_waffle_core
{
    public class Neuron : CompObject, IConnectable
    {
        public Ellipse Shape;
        public double Value;

        CompObject IComponent.Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Neuron(Ellipse ellipse, Board board, double value, string name) : base(ellipse, board, name)
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
