using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace fluffy_waffle_core
{
    /* 
     * layer(neuron group) 와 layer 사이에는 모든 bridge만 가능하다. 
     * bridge에서 branch와 행렬인 layer와 layer를 연결하는 가중치 값 Weight를 가지고 있다.
     */ 
    public class Bridge
    {
        public Branch[,] Branches { get; set; }
        public Matrix<double> Weights { get; set; }

        public void BuildBridge(NeuronGroup first, NeuronGroup second)
        {
            Branches = new Branch[first.Group.Count, second.Group.Count];
            double[,] weights = new double[first.Group.Count, second.Group.Count];
            for (int i = 0; i < first.Group.Count; i++)
            {
                for (int j = 0; j < second.Group.Count; j++)
                {
                    Branch branch = new Branch(first.Group[i], first.Group[j]);
                    Branches[i, j] = branch;
                    weights[i, j] = branch.Weight;
                }
            }
            Weights = DenseMatrix.OfArray(weights);
        }

        public Vector<double> CrossBridge(Vector<double> layer)
        {
            return layer * Weights;
        }
    }
}
