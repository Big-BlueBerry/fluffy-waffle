using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace fluffy_waffle_core.Components
{
    public class StepComponent : IStepable, IRightMouseComponent
    {
        public CompObject Parent { get; set; }
        public UIElement Control;

        public void InitControls(UIElement control)
        {
            Control = control;
        }

        public void Init()
        {
            Control.MouseRightButtonDown += RightMouseDown;
        }

        public void RightMouseDown(object sender, MouseEventArgs e)
        {
            Step();
        }

        // 영찬이가 코드 입력받아서 step 런타임에서 바꿔주는거 하겟지 ....
        public void Step()
        {
            NeuronComponent neuron = Parent.GetComponent<NeuronComponent>();
            if (neuron == null)
                return;

            foreach(SynapseComponent synapse in neuron.ConnectList)
            {
                if(synapse.To is NeuronComponent)
                    ((NeuronComponent)synapse.To).Value += synapse.Weight * neuron.Value;
            }
        }
    }
}
