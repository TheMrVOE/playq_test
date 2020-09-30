using Collections;
using Editor.Tests.Collections.TestEntities;
using NUnit.Framework;

namespace Editor.Tests.Collections
{
    public class EnhancedScrollerTests
    {
        private const int REGULAR_PRECISION_COUNT = 20;
        private const int LOW_PRECISSION_COUNT = 1;
        
        [Test]
        public void PalindromeProcessingReturnedActualArraySizeTest()
        {
            var palindromeSample = new[] {false, true, true, false};   

            var palindromeCollection = new EnhancedContainer(REGULAR_PRECISION_COUNT,
                new DoubleLinkedCollection(palindromeSample));

            Assert.AreEqual(palindromeSample.Length, palindromeCollection.Count,
                "palindrome collection check");
        }

        [Test]
        public void RandomSequenceProcessingReturnedActualArraySizeTest()
        {
            var randomSample = new[]
            {false, true, true, false, false, true, true, false, true, true, true, true, false, 
                true, false, true, false, true, true, false, false, true, false, true, false};
            
            var randomCollection = new EnhancedContainer(REGULAR_PRECISION_COUNT,
                new DoubleLinkedCollection(randomSample));
            
            Assert.AreEqual(randomSample.Length, randomCollection.Count,
                "random elements collection check");
        }

        [Test]
        public void SingleElementProcessingReturnsActualArrayLengthTest()
        {
            var singleElementSample = new[] {true};
            
            var singleElementCollection = new EnhancedContainer(REGULAR_PRECISION_COUNT,
                new DoubleLinkedCollection(singleElementSample));
            
            Assert.AreEqual(singleElementSample.Length, singleElementCollection.Count,
                "single elements collection check");
        }

        [Test]
        public void SameElementsSequenceProcessingReturnedWrongCountTest()
        {
            var sameElementsSample = new[] {true, true, true};
            
            var sameElementsCollection = new EnhancedContainer(REGULAR_PRECISION_COUNT,
                new DoubleLinkedCollection(sameElementsSample));
            
            Assert.AreEqual(1, sameElementsCollection.Count, "single elements collection check");
        }
        [Test]
        public void RepeatableElementsProcessingReturnedWrongCountTest()
        {
            var repeatableElementsSample = new[] {true, false, true, false};
   
            var repeatableElementsCollection = new EnhancedContainer(REGULAR_PRECISION_COUNT,
                new DoubleLinkedCollection(repeatableElementsSample));
            
            Assert.AreEqual(2, repeatableElementsCollection.Count, "repeatable elements collection check");
        }

        [Test]
        public void LowPrecisionProcessingReturnedWrongLengthTest()
        {   
            var lowPrecisionSamples = new[] {true, false, true, false, false, false, false};
            var lowPrecisionSampleCollection = new EnhancedContainer(LOW_PRECISSION_COUNT,
                new DoubleLinkedCollection(lowPrecisionSamples));
            
            Assert.AreEqual(2, lowPrecisionSampleCollection.Count, "low precision sample collection check");
        }
    }
}