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
using fluffy_waffle_core.Components;

namespace fluffy_waffle
{
    public partial class MainWindow : Window
    {
        private bool _isAddMode = false;
        Board board;
        private List<CompObject> _objectList;

        public MainWindow()
        {
            InitializeComponent();

            board = new Board();
            _objectList = new List<CompObject>();

            // before
            //var c = new MouseEventHandleCompObject(ellipse, board, "뀨");
            //c.AddComponent<ShapeRandomMoveSexComponent>().InitControls(canvas, ellipse);

            var c = new MouseEventHandleCompObject(ellipse, board, "의읭ㅇㅇ");
            c.AddComponent<ShapeDragComponent>().InitControls(canvas, ellipse);
        }

        public class MouseEventHandleCompObject : CompObject
        {
            public MouseEventHandleCompObject(Ellipse ellipse, Board board, string name) : base(ellipse, board, name)
            {}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _isAddMode = !_isAddMode;
            if(_isAddMode)
            {
                addingButton.Content = "cancel";
                addingButton.Background = Brushes.HotPink;
            }
            else
            {
                addingButton.Content = "add";
                addingButton.Background = Brushes.White;
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(canvas);

            if (_isAddMode)
            {
                Ellipse shape = new Ellipse()
                {
                    Width = 100,
                    Height = 100,
                    Fill = Brushes.Aqua,
                    Stroke = Brushes.Black,
                };

                MouseEventHandleCompObject neuron = new MouseEventHandleCompObject(shape, board, "test");
                neuron.AddComponent<ShapeDragComponent>().InitControls(canvas, shape);
                NeuronComponent comp = neuron.AddComponent<NeuronComponent>();

                comp.InitControls(canvas, shape, 0, "test");
                comp.Pos = (Vector)pos;
                neuron.InitAllComponents();

                _objectList.Add(neuron);
            }
        }
    }
}

