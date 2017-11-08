using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace fluffy_waffle_core
{
    public abstract class CompObject
    {
        public UIElement Control;
        public Board Board;
        public string Name { get; set; }
        private List<IComponent> _components;

        public CompObject(UIElement control, Board board, string name)
        {
            Control = control;
            Board = board;
            Name = name;
            _components = new List<IComponent>();
        }

        public T GetComponent<T>() where T : class, IComponent
        {
            foreach (var comp in _components)
            {
                if (comp is T)
                    return (T)comp;
            }

            return null;
        }

        public T AddComponent<T>() where T : class, IComponent, new()
        {
            if (Attribute.GetCustomAttribute(typeof(T), typeof(SingleComponent)) != null)
                if (_components.Any(c => c is T))
                    throw new ArgumentException("SingleComponent can't be duplicated");

            return AddComponent(new T()) as T;
        }

        protected IComponent AddComponent(IComponent t)
        {
            t.Parent = this;
            _components.Add(t);
            return t;
        }

        public void RemoveAllComponents<T>() where T : IComponent
        {
            _components.RemoveAll(comp => comp is T);
        }

        public void InitAllComponents()
        {
            foreach (IComponent component in _components)
                component.Init();
        }
    }
}
