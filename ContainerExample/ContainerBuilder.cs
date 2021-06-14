using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ContainerExample
{
    public class ContainerBuilder
    {
        private List<ContainerActor> ContainerActors = new List<ContainerActor>();

        public ContainerActor<T> Register<T>()
        {
            var actor = new ContainerActor<T>(typeof(T));
            ContainerActors.Add(actor);
            return actor;
        }

        public Container Build => Make();
 
        private Container Make()
        {
            var lookupDict = ContainerActors.GroupBy(f => f.FromType).ToDictionary(f => f.Key, f => f.First());
            foreach (var actor in ContainerActors)
            {
                actor.ParameterInfo = ParamaterInfo(actor.Type, lookupDict).ToArray();
            }
            return new Container(lookupDict);
        }

        private IEnumerable<ParameterInfo> ParamaterInfo(Type type, Dictionary<Type, ContainerActor> definedTypes)
        {
            var constructorInfos = type.GetConstructors();

            return constructorInfos.FirstOrDefault(f =>
                    f.GetParameters().Length == 0 ||
                    f.GetParameters().Any(t => definedTypes.ContainsKey(t.ParameterType)))
                ?.GetParameters();
        }
    }
}