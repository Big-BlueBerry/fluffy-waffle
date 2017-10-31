using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace fluffy_waffle_core
{
    public interface IValuable
    {
        List<(IValuable, Branch)> ConnectList { get; set; }
        Vector<double> Value { get; set; }
        int Size { get; set; }

        void PassTo(IValuable target);
        bool Connect(IValuable target);
    }
}
