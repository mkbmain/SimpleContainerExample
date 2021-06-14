using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ContainerExample
{
    public abstract class ResolveActor
    {
        public ResolveActor(Type T)
        {
            FromType = T;
            Type = T;
        }

        internal Type FromType;
        internal Type Type;

        public ParameterInfo[] ParameterInfo = null;

        public virtual object Resolve(Container container)
        {
            return null;
        }
        
        public void As<T>()
        {
            FromType = typeof(T);
        }
  

        public void AsImplementedInterface()
        {
            FromType = FromType.GetInterfaces().First();
        }
    }

    public class ResolveActor<T> : ResolveActor
    {
        public Func<Container, T> ResolveFunc = null;

        public override object Resolve(Container container)
        {
            return ResolveFunc != null ? (object) ResolveFunc(container) : null;
        }

        public void Resolve(Func<Container, T> func)
        {
            ResolveFunc = func;
        }

        public ResolveActor(Type T) : base(T)
        {
        }
    }
}