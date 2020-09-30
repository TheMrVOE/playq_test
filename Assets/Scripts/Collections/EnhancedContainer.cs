using Collections.Abstractions;
using Collections.Adapters;

namespace Collections
{
    /// <summary>
    /// Allocation-free enhanced version of Container 
    /// </summary>
    public class EnhancedContainer : IEnhancedContainer
    {
        /// <summary>
        /// Stores the index of the beginning of the next sequence
        /// </summary>
        private int? _firstMatchIndex;

        /// <summary>
        /// Used to cache the first arrays value
        /// </summary>
        private  bool _firstItemValue;

        /// <summary>
        /// The cursor position in the sequence
        /// </summary>
        private int _cursorPosition;

        /// <summary>
        /// Stores the reference to data provider
        /// </summary>
        private IDataAccessProvider _dataProvider;

        public int Count => _firstMatchIndex ?? (_firstMatchIndex = CalculateLength()).Value;

        public bool Value
        {
            get => _dataProvider.Value;
            set => _dataProvider.Value = value;
        }

        public void MoveForward()
            => _dataProvider.MoveForward();

        public void MoveBackward()
            => _dataProvider.MoveBackward();

        /// <param name="itemsCount">Items count to generate randomly</param>
        public EnhancedContainer(int itemsCount = 0)
        {
            _dataProvider = new ContainerAccessAdapter(itemsCount);
        }

        /// <param name="accessProvider">Custom container data provider</param>
        public EnhancedContainer(IDataAccessProvider accessProvider)
        {
            _dataProvider = accessProvider;
        }

        public int CalculateLength()
        {
            _firstItemValue = _dataProvider.Value;
            return ProcessNextElement();
        }

        /// <summary>
        /// Moves the cursor to the next item and tries to find the number of items
        /// </summary>
        /// <returns>The number of items in container</returns>
        private int ProcessNextElement()
        {
            MoveCursorForward();

            Value = !Value;
            var previousValue = GetPreviousItem(_cursorPosition);
            if (previousValue != _firstItemValue)
            {
                Value = !Value;
                return _cursorPosition;
            }

            Value = !Value;

            return ProcessNextElement();
        }

        private void MoveCursorForward()
        {
            _cursorPosition++;
            _dataProvider.MoveForward();
        }


        /// <summary>
        /// Returns an item by moving the cursor backward to the target item
        /// </summary>
        /// <param name="steps">Quantity of steps to go back to the target item</param>
        /// <returns>The value of the item</returns>
        private bool GetPreviousItem(int steps)
        {
            //I've decided to move the cursor backward and forward (like in Turing machine)
            //instead of using caching by using a list which leads to memory allocation
            MoveBackward(steps);
            var value = _dataProvider.Value;
            MoveForward(steps);

            return value;
        }

        /// <summary>
        /// Moves cursor backward to the target index item using steps
        /// </summary>
        /// <param name="stepsCount">Quantity of steps that are used to move backward to the target item</param>
        private void MoveBackward(int stepsCount)
        {
            for (int i = 0; i < stepsCount; i++)
                _dataProvider.MoveBackward();
        }

        /// <summary>
        /// Moves the cursor forward to the target index item using steps
        /// </summary>
        /// <param name="stepsCount">Quantity of steps that are used to move forward to the target item</param>
        private void MoveForward(int stepsCount)
        {
            for (int i = 0; i < stepsCount; i++)
                _dataProvider.MoveForward();
        }
    }
}