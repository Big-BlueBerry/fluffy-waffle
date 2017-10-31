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
        Vector<double> Value { get; set; }
        Vector<double> NetworkValue { get; set; }
        Vector<double> OutputValue { get; set; }
        Vector<double> Delta { get; set; }

        int Size { get; set; }

        void PassTo(IValuable target, Branch branch);
        void SetDelta(Vector<double> nextLayerDelta, Matrix<double> weights);
        void SetValue(Vector<double> passResult);
        Branch Connect(IValuable target);
    }
}
