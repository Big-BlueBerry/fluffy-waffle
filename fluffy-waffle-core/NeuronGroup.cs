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
        public Vector<double> NetworkValue { get; set; }
        public Vector<double> OutputValue { get; set; }
        public List<bool> IsActivated;

        public NeuronGroup()
        {
            Group = new List<Neuron>();
            IsActivated = new List<bool>();
        }

        public void AddNeuron(Neuron neuron)
        {
            Group.Add(neuron);
        }

        public Vector<double> GetGroupVector()
        {
            double[] vector = (from neuron in Group
                               select neuron.Value).ToArray();
            return DenseVector.OfArray(vector);
        }

        public void SetValue(Vector<double> passResult)
        {
            NetworkValue = passResult;
            ActivateNeuron(passResult);
        }
        private void ActivateNeuron(Vector<double> networkResult)
        {
            OutputValue = Sigmoid(networkResult);
        }

        private Vector<double> Sigmoid(Vector<double> vector)
        {
            return 1 / 1 + Vector.Exp(-vector);
        }
    }
}
