using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluffy_waffle_core
{
    public class Branch
    {
        public double Weight;

        public Branch()
        {
            Weight = new Random().NextDouble();
        }

        public void Link(IConnectable from, IConnectable target)
        {
            throw new NotImplementedException();
        }
    }
}
