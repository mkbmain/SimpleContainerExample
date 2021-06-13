namespace ContainerResolveTests.ServicesForTests
{
    public class ServiceWithOne<T> where T : IDone
    {
        private readonly T _simpleService;
        public bool Done => _simpleService.Done();

        public ServiceWithOne(T simpleService)
        {
            _simpleService = simpleService;
        }
    }
}