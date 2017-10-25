using Microsoft.VisualStudio.TestTools.UnitTesting;
using fluffy_waffle_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MathNet.Numerics.LinearAlgebra;

namespace fluffy_waffle_core.Tests
{
    [TestClass()]
    public class NeuronGroupTests
    {
        [TestMethod()]
        public void SimulatorTest()
        {
            NeuronGroup group1 = new NeuronGroup();
            group1.AddNeuron(new Neuron(new Vector(10, 10)));
            group1.AddNeuron(new Neuron(new Vector(200, 200)));

            NeuronGroup group2 = new NeuronGroup();
            group2.AddNeuron(new Neuron(new Vector(300, 300)));

            Bridge bridge = new Bridge();
            bridge.BuildBridge(group1, group2);
            Vector<double> nextVector = bridge.CrossBridge(group1.GetGroupVector());
            group2.SetValue(nextVector);
        }
    }
}