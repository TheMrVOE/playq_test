namespace Collections.Abstractions
{
    public interface IDataAccessProvider
    {
        bool Value { get; }
        
        void MoveForward();
        void MoveBackward();
    }
}