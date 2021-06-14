namespace ContainerResolveTests.ServicesForTests
{
    public class ComplexService<T> where T : IDone
    {
        private readonly ServiceWithOne<T> _serviceWithOne;
        private readonly IDone _done;
        private readonly SimpleService _simpleService;
        public bool Done => _done.Done() && _simpleService.Done() && _serviceWithOne.Done;

        public ComplexService(ServiceWithOne<T> serviceWithOne, IDone done, SimpleService simpleService)
        {
            _done = done;
            _simpleService = simpleService;
            _serviceWithOne = serviceWithOne;
        }
    }
}