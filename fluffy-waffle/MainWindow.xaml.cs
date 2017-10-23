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
        
        private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var neuron = (sender as Shape).Tag as Neuron;
            if (neuron != null)
            {
                if (_clicked == null)
                {
                    _clicked = neuron;
                }
                else if (_clicked != neuron && 
                    !_clicked.IsNeuronOutputBranch(neuron) && 
                    !neuron.IsNeuronInputBranch(_clicked))
                {
                    Branch branch = new Branch(_clicked, neuron);
                    DrawBranch(branch);
                    _clicked.AppendOutputBranch(neuron, branch);
                    neuron.AppendInputBranch(_clicked, branch);

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
        
        private void DrawBranch(Branch branch)
        {
            AddDrawable(branch);
        }

        void AddDrawable(IDrawable item)
        {
            _drawables.Add(item);
            ShapeCanvas.Children.Add(item.Control);
            ShapeCanvas.Children.Add(item.TextControl);
        }

        private void ShapeCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_isAddingMode)
            {
                var pos = e.GetPosition(ShapeCanvas);
                var neuron = new Neuron(new Vector(pos.X, pos.Y));
                neuron.Control.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                neuron.Control.MouseRightButtonDown += Control_MouseRightButtonDown;
                Neurons.Add(neuron);
                AddDrawable(neuron);
            }
        }
    }
}

