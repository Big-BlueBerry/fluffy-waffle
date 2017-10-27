using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace fluffy_waffle_core
{
    public class Network
    {
        public List<NeuronGroup> Layers;
        public List<Bridge> Bridges;

        public Network()
        {
            Layers = new List<NeuronGroup>();
            Bridges = new List<Bridge>();
        }           

        public void AddLayer(NeuronGroup layer)
        {
            Layers.Add(layer);
        }

        public void AddBridge(Bridge bridge)
        {
            Bridges.Add(bridge);
        }

        public void Build()
        {
            for (int i = 0; i < Layers.Count - 1; i++)
            {
                Bridges[i].BuildBridge(Layers[i], Layers[i + 1]);
            }
        }

        public NeuronGroup GetPredictLayer()
        {
            return Layers.Last();
        }

        public void FowardPass()
        {
            for (int i = 0; i < Layers.Count - 1; i++)
            {
                Vector<double> nextLayerVector = i == 0 ? Layers[i].GetGroupVector() : Layers[i].OutputValue;
                nextLayerVector = Bridges[i].CrossBridge(nextLayerVector);
                Layers[i + 1].SetValue(nextLayerVector);
            }            
        }

        public void BackPropagation(Vector<double> Y)
        {
            Vector<double> output = Layers.Last().OutputValue;
            Vector<double> error = Y - output;
            Vector<double> lastDelta = error * output * (1 - output);
            Layers.Last().Delta = lastDelta;

            Bridges[1].BackPropagation(Layers[1].OutputValue, lastDelta);
            Layers[1].SetDelta(Layers[2].Delta, Bridges[1].Weights);
        }
    }
}
