using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ContainerExample
{
 public class Container
    {
        private Dictionary<Type, ResolveActor> Types;

        public Container(Dictionary<Type, ResolveActor> types)
        {
            Types = types;
        }

        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }

        public object Resolve(Type T)
        {
            if (Types.TryGetValue(T, out var resolveActor) == false)
            {
                throw new NoRegistrationException(
                    $"type of {T} is not registered {Environment.NewLine}{string.Join(Environment.NewLine, new StackTrace(true).GetFrames().Take(5).Select(f => $"{f.GetFileName()} => {f.GetMethod()} => {f.GetFileLineNumber()}"))}");
            }

            var item = resolveActor.Resolve(this);
            if (item != null)
            {
                return item;
            }
            
            return Activator.CreateInstance(resolveActor.Type, resolveActor?.ParameterInfo.Select(t => Resolve(t.ParameterType)).ToArray());
        }
    }
}