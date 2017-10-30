using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluffy_waffle_core
{
    interface IConnectable
    {
        bool Connect(IValuable from, IValuable to);
    }
}
