using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;

namespace fluffy_waffle_core
{
    /*
     * 모든 뉴런들은 그룹화 되어서 사용된다.
     * 하나의 뉴런일지라도 그룹화 돼야함.
     * 이 뉴런 그룹을 통해 다른 그룹 혹은 Bridge과 상호작용한다.
     */
    public class NeuronGroup
    {
        public List<Neuron> Group { get; set; }
        public Double NetworkValue { get; set; }
        public Double OutputValue { get; set; }

        public NeuronGroup()
        {
            Group = new List<Neuron>();
            NetworkValue = 0;
            OutputValue = 0;
        }

        public void AddNeuron(Neuron neuron)
        {
            Group.Add(neuron);
        }

        public Matrix<double> GetGroupVector()
        {
            double[] vector = (from neuron in Group
                               select neuron.Value).ToArray();
            int size = vector.Length;
            double[,] matrix = new double[1, size];
            for (int i = 0; i < size; i++) matrix[0, i] = vector[i];
            return DenseMatrix.OfArray(matrix);
        }

        public void ActivateNeuron()
        {
            throw new NotImplementedException();
        }
    }
}
