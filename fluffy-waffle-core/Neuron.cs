using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Timers;

namespace fluffy_waffle_core
{
    public class Neuron : IDrawable
    {
        public List<(Neuron neuron, Line line)> InputBranch;
        public List<(Neuron neuron, double weight, Line line)> OutputBranch;
        public List<Line> DrawingBranch;

        private Ellipse _shape;
        public UIElement Control => _shape;
        public Vector Position { get; set; }
        public Double Value { get; set; }

        public bool IsNeuronClicked;
        public Point NeuronFirstPosition;

        private Timer _timer;        

        public Neuron(Vector pos)
        {
            InputBranch = new List<(Neuron neuron, Line line)>();
            OutputBranch = new List<(Neuron neuron, double weight, Line line)>();
            DrawingBranch = new List<Line>();

            // 초기 세팅, Value 는 test용
            Position = pos;
            Value = 1;
            InitTimer();

            _shape = new Ellipse();
            _shape.SetCircle(Position, 30);
            _shape.Stroke = Brushes.Blue;
            _shape.Fill = Brushes.White;
            _shape.Tag = this;
        }

        private void InitTimer()
        {
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += PropagationToOutputBranches;
        }

        public void AppendInputBranch(Neuron neuron, Line line)
        {
            this.InputBranch.Add((neuron, line));
        }
        
        public void AppendOutputBranch(Neuron neuron, Line line)
        {
            double weight = new Random().NextDouble();
            this.OutputBranch.Add((neuron, weight, line));          
        }

        public void Propagation()
        {
            this._timer.Start();
            foreach ((Neuron neuron, double weight, Line line) in this.OutputBranch)
            {   
                neuron.Value += this.Value * weight;
                line.Dispatcher.Invoke(() => { line.Stroke = Brushes.Aqua; });
            }
        }

        private void PropagationToOutputBranches(object sender, EventArgs e)
        {
            _timer.Stop();
            
            foreach ((Neuron neuron, double weight, Line line) in this.OutputBranch)
            {
                neuron.Propagation();
                line.Dispatcher.Invoke(() => { line.Stroke = Brushes.HotPink; });
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
    }
}
