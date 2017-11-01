using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluffy_waffle_core
{
    public abstract class Board
    {
        private List<CompObject> _compObjects;

        public Board()
        {
            _compObjects = new List<CompObject>();
        }

        public CompObject FindCompObjectByName(string name)
            => _compObjects.Find(c => c.Name == name);
    }
}
