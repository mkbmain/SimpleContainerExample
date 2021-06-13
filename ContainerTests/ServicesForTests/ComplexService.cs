namespace ContainerResolveTests.ServicesForTests
{
    public class ComplexService<T> where T : IDone
    {
        private ServiceWithOne<T> _serviceWithOne;
        private IDone _done;
        private SimpleService _simpleService;
        public bool Done => _done.Done() && _simpleService.Done() && _serviceWithOne.Done;

        public ComplexService(ServiceWithOne<T> serviceWithOne, IDone done, SimpleService simpleService)
        {
            _done = done;
            _simpleService = simpleService;
            _serviceWithOne = serviceWithOne;
        }
    }
}