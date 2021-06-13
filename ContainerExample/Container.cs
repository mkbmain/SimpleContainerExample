using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ContainerExample
{
 public class Container
    {
        private Dictionary<Type, ContainerActor> Types;

        public Container(Dictionary<Type, ContainerActor> types)
        {
            Types = types;
        }

        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }

        public object Resolve(Type T)
        {
            if (Types.TryGetValue(T, out var details) == false)
            {
                throw new NoRegistrationException(
                    $"type of {T} is not registered {Environment.NewLine}{string.Join(Environment.NewLine, new StackTrace(true).GetFrames().Take(5).Select(f => $"{f.GetFileName()} => {f.GetMethod()} => {f.GetFileLineNumber()}"))}");
            }

            var item = details.Resolve(this);
            if (item != null)
            {
                return item;
            }
            
            return Activator.CreateInstance(details.Type, details?.ParamaterInfo(Types).Select(t => Resolve(t.ParameterType)).ToArray());
        }
    }
}