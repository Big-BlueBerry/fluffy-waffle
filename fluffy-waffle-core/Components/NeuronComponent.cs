using System;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;
using fluffy_waffle_core;
using fluffy_waffle_core.Components;

namespace fluffy_waffle_core
{
    public class NeuronComponent : ColorableComponent, IConnectable
    {
        public double Value;
        public string Name;
        public List<IConnect> ConnectList = new List<IConnect>();

        CompObject IComponent.Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    
        public void InitControls(Panel panel, UIElement control, double value, String name)
        {
            base.InitControls(panel, control);
            Value = value;
            Name = name;
        }
        
        public void Connect(IConnectable target)
        {
            SynapseComponent synapse = Parent.AddComponent<SynapseComponent>();
            synapse.InitControls(ParentPanel, Control, this, target, 0);
            ConnectList.Add(synapse);
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
    }
}
