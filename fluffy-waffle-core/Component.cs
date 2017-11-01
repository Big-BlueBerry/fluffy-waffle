using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluffy_waffle_core
{
    public abstract class Component
    {
        public CompObject Parent { get; set; }

        public Component()
        {

        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class SingleComponent : Attribute { }

    [SingleComponent]
    public class MoveCameraComponent : Component
    {
        ///
    }

}
