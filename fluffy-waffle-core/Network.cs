using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace fluffy_waffle_core
{
    public class Network
    {
        public List<IValuable> Layers;
        public List<Branch> Branches;

        public Network()
        {
            Layers = new List<IValuable>();
            Branches = new List<Branch>();
        }           

        public void AddLayer(IValuable layer)
        {
            Layers.Add(layer);
        }

        public void AddBranch(Branch branch)
        {
            Branches.Add(branch);
        }

        public void FowardPass()
        {
            for (int i = 0; i < Layers.Count - 1; i++)
            {
                Vector<double> outputVector = (i == 0) ? Layers[i].Value : Layers[i].OutputValue;
                outputVector = Branches[i].Weight.Transpose() * outputVector;
                Layers[i + 1].SetValue(outputVector);
                
            }            
        }

        private Vector<double> Sigmoid(Vector<double> vector)
        {
            return 1 / (1 + Vector<double>.Exp(-vector));

        }
        public void BackPropagation(Vector<double> Y)
        {
            Vector<double> output = Layers.Last().OutputValue;
            Vector<double> network = Layers.Last().NetworkValue;
            Layers.Last().Delta = (output - Y) * (Sigmoid(network) * (1 - Sigmoid(network)));

            for (int i = Branches.Count - 1; i > 0; i--)
            {
                Branches[i].BackPropagation(Layers[i].OutputValue, Layers[i + 1].Delta);
                Layers[i].SetDelta(Layers[i + 1].Delta, Branches[i].Weight);
            }
            Branches[0].BackPropagation(Layers[0].Value, Layers[1].Delta);
            UpdateValues();
        }

        private void UpdateValues()
        {
            foreach(Branch branch in Branches)
            {
                branch.WeightUpdate();
            }
        }
    }
}

