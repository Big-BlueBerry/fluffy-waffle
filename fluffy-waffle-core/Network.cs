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
                Bridges[i].BuildBridge(Layers[i - 1], Layers[i]);
                Vector<double> nextLayerVector = i == 0 ? Layers[i - 1].GetGroupVector() : Layers[i - 1].OutputValue;
                nextLayerVector = Bridges[i].CrossBridge(nextLayerVector);
            }
        }

        public NeuronGroup GetPredictLayer()
        {
            return Layers.Last();
        }

        public void FowardPass()
        {
            throw new NotImplementedException();
        }

        public void Propagation()
        {
            throw new NotImplementedException();
        }
    }
}
