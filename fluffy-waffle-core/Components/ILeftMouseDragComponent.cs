﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using Vector = System.Windows.Vector;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace fluffy_waffle_core.Components
{
    public interface ILeftMouseDragComponent : ILeftMouseComponent
    {
        void LeftMouseUp(object sender, MouseEventArgs e);
        void MouseLeave(object sender, MouseEventArgs e);
        void MouseMove(object sender, MouseEventArgs e);
    }

    public class ShapeDragComponent : RenderableComponent, ILeftMouseComponent
    {
        private bool _isClicked;
        private Point _firstPosition;
        
        public void LeftMouseDown(object sender, MouseEventArgs e)
        {
            _firstPosition = e.GetPosition(ParentPanel);
            _isClicked = true;
        }

        public void LeftMouseUp(object sender, MouseEventArgs e)
        {
            _isClicked = false;
        }

        public void MouseLeave(object sender, MouseEventArgs e)
        {
            if (_isClicked) _isClicked = false;
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isClicked) return;

            var pos = e.GetPosition(ParentPanel);
            Pos = (Vector)pos;
        }

        public new void InitControls(Panel panel, UIElement control)
        {
            base.InitControls(panel, control);
            InitMouseControl();
        }

        private void InitMouseControl()
        {
            Control.MouseLeftButtonDown += LeftMouseDown;
            Control.MouseLeftButtonUp += LeftMouseUp;
            Control.MouseLeave += MouseLeave;
            Control.MouseMove += MouseMove;
        }
    }
}

