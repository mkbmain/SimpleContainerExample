using System;
using System.Linq;

namespace ContainerExample
{
    public abstract class ContainerActor
    {
        public ContainerActor(Type T)
        {
            FromType = T;
            Type = T;
        }

        internal Type FromType;
        internal Type Type;

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

    public class ContainerActor<T> : ContainerActor
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

        public ContainerActor(Type T) : base(T)
        {
        }
    }
}