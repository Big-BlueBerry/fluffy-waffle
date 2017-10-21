using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

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

        public Neuron(Vector pos)
        {
            InputBranch = new List<(Neuron neuron, Line line)>();
            OutputBranch = new List<(Neuron neuron, double weight, Line line)>();
            DrawingBranch = new List<Line>();

            // 초기 세팅, Value 는 test용
            Position = pos;
            Value = 1;
            _shape = new Ellipse();
            _shape.SetCircle(Position, 30);
            _shape.Stroke = System.Windows.Media.Brushes.Blue;
            _shape.Fill = System.Windows.Media.Brushes.White;
            _shape.Tag = this;
        }

        // 다른뉴런에서 클릭 됐을 때 추가
        public void AppendInputBranch(Neuron neuron, Line line)
        {
            this.InputBranch.Add((neuron, line));

        }
        // 뉴런에서 다른뉴런으로 클릭 했을 때 추가
        public void AppendOutputBranch(Neuron neuron, Line line)
        {
            double weight = new Random().NextDouble();
            this.OutputBranch.Add((neuron, weight, line));          
        }

        // 각 Branch에 값을 전달
        public void Propagation()
        {
            foreach ((Neuron neuron, Line line) in this.InputBranch)
            {
                neuron.Propagation();
            }

            foreach ((Neuron neuron, double weight, Line line) in this.OutputBranch)
            {
                neuron.Value += this.Value * weight;
            }
        }

        public bool IsNeuronInBranch(Neuron neuron)
        {
            return InputBranch.Any((t) => t.neuron == neuron) || 
                   OutputBranch.Any((t) => t.neuron == neuron);
        }
    }
}
