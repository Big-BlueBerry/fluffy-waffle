using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

using Vector = System.Windows.Vector;
using Timer = System.Windows.Threading.DispatcherTimer;

namespace fluffy_waffle_core
{
    public class Neuron : Drawing, IValuable
    {
        private List<Connector> _start { get; set; }
        private List<Connector> _end { get; set; }
        public List<(IValuable, Branch)> ConnectList { get; set; }

        protected override Shape _shape { get; set; }
        public int Size { get; set; }
        public Vector<double> Value { get; set; }

        public Double NetworkValue;
        public Double OutputValue;

        public bool IsNeuronClicked;
        public Point NeuronFirstPosition;

        private Timer _timer;

        public Neuron(Vector pos)
        {
            // 초기 세팅, Value 는 test용
            Position = pos;
            Value = DenseVector.OfArray(new double[] { 1 });
            Size = 1;

            ConnectList = new List<(IValuable, Branch)>();
            _start = new List<Connector>();
            _end = new List<Connector>();

            _shape = new Ellipse();
            ((Ellipse)_shape).SetCircle(Position, 30);
            ((Ellipse)_shape).Tag = this;
            SetLineColor(Colors.Blue);
            SetFillColor(Colors.White);

            InitDrag();
        }

        public void PassTo(IValuable target)
        {
            foreach((IValuable valuable, Branch branch) in ConnectList)
            {
                if(valuable == target)
                {
                    target.Value = branch.Weight * Value;
                    return;
                }
            }
        }

        public bool Connect(IValuable target)
        {
            // if already connected 
            foreach ((IValuable valuable, Branch _) in ConnectList)
            {
                if (valuable.Equals(target))
                    return false;
            }

            Branch branch = new Branch(this, target);
            ConnectList.Add((target, branch));
            return true;
        }

        public void AppendStartConn(Connector connector)
        {
            _start.Add(connector);
        }

        public void AppendEndConn(Connector connector)
        {
            _end.Add(connector);
        }
        
        public void InitDrag()
        {
            _shape.MouseLeftButtonDown += (s, e) =>
            {
                NeuronFirstPosition = e.GetPosition(_shape);
                IsNeuronClicked = true;
            };
            _shape.MouseLeftButtonUp += (s, e) => IsNeuronClicked = false;
            _shape.MouseLeave += (s, e) =>
            {
                if (IsNeuronClicked) IsNeuronClicked = false;
            };
            _shape.MouseMove += (s, e) =>
            {
                if (!this.IsNeuronClicked) return;
                var delta = e.GetPosition(_shape) - NeuronFirstPosition;

                Position += new Vector(delta.X, delta.Y);

                Move(delta.X, delta.Y, delta.X, delta.Y);
                MoveText(Position);
                MoveConnector();
            };
        }

        private void MoveConnector()
        {
            foreach (Connector conn in _start)
            {
                conn.SetLineStart((Point)this.Position);
            }

            foreach (Connector conn in _end)
            {
                conn.SetLineEnd((Point)this.Position);
            }
        }
    }
}
