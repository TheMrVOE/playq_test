using Collections.Abstractions;
using Collections.Adapters;

namespace Collections
{
    /// <summary>
    /// Allocation-free enhanced version of Container 
    /// </summary>
    public class EnhancedContainer : IEnhancedContainer
    {
        private readonly int _precision;

        /// <summary>
        /// Stores the index of the beginning of the next sequence
        /// </summary>
        private int? _firstMatchIndex;
        /// <summary>
        /// Used to define how many items match in the sub-sequence
        /// </summary>
        private int _matchItems;
        /// <summary>
        /// Defines how many same combinations were found in the sequence
        /// </summary>
        private int _totalMachCount;
        /// <summary>
        /// The cursor position in the sequence
        /// </summary>
        private int _cursorPosition;

        /// <summary>
        /// Stores the reference to data provider
        /// </summary>
        private IDataAccessProvider _dataProvider;

        public int Count => _firstMatchIndex ?? (_firstMatchIndex = ProcessNextElement()).Value;
        public bool Value => _dataProvider.Value;
        
        /// <param name="precision">As there is no way to calculate the size of each case container,
        /// it's needed to add a precision parameter. The bigger number the better precision.
        /// It tells to the counter what is the maximum of same item combinations can be put in the container</param>
        /// <param name="itemsCount">Items count to generate randomly</param>
        public EnhancedContainer(int precision, int itemsCount = 0)
        {
            _precision = precision;
            _dataProvider = new ContainerAccessAdapter(itemsCount);
        }

        /// <param name="precision">As there is no way to calculate the size of each case container,
        /// it's needed to add a precision parameter. The bigger number the better precision.
        /// It tells to the counter what is the maximum of same item combinations can be put in the container</param>
        /// <param name="accessProvider">Custom container data provider</param>
        public EnhancedContainer(int precision, IDataAccessProvider accessProvider)
        {
            _precision = precision;
            _dataProvider = accessProvider;
        }
        
        /// <summary>
        /// Moves container's cursor forward
        /// </summary>
        public void MoveForward()
            => _dataProvider.MoveForward();

        /// <summary>
        /// Moves container's cursor backward
        /// </summary>
        public void MoveBackward()
            => _dataProvider.MoveBackward();

        /// <summary>
        /// Resets the counter to the initial state
        /// </summary>
        private void ResetMatchCounter()
        {
            _firstMatchIndex = null;
            _totalMachCount = 0;
            _matchItems = 0;
        }

        /// <summary>
        /// Moves the cursor to the next item and tries to find the number of items
        /// </summary>
        /// <returns>The number of items in container</returns>
        private int ProcessNextElement()
        {
            MoveCursorForward();

            //We need to determine how many steps we need to get back to
            //the item that will be compared with the current one
            var stepsToTheBackItem = _firstMatchIndex ?? _cursorPosition;
            var readItemValue = GetPreviousItem(stepsToTheBackItem);

            var isMatch = _dataProvider.Value == readItemValue;
            if (isMatch)
            {
                //Checks if it's the first item match 
                if (!_firstMatchIndex.HasValue)
                    _firstMatchIndex = _cursorPosition;

                _matchItems++;
                if (_firstMatchIndex.Value == _matchItems)
                {
                    _totalMachCount++;
                    _matchItems = 0;

                    if (_totalMachCount == _precision)
                        return _firstMatchIndex.Value;
                }

                return ProcessNextElement();
            }

            ResetCursorAndCounterIfNeed();

            ResetMatchCounter();
            return ProcessNextElement();
        }

        private void MoveCursorForward()
        {
            _cursorPosition++;
            _dataProvider.MoveForward();
        }

        /// <summary>
        /// Moves cursor backward to the item which was 'suspected' as the beginning of the cycle 
        /// </summary>
        private void ResetCursorAndCounterIfNeed()
        {
            var needToMoveBack = _firstMatchIndex.HasValue;
            if (!needToMoveBack)
                return;

            var stepsBackward = _cursorPosition - _firstMatchIndex.Value;
            MoveBackward(stepsBackward);
            _cursorPosition -= stepsBackward;
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