using Collections.Abstractions;

namespace Editor.Tests.Collections.TestEntities
{
    /// <summary>
    /// It has the same behavior as a Container. It was created because of the inability to change the Container
    /// by adding an ability to set the linked list manually (without the reflection). It's used in test cases only
    /// </summary>
    public class DoubleLinkedCollection : IDataAccessProvider
    {
        private Node _current;
        
        public bool Value
        {
            get { return _current.Value; }
            set { _current.Value = value; }
        }
        
        public DoubleLinkedCollection(bool[] list)
        {
            Node prev = null;
            for (int i = 0; i < list.Length; i++)
            {
                var currentNode = new Node(prev, list[i]);
                if (prev != null)
                {
                    prev.Next = currentNode;
                }

                if (_current == null)
                {
                    _current = currentNode;
                }

                prev = currentNode;
            }

            prev.Next = _current;
            _current.Prev = prev;
        }

        public void MoveForward()
        {
            _current = _current.Next;
        }

        public void MoveBackward()
        {
            _current = _current.Prev;
        }
    }
}