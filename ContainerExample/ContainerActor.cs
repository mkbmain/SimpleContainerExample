using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        private ParameterInfo[] _parameterInfos = null;
        public IEnumerable<ParameterInfo> ParamaterInfo(Dictionary<Type,ContainerActor> definedTypes)
        {
            if (_parameterInfos != null)
            {
                return _parameterInfos;
            }
            var constructorInfos = Type.GetConstructors();

            _parameterInfos = constructorInfos.FirstOrDefault(f =>
                f.GetParameters().Length == 0 || f.GetParameters().Any(t => definedTypes.ContainsKey(t.ParameterType)))?.GetParameters();
           
            return _parameterInfos;
        }
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