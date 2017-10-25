using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluffy_waffle_core
{
    /*
     * 모든 뉴런들은 그룹화 되어서 사용된다.
     * 하나의 뉴런일지라도 그룹화 돼야함.
     * 이 뉴런 그룹을 통해 다른 그룹 혹은 Bridge과 상호작용한다.
     */
    public class NeuronGroup
    {
        private List<Neuron> _group { get; set; }
        public Double NetworkValue { get; set; }
        public Double OutputValue { get; set; }

        public void AppendNeuron(Neuron neuron)
        {
            _group.Add(neuron);
        }

        public Double[] GetGroupVector()
        {
            var vector = from neuron in _group
                       select neuron.Value;
            return vector.Select(x => (double)x).ToArray();
        }

        public void ActivateNeuron()
        {
            throw new NotImplementedException();
        }
    }
}
