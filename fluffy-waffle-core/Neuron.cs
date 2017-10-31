using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

using Timer = System.Windows.Threading.DispatcherTimer;

namespace fluffy_waffle_core
{
    public class Neuron : Drawing, IValuable
    {
        public List<(Neuron neuron, Branch branch)> InputBranch;
        public List<(Neuron neuron, Branch branch)> OutputBranch;

        protected override Shape _shape { get; set; }
        public List<(IValuable, Branch)> ConnectList { get; set; }
        public int Size { get; set; }
        public Double Value { get; set; }

        public Double NetworkValue;
        public Double OutputValue;

        public bool IsNeuronClicked;
        public Point NeuronFirstPosition;

        private Timer _timer;

        public Neuron(Vector pos)
        {
            InputBranch = new List<(Neuron neuron, Branch branch)>();
            OutputBranch = new List<(Neuron neuron, Branch branch)>();

            // 초기 세팅, Value 는 test용
            Position = pos;
            Value = 1;
            Size = 1;
            InitTimer();

            ConnectList = new List<(IValuable, Branch)>();

            _shape = new Ellipse();
            ((Ellipse)_shape).SetCircle(Position, 30);
            ((Ellipse)_shape).Tag = this;
            SetLineColor(Colors.Blue);
            SetFillColor(Colors.White);

            InitDrag();
        }

        public void AppendInputBranch(Neuron neuron, Branch branch)
        {
            this.InputBranch.Add((neuron, branch));
            // 이 뉴런은 hidden layer의 neuron이기 때문에 값을 초기화한다.
            if (Value == 1)
                Value = 0;
        }

        public void AppendOutputBranch(Neuron neuron, Branch branch)
        {
            this.OutputBranch.Add((neuron, branch));
        }

        public void PassTo(IValuable connectable)
        {

        }

        public bool Connect(IValuable target)
        {
            // if already connected 
            foreach ((IValuable valuable, Branch _) in ConnectList)
            {
                if (valuable.Equals(target))
                    return false;
            }

            Branch branch = new Branch();
            branch.Connect(this, target);
            ConnectList.Add((target, branch));
            return true;
        }

        public bool IsNeuronInputBranch(Neuron neuron)
        {
            return InputBranch.Any((t) => t.neuron == neuron);
        }

        public bool IsNeuronOutputBranch(Neuron neuron)
        {
            return OutputBranch.Any((t) => t.neuron == neuron);
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
                _shape.Margin = new Thickness(
                        _shape.Margin.Left + delta.X,
                        _shape.Margin.Top + delta.Y,
                        _shape.Margin.Right + delta.X,
                        _shape.Margin.Bottom + delta.Y
                    );
                Point newPoint = new Point();
                newPoint.X = delta.X;
                newPoint.Y = delta.Y;

                this.Position += (Vector)newPoint;
                MoveText(Position);
                foreach ((Neuron neuron, Branch branch) in InputBranch)
                {
                    branch.SetLineEnd();
                }
                foreach ((Neuron neuron, Branch branch) in OutputBranch)
                {
                    branch.SetLineStart();
                }
            };
        }

        private void InitTimer()
        {
            _timer = new Timer()
            {
                Interval = new TimeSpan(0, 0, 1)
            };

            // TODO
            //_timer.Tick += FowardPassToOutputBranches;
        }
    }
}
