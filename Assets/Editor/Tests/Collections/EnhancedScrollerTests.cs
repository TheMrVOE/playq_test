using System.Linq;
using Collections;
using Editor.Tests.Collections.TestEntities;
using NUnit.Framework;

namespace Editor.Tests.Collections
{
    public class EnhancedScrollerTests
    {
        private bool[] _randomSequenceArray =
        {
            false, true, true, false, false, true, true, false, true, true, true, true, false,
            true, false, true, false, true, true, false, false, true, false, true, false
        };

        [Test]
        public void PalindromeProcessingReturnedActualArraySizeTest()
        {
            var palindromeSample = new[] {false, true, true, false};

            var palindromeCollection = new EnhancedContainer(new DoubleLinkedCollection(palindromeSample));

            Assert.AreEqual(palindromeSample.Length, palindromeCollection.Count);
        }

        [Test]
        public void RandomSequenceProcessingReturnedActualArraySizeTest()
        {
            var randomCollection = new EnhancedContainer(new DoubleLinkedCollection(_randomSequenceArray));

            Assert.AreEqual(_randomSequenceArray.Length, randomCollection.Count);
        }

        [Test]
        public void SingleElementProcessingReturnsActualArrayLengthTest()
        {
            var singleElementSample = new[] {true};

            var singleElementCollection = new EnhancedContainer(new DoubleLinkedCollection(singleElementSample));

            Assert.AreEqual(singleElementSample.Length, singleElementCollection.Count);
        }

        [Test]
        public void SameElementsSequenceProcessingReturnedWrongLengthTest()
        {
            var sameElementsSample = new[] {true, true, true};

            var sameElementsCollection = new EnhancedContainer(new DoubleLinkedCollection(sameElementsSample));

            Assert.AreEqual(sameElementsSample.Length, sameElementsCollection.Count);
        }

        [Test]
        public void RepeatableElementsProcessingReturnedWrongLengthTest()
        {
            var repeatableElementsSample = new[] {true, false, true, false};

            var repeatableElementsCollection = new EnhancedContainer(
                new DoubleLinkedCollection(repeatableElementsSample));

            Assert.AreEqual(repeatableElementsSample.Length, repeatableElementsCollection.Count);
        }

        [Test]
        public void CollectionIsNotChangedAfterProcessingTest()
        {
            var randomCollection = new EnhancedContainer(new DoubleLinkedCollection(_randomSequenceArray));
            var collectionCount = randomCollection.Count;
            
            var initialSequence = new bool[collectionCount];
            for (int i = 0; i < collectionCount; i++)
            {
                initialSequence[i] = randomCollection.Value;
                randomCollection.MoveForward();
            }
            
            randomCollection.CalculateLength();
            
            var sequenceAfterGettingCount = new bool[collectionCount];
            for (int i = 0; i < collectionCount; i++)
            {
                sequenceAfterGettingCount[i] = randomCollection.Value;
                randomCollection.MoveForward();
            }
            
            bool areEqual = Enumerable.SequenceEqual(initialSequence, sequenceAfterGettingCount);
            
            Assert.IsTrue(areEqual);
        }
    }
}