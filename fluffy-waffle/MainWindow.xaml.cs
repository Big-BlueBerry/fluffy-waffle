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
        Board board;
        public MainWindow()
        {
            InitializeComponent();

            board = new Board();

            // before
            //var c = new MouseEventHandleCompObject(ellipse, board, "뀨");
            //c.AddComponent<ShapeRandomMoveSexComponent>().InitControls(canvas, ellipse);

            var c = new MouseEventHandleCompObject(ellipse, board, "의읭ㅇㅇ");
            c.AddComponent<ShapeDragComponent>().InitControls(canvas, ellipse);
        }

        public class MouseEventHandleCompObject : CompObject
        {
            public MouseEventHandleCompObject(Ellipse ellipse, Board board, string name) : base(ellipse, board, name)
            {
                ellipse.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
                ellipse.MouseLeftButtonUp += Ellipse_MouseLeftButtonUp;
                ellipse.MouseLeave += Ellipse_MouseLeave;
                ellipse.MouseMove += Ellipse_MouseMove;
            }

            private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
                => GetComponent<ILeftDragableComponent>()?.LeftMouseDown(sender, e);

            private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
                => GetComponent<ILeftDragableComponent>()?.LeftMouseUp(sender, e);

            private void Ellipse_MouseLeave(object sender, MouseEventArgs e)
                => GetComponent<ILeftDragableComponent>()?.MouseLeave(sender, e);

            private void Ellipse_MouseMove(object sender, MouseEventArgs e)
                => GetComponent<ILeftDragableComponent>()?.MouseMove(sender, e);
        }

        public class ShapeRandomMoveSexComponent : RenderableComponent, ILeftMouseComponent
        {
            Random r = new Random();
            void ILeftMouseComponent.LeftMouseDown(object sender, MouseEventArgs e)
            {
                Canvas.SetLeft(_control,  r.Next(0, (int)_parent.ActualWidth));
                Canvas.SetTop(_control, r.Next(0, (int)_parent.ActualHeight));
            }
        }
    }
}

