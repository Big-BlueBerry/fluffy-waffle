using System;
using System.Collections.Generic;
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

        public MainWindow()
        {
            InitializeComponent();
            AddDrawable(new Neuron(new Vector(100, 100)));
        }

        void AddDrawable(IDrawable item)
        {
            _drawables.Add(item);
            ShapeCanvas.Children.Add(item.Control);
        }
    }
}
