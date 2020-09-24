using Collections.Abstractions;

namespace Collections.Adapters
{
    /// <summary>
    /// A simple Container API wrapper. Generally, it's created to be able to cover the counter
    /// algorithm implementation with the tests
    /// </summary>
    public class ContainerAccessAdapter : IDataAccessProvider
    {
        private readonly Container _container;
        
        public bool Value => _container.Value;

        public ContainerAccessAdapter(int count)
        {
            _container = new Container(count);
        }

        public void MoveForward()
        {
            _container.MoveForward();
        }

        public void MoveBackward()
        {
            _container.MoveBackward();
        }
    }
}