namespace Collections.Abstractions
{
    public interface IEnhancedContainer : IDataAccessProvider
    {
        int Count { get; }
    }
}