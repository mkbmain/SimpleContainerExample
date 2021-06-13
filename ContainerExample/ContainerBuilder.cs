using System.Collections.Generic;
using System.Linq;

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

        public Container Build =>
            new Container(ContainerActors.GroupBy(f => f.FromType).ToDictionary(f => f.Key, f => f.First()));
    }
}