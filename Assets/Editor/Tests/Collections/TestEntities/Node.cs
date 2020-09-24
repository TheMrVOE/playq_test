namespace Editor.Tests.Collections.TestEntities
{
    /// <summary>
    /// This class was created because if encapsulation of the Container's Node class
    /// </summary>
    public class Node
    {
        public Node Next;
        public Node Prev;
        public bool Value;
        
        public Node(Node prev, bool value)
        {
            Value = value;
            Prev = prev;
        }
    }
}