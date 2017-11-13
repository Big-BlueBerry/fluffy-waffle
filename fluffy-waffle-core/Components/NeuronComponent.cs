using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;
using fluffy_waffle_core;
using fluffy_waffle_core.Components;
using System.Windows.Input;

namespace fluffy_waffle_core
{
    public class NeuronComponent : ColorableComponent, IConnectable, ILeftMouseComponent
    {
        public double Value;
        public string Name;
        public List<IConnect> ConnectList { get; set; }
        public List<IConnect> ConnectedList { get; set; }

        public void InitControls(Panel panel, UIElement control, double value, String name)
        {
            base.InitControls(panel, control);
            Value = value;
            Name = name;

            Control.MouseLeftButtonDown += LeftMouseDown;
        }
        
        public new void Init()
        {
            base.Init();
            ConnectList = new List<IConnect>();
            ConnectedList = new List<IConnect>();
        }
        public void Connect(IConnectable target)
        {
            if (target is IGroup)
                throw new NotImplementedException();
            else
            {
                Line line = new Line()
                {
                    Stroke = Brushes.Black
                };
                
                SynapseComponent synapse = Parent.AddComponent<SynapseComponent>();
                synapse.InitControls(ParentPanel, line, this, target, 0);
                synapse.Init();
                ConnectList.Add(synapse);
                target.ConnectedList.Add(synapse);
            }
        }

        public bool IsConnected(IConnectable target)
        {
            foreach(IConnect connection in ConnectList)
            {
                if (connection.To == target)
                    return true;
            }
            return false;
        }

        public void LeftMouseDown(object sender, MouseEventArgs e)
        {
            if (Board.ClickedNeuron == null)
                Board.ClickedNeuron = this;
            else
            {
                Connect(Board.ClickedNeuron);
                Board.ClickedNeuron = null;
            }
        }
    }
}
