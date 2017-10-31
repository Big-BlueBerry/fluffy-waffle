using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluffy_waffle_core
{
    public interface IConnectable
    {
        IValuable From { get; set; }
        IValuable To { get; set; }

        void Connect(IValuable from, IValuable to);
    }
}
