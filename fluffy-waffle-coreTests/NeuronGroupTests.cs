using Microsoft.VisualStudio.TestTools.UnitTesting;
using fluffy_waffle_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using Vector = System.Windows.Vector;


namespace fluffy_waffle_core.Tests
{
    [TestClass()]
    public class NeuronGroupTests
    {
        [TestMethod()]
        public void SimulatorTest()
        {
            Network model = new Network();
            NeuronGroup group1 = new NeuronGroup();
            group1.AddNeuron(new Neuron(new Vector(100, 100)));
            group1.AddNeuron(new Neuron(new Vector(200, 200)));

            NeuronGroup group2 = new NeuronGroup();
            group2.AddNeuron(new Neuron(new Vector(300, 300)));
            group2.AddNeuron(new Neuron(new Vector(400, 400)));

            NeuronGroup group3 = new NeuronGroup();
            group3.AddNeuron(new Neuron(new Vector(500, 500)));
            group3.AddNeuron(new Neuron(new Vector(600, 600)));

            model.AddLayer(group1);
            model.AddLayer(group2);
            model.AddLayer(group3);
            model.AddBridge(new Bridge());
            model.AddBridge(new Bridge());

            model.Build();
            model.FowardPass();
            model.BackPropagation(DenseVector.OfArray(new Double[] { 4, 5 }));
            //NeuronGroup last = model.GetPredictLayer();
            //Vector<double> predict = last.InputValue;
            //double[] y = new double[] { 4 };

        }
    }
}