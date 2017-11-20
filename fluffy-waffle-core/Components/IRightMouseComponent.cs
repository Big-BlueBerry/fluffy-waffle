using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluffy_waffle_core.Components
{
    public interface IRightMouseComponent : IComponent
    {
        void RightMouseDown(object sender, System.Windows.Input.MouseEventArgs e);
    }
}
