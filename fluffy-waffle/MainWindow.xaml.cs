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

        public MainWindow()
        {   
            InitializeComponent();

            _temp = new Vector();
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

        // 첫 클릭 => 두번째 클릭으로 branch 생성
        private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var neuron = (sender as Shape).Tag as Neuron;
            if (neuron != null)
            {
                if (_clicked == null)
                {
                    _clicked = neuron;
                    _temp = neuron.Position;
                }
                else if (_clicked != neuron && !_clicked.IsNeuronInBranch(neuron))
                {
                    // if two neurons were faired set output and input branch
                    _clicked.AppendOutputBranch(neuron);
                    neuron.AppendInputBranch(_clicked);

                    DrawLine(neuron);
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

        private void DrawLine(Neuron neuron)
        {
            Line line = new Line();
            line.X1 = _temp.X;
            line.Y1 = _temp.Y;
            line.X2 = neuron.Position.X;
            line.Y2 = neuron.Position.Y;
            line.Stroke = Brushes.HotPink;
            ShapeCanvas.Children.Add(line);

            _clicked.DrawingBranch.Add(line);
            neuron.DrawingBranch.Add(line);
        }

        void AddDrawable(IDrawable item)
        {
            _drawables.Add(item);
            ShapeCanvas.Children.Add(item.Control);
        }  
    }
}
