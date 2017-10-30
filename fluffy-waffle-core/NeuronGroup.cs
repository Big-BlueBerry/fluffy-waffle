using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace fluffy_waffle_core
{
    /*
     * 모든 뉴런들은 그룹화 되어서 사용된다.
     * 하나의 뉴런일지라도 그룹화 돼야함.
     * 이 뉴런 그룹을 통해 다른 그룹 혹은 Bridge과 상호작용한다.
     * network value는 activate하기 전 값이고, 미분할 때 편하게 하기 위해서 생성하였다.
     */
    public class NeuronGroup
    {
        public List<Neuron> Group { get; set; }
        public Vector<double> NetworkValue { get; set; }
        public Vector<double> OutputValue { get; set; }
        public Vector<double> Delta { get; set; }
        public List<bool> IsActivated;

        public NeuronGroup()
        {
            Group = new List<Neuron>();
            IsActivated = new List<bool>();
            NetworkValue = null;
            OutputValue = null;
            Delta = null;
        }

        public void AddNeuron(Neuron neuron)
        {
            neuron.SetColor(Colors.Yellow);
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

            for (int i = 0; i < Group.Count; i++)
            {
                Group[i].NetworkValue = NetworkValue[i];
                Group[i].OutputValue = OutputValue[i];
            }

            SetNeuronValue();
        }

        private void SetNeuronValue()
        {
            for(int i = 0; i < OutputValue.Count; i++)
            {
                Group[i].OutputValue = OutputValue[i];
            }
        }

        private void ActivateNeuron(Vector<double> networkResult)
        {
            OutputValue = Sigmoid(networkResult);
        }

        private Vector<double> Sigmoid(Vector<double> vector)
        {
            return 1 / (1 + Vector.Exp(-vector));
        }

        public void SetDelta(Vector<double> nextLayerDelta, Matrix<double> weights)
        {
            Matrix<double> transWeights = weights.Transpose();
            Delta = (nextLayerDelta * transWeights) * (Sigmoid(NetworkValue) * (1 - Sigmoid(NetworkValue)));
        }
    }
}
