using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluffy_waffle_core
{
    public class Bridge
    {
        public Branch[,] Branches { get; set; }
        public Double[,] Weights { get; set; }

        public void ConnectGroup(NeuronGroup first, NeuronGroup second)
        {
            throw new NotImplementedException();
        }
    }
}
