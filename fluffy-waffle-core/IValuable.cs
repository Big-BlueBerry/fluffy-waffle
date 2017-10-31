using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluffy_waffle_core
{
    public interface IValuable
    {
        List<IValuable> ConnectList { get; set; }

        void PassTo(IValuable target);
        bool Connect(IValuable target);
    }
}
