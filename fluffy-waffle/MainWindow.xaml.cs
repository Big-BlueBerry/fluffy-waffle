using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using fluffy_waffle_core;

namespace fluffy_waffle
{
    public partial class MainWindow : Window
    {
        private List<IDrawable> _drawables = new List<IDrawable>();
        List<Neuron> Neurons;
        private Neuron _clicked = null;
        private Vector _temp;
        private bool _isAddingMode = false;

        public MainWindow()
        {   
            InitializeComponent();

            AddNeuronBtn.Click += AddNeuronBtn_Click;
            
            Neurons = new List<Neuron>();

            Neurons.Add(new Neuron(new Vector(100, 100)));
            Neurons.Add(new Neuron(new Vector(200, 200)));

            foreach(Neuron neuron in Neurons)
            {
                neuron.Control.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                neuron.Control.MouseRightButtonDown += Control_MouseRightButtonDown;
                AddDrawable(neuron);
            }
        }

        private void AddNeuronBtn_Click(object sender, RoutedEventArgs e)
        {
            _isAddingMode = !_isAddingMode;
            if (_isAddingMode)
            {
                AddNeuronBtn.Content = "Cancel";
                AddNeuronBtn.Background = Brushes.LightPink;
            }
            else
            {
                AddNeuronBtn.Content = "Add";
                AddNeuronBtn.Background = Brushes.White;
            }
        }

        // 첫 클릭 => 두번째 클릭으로 branch 생성
        private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var neuron = (sender as Shape).Tag as Neuron;
            if (neuron != null)
            {
                if (_clicked == null)
                {
                    _clicked = neuron;
                }
                else if (_clicked != neuron && !_clicked.IsNeuronInBranch(neuron))
                {
                    Line line = DrawLine(_clicked, neuron);
                    _clicked.AppendOutputBranch(neuron, line);
                    neuron.AppendInputBranch(_clicked, line);

                    _clicked = null;
                }
            }
            Debug.WriteLine($"어엉어 :{neuron.Value}\n");
        }

        // 우클릭시 전파
        private void Control_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var neuron = (sender as Shape).Tag as Neuron;
            neuron.Propagation();
            Debug.WriteLine($"하읏 :{neuron.Value}\n");
        }

        // 두 Neuron에 선 추가
        private Line DrawLine(Neuron start, Neuron end)
        {
            Line line = new Line();
            line.X1 = start.Position.X;
            line.Y1 = start.Position.Y;
            line.X2 = end.Position.X;
            line.Y2 = end.Position.Y;

            line.Stroke = Brushes.HotPink;
            ShapeCanvas.Children.Add(line);
            return line;
        }

        void AddDrawable(IDrawable item)
        {
            // 드래그 이벤트 추가
            if (item is Neuron)
            {
                var neu = item as Neuron;
                var shape = neu.Control as Ellipse;
                shape.MouseLeftButtonDown += (s, e) =>
                {
                    neu.NeuronFirstPosition = e.GetPosition(shape);
                    neu.IsNeuronClicked = true;
                };
                shape.MouseLeftButtonUp += (s, e) => neu.IsNeuronClicked = false;
                shape.MouseLeave += (s, e) =>
                {
                    if (neu.IsNeuronClicked) neu.IsNeuronClicked = false;
                };
                shape.MouseMove += (s, e) =>
                {
                    if (!neu.IsNeuronClicked) return;
                    var delta = e.GetPosition(shape) - neu.NeuronFirstPosition;
                    shape.Margin = new Thickness(shape.Margin.Left + delta.X,
                        shape.Margin.Top + delta.Y,
                        shape.Margin.Right + delta.X,
                        shape.Margin.Bottom + delta.Y);

                    Point newPoint = new Point();
                    newPoint.X = delta.X;
                    newPoint.Y = delta.Y;

                    neu.Position += (Vector)newPoint;
                    foreach((Neuron neron, Line line) in neu.InputBranch)
                    {
                        line.X2 = neu.Position.X;
                        line.Y2 = neu.Position.Y;
                    }
                    foreach ((Neuron neron, double weight, Line line) in neu.OutputBranch)
                    {
                        line.X1 = neu.Position.X;
                        line.Y1 = neu.Position.Y;
                    }
                };
            }

            _drawables.Add(item);
            ShapeCanvas.Children.Add(item.Control);
        }

        private void ShapeCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_isAddingMode)
            {
                var pos = e.GetPosition(ShapeCanvas);
                var neuron = new Neuron(new Vector(pos.X, pos.Y));
                Neurons.Add(neuron);
                AddDrawable(neuron);
            }
        }
    }
}
