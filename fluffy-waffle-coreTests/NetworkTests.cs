using Microsoft.VisualStudio.TestTools.UnitTesting;
using fluffy_waffle_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using Vector = System.Windows.Vector;
using System.Diagnostics;

namespace fluffy_waffle_core.Tests
{
    [TestClass()]
    public class NetworkTests
    {
        [TestMethod()]
        public void BackPropagationTest()
        {

            Network model = new Network();
            NeuronGroup group1 = new NeuronGroup();
            group1.AddNeuron(new Neuron(new Vector(100, 100)));
            group1.AddNeuron(new Neuron(new Vector(200, 200)));

            Neuron group2 = new Neuron(new Vector(500, 500));

            Branch branch1 = group1.Connect(group2);

            model.AddLayer(group1);
            model.AddLayer(group2);
            model.AddBranch(branch1);
            
            model.FowardPass();
            Debug.WriteLine(model.Layers[0].NetworkValue);
            Debug.WriteLine(model.Layers[1].NetworkValue);
            model.BackPropagation(DenseVector.OfArray(new Double[] { 3 }));
            model.FowardPass();
            Debug.WriteLine(model.Layers[0].NetworkValue);
            Debug.WriteLine(model.Layers[1].NetworkValue);
        }
    }
}