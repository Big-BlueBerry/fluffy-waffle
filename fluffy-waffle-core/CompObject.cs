using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluffy_waffle_core
{
    public abstract class CompObject
    {
        public Board Board;
        public string Name { get; set; }
        private List<Component> _components;

        public CompObject(Board board, string name)
        {
            Board = board;
            Name = name;
            _components = new List<Component>();
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (var comp in _components)
            {
                if (comp is T)
                    return comp as T;
            }

            return null;
        }

        public T AddComponent<T>() where T : Component, new()
        {
            if (Attribute.GetCustomAttribute(typeof(T), typeof(SingleComponent)) != null)
                if (_components.Any(c => c is T))
                    throw new ArgumentException("SingleComponent can't be duplicated");

            return AddComponent(new T()) as T;
        }

        protected Component AddComponent(Component t)
        {
            t.Parent = this;
            _components.Add(t);
            return t;
        }

        public void RemoveAllComponents<T>() where T : Component
        {
            _components.RemoveAll(comp => comp is T);
        }
    }
}
