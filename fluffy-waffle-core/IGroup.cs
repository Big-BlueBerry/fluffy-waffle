using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluffy_waffle_core
{
    public interface IGroup
    {
        List<IConnectable> MemberList { get; set; }

        void ExecuteAllMember();
    }
}
