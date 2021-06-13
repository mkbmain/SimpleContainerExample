using ContainerExample;
using ContainerResolveTests.ServicesForTests;
using Shouldly;
using Xunit;

namespace ContainerResolveTests
{
    public class ResolveTests
    {
        [Fact]
        public void Ensure_we_can_resolve_a_simple_service()
        {
            var cb = new ContainerBuilder();
            cb.Register<SimpleService>();
            cb.Build.Resolve<SimpleService>().Done().ShouldBeTrue();
        }


        [Fact]
        public void Ensure_we_can_resolve_a_service_with_one_Simple_service()
        {
            var cb = new ContainerBuilder();
            cb.Register<SimpleService>();
            cb.Register<ServiceWithOne<SimpleService>>();
            cb.Build.Resolve<ServiceWithOne<SimpleService>>().Done.ShouldBeTrue();
        }

        [Fact]
        public void Ensure_we_can_resolve_a_service_with_interface_of_a_service()
        {
            var cb = new ContainerBuilder();
            cb.Register<SimpleService>().AsImplementedInterface();
            cb.Register<ServiceWithOne<IDone>>();
            cb.Build.Resolve<ServiceWithOne<IDone>>().Done.ShouldBeTrue();
        }

        [Fact]
        public void Ensure_we_can_resolve_a_service_with_interface_of_a_service_using_as()
        {
            var cb = new ContainerBuilder();
            cb.Register<SimpleService>().As<IDone>();
            cb.Register<ServiceWithOne<IDone>>();
            cb.Build.Resolve<ServiceWithOne<IDone>>().Done.ShouldBeTrue();
        }

        [Fact]
        public void Ensure_we_can_resolve_a_complex_service()
        {
            var cb = new ContainerBuilder();
            cb.Register<SimpleService>().AsImplementedInterface();
            cb.Register<SimpleService>();
            cb.Register<ComplexService<IDone>>();
            cb.Register<ServiceWithOne<IDone>>();
            cb.Build.Resolve<ComplexService<IDone>>().Done.ShouldBeTrue();
        }


        [Fact]
        public void Ensure_our_exceptions_are_readable()
        {
            var cb = new ContainerBuilder();

            var exception = Should.Throw<NoRegistrationException>(() => cb.Build.Resolve<ComplexService<IDone>>().Done);
            exception.Message.ShouldStartWith("type of ContainerResolveTests.ServicesForTests.ComplexService`1[ContainerResolveTests.ServicesForTests.IDone] is not registered ");
        }
    }
}