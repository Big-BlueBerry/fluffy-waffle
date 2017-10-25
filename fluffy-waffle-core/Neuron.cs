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
    public class Neuron : IDrawable
    {
        public List<(Neuron neuron, Branch branch)> InputBranch;
        public List<(Neuron neuron, Branch branch)> OutputBranch;

        private Ellipse _shape;
        private TextBlock _text;
        public UIElement Control => _shape;
        public UIElement TextControl => _text;
        public Vector Position { get; set; }
        public Double Value { get; set; }
        public Double Delta { get; set; }

        public bool IsNeuronClicked;
        public Point NeuronFirstPosition;

        private Timer _timer;        

        public Neuron(Vector pos)
        {
            InputBranch = new List<(Neuron neuron, Branch branch)>();
            OutputBranch = new List<(Neuron neuron, Branch branch)>();

            // 초기 세팅, Value 는 test용
            _text = new TextBlock();
            Position = pos;
            Value = 1;
            Delta = 0;
            InitTimer();

            _shape = new Ellipse();
            _shape.SetCircle(Position, 30);
            _shape.Stroke = Brushes.Blue;
            _shape.Fill = Brushes.White;
            _shape.Tag = this;
            
            InitDrag();
            SetText();
            MoveText();
        }

        private void InitTimer()
        {
            _timer = new Timer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += FowardPassToOutputBranches;
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

        public void FowardPass()
        {
            this._timer.Start();

            foreach ((Neuron neuron, Branch branch) in this.OutputBranch)
            {
                branch.FowardPass();
                branch.BranchAnimation(Colors.Aqua);
                neuron.SetText();
            }
        }

        private void FowardPassToOutputBranches(object sender, EventArgs e)
        {
            _timer.Stop();
            
            foreach ((Neuron neuron, Branch branch) in this.OutputBranch)
            {
                neuron.FowardPass();
            }
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
                MoveText();
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

        private void SetText()
        {
            _text.Text = String.Format("{0:0.###}", Value);
        }

        private void MoveText()
        {
            _text.Margin = new Thickness(
                Position.X,
                Position.Y,
                0, 0);
        }
    }
}
