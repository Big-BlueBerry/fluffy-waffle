using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluffy_waffle_core
{
    public interface IConnectable
    {
        void Connect(IConnectable target);
        bool IsConnected(IConnectable target);
    }
}
