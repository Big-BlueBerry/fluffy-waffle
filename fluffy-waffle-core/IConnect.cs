using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluffy_waffle_core
{
    public interface IConnect : IComponent
    {
        IConnectable From { get; set; }
        IConnectable To { get; set; }

        void Connect(IConnectable from, IConnectable to);
    }
}
