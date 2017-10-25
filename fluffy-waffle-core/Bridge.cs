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

        public void BuildBridge(NeuronGroup first, NeuronGroup second)
        {
            Branches = new Branch[first.Group.Count, second.Group.Count];
            Weights = new double[first.Group.Count, second.Group.Count];
            for (int i = 0; i < first.Group.Count; i++)
            {
                for (int j = 0; j < first.Group.Count; j++)
                {
                    Branch branch = new Branch(first.Group[i], first.Group[j]);
                    Branches[i, j] = branch;
                    Weights[i, j] = branch.Weight;
                }
            }
        }
    }
}
