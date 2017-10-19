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
        public List<Neuron> InputBranch;
        public List<(Neuron neuron, double weight)> OutputBranch;

        private Ellipse _shape;
        public UIElement Control => _shape;
        public Vector Position { get; set; }
        public Double Value { get; set; }

        public Neuron(Vector pos)
        {
            InputBranch = new List<Neuron>();
            OutputBranch = new List<(Neuron neuron, double weight)>();
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
        public void AppendInputBranch(Neuron neuron)
        {
            this.InputBranch.Add(neuron);

        }
        // 뉴런에서 다른뉴런으로 클릭 했을 때 추가
        public void AppendOutputBranch(Neuron neuron)
        {
            double weight = new Random().NextDouble();
            this.OutputBranch.Add((neuron, weight));          
        }

        // 각 Branch에 값을 전달
        public void Propagation()
        {
            foreach (Neuron neuron in this.InputBranch)
            {
                neuron.Propagation();
            }

            foreach ((Neuron neuron, double weight) in this.OutputBranch)
            {
                neuron.Value += this.Value * weight;
            }
        }
    }
}
