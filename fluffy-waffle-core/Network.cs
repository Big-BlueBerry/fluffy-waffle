using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluffy_waffle_core
{
    public class Network
    {
        public List<NeuronGroup> Layers;
        public List<Bridge> Bridges;

        public Network()
        {
        }

        public void AppendLayer(NeuronGroup layer)
        {
            Layers.Add(layer);
        }

        public void AppendBridge(Bridge bridge)
        {
            Bridges.Add(bridge);
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
