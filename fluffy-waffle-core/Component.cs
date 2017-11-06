using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluffy_waffle_core
{
    public interface IComponent
    {
        CompObject Parent { get; set; }
        void Init();
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class SingleComponent : Attribute { }
}
