using System;
using ContainerExample;

namespace Example
{
    class Program
    {
        // bit more visual than the tests of how to use si
        static void Main(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.Register<Test>();
            cb.Register<boom>();
            cb.Register<oo>().AsImplementedInterface();
            cb.Register<oo1>().Resolve(f => new oo1("test", f.Resolve<o>(),f.Resolve<boom>()));
            var con = cb.Build;
            var t = con.Resolve<Test>();
            var tw = con.Resolve<oo1>();
        }
    }

    public class Test
    {
        public Test(o item)
        {
            item.Print();
        }
    }

    public interface o
    {
        public void Print();
    }

    public class oo : o
    {
        void o.Print()
        {
            Console.WriteLine("done");
        }
    }

    public class oo1
    {
        public oo1(string name, o item,boom t)
        {
            item.Print();
            Console.WriteLine(name);
        }
    }

    public class boom
    {
        public boom()
        {
            Console.Write("Boom");
        }
    }
}