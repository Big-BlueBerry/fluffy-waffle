using Microsoft.VisualStudio.TestTools.UnitTesting;
using fluffy_waffle_core;
using fluffy_waffle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace fluffy_waffle_core.Components.Tests
{
    [TestClass()]
    public class StepComponentTests
    {
        MouseEventHandleCompObject object1, object2;

        [TestMethod()]
        public void StepTest()
        {
            InitNeuron(1, 0);

            object1.GetComponent<NeuronComponent>().Connect(object2.GetComponent<IConnectable>());
            object1.AddComponent<StepComponent>().Step();

            NeuronComponent neuron = object1.GetComponent<NeuronComponent>();
            SynapseComponent synapse = (SynapseComponent)neuron.ConnectList[0];
            Assert.AreEqual(neuron.Value * synapse.Weight, object2.GetComponent<NeuronComponent>().Value);
        }

        void InitNeuron(double firstValue, double secondValue)
        {
            Board board = new Board();
            Canvas panel = new Canvas();

            Ellipse ellipse = new Ellipse()
            {
                Width = 100,
                Height = 100
            };

            object1 = new MouseEventHandleCompObject(ellipse, board, "test1");
            NeuronComponent comp1 = object1.AddComponent<NeuronComponent>();
            comp1.InitControls(panel, ellipse, firstValue, "neuron1");
            comp1.Pos = new Vector(30, 30);

            object1.InitAllComponents();

            Ellipse ellipse1 = new Ellipse()
            {
                Width = 50,
                Height = 50
            };

            object2 = new MouseEventHandleCompObject(ellipse1, board, "test2");
            NeuronComponent comp2 = object2.AddComponent<NeuronComponent>();
            comp2.InitControls(panel, ellipse1, secondValue, "neuron2");
            comp2.Pos = new Vector(300, 300);
            object2.InitAllComponents();
        }
    }
}