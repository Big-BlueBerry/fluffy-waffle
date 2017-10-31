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
        public Vector<double> NetworkValue { get; set; }
        public Vector<double> OutputValue { get; set; }
        public Vector<double> Delta { get; set; }

        public bool IsNeuronClicked;
        public Point NeuronFirstPosition;

        public Neuron(Vector pos)
        {
            // 초기 세팅, Value 는 test용
            Position = pos;
            Value = DenseVector.OfArray(new double[] { 1 });
            Size = 1;
            
            _start = new List<Connector>();
            _end = new List<Connector>();

            _shape = new Ellipse();
            ((Ellipse)_shape).SetCircle(Position, 30);
            ((Ellipse)_shape).Tag = this;
            SetLineColor(Colors.Blue);
            SetFillColor(Colors.White);

            InitDrag();
        }

        public void PassTo(IValuable target, Branch branch)
        {
            target.Value = branch.Weight * Value;
        }

        public Branch Connect(IValuable target)
        {
            Branch branch = new Branch(this, target);
            return branch;
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

        public void SetValue(Vector<double> passResult)
        {
            double sum = 0;
            foreach (double a in passResult)
                sum += a;

            NetworkValue = DenseVector.OfArray(new double[] { sum });
            ActivateNeuron(NetworkValue);
        }

        private void ActivateNeuron(Vector<double> networkResult)
        {
            OutputValue = Sigmoid(networkResult);
        }

        private Vector<double> Sigmoid(Vector<double> vector)
        {
            
            return 1 / (1 + MathNet.Numerics.LinearAlgebra.Double.Vector.Exp(-vector));
        }

        public void SetDelta(Vector<double> nextLayerDelta, Matrix<double> weights)
        {
            Matrix<double> transWeights = weights.Transpose();
            Delta = (nextLayerDelta * transWeights) * (Sigmoid(NetworkValue) * (1 - Sigmoid(NetworkValue)));
        }
    }
}
