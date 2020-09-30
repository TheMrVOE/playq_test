namespace Collections.Abstractions
{
    public interface IDataAccessProvider
    {
        bool Value { get; set; }
        
        void MoveForward();
        void MoveBackward();
    }
}