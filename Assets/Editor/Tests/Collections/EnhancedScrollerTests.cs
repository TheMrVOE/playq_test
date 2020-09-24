using Collections;
using Editor.Tests.Collections.TestEntities;
using NUnit.Framework;

namespace Editor.Tests.Collections
{
    public class EnhancedScrollerTests
    {
        [Test]
        public void ValidTestCasesTests()
        {
            const int precision = 20;

            var palindromeSample = new[] {false, true, true, false};
            var randomSample = new[]
                {false, true, true, false, false, true, true, false, true, true, true, true, false, 
                    true, false, true, false, true, true, false, false, true, false, true, false};
            var singleElementSample = new[] {true};

            var palindromeCollection = new EnhancedContainer(precision,
                new DoubleLinkedCollection(palindromeSample));
            var randomCollection = new EnhancedContainer(precision,
                new DoubleLinkedCollection(randomSample));
            var singleElementCollection = new EnhancedContainer(precision,
                new DoubleLinkedCollection(singleElementSample));

            Assert.AreEqual(palindromeSample.Length, palindromeCollection.Count,
                "palindrome collection check");
            Assert.AreEqual(randomSample.Length, randomCollection.Count,
                "random elements collection check");
            Assert.AreEqual(singleElementSample.Length, singleElementCollection.Count,
                "single elements collection check");
        }

        [Test]
        public void InvalidTestCasesTests()
        {
            const int precision = 20;
            var sameElementsSample = new[] {true, true, true};
            var repeatableElementsSample = new[] {true, false, true, false};

            var sameElementsCollection = new EnhancedContainer(precision,
                new DoubleLinkedCollection(sameElementsSample));
            var repeatableElementsCollection = new EnhancedContainer(precision,
                new DoubleLinkedCollection(repeatableElementsSample));

            Assert.AreEqual(1, sameElementsCollection.Count, "single elements collection check");
            Assert.AreEqual(2, repeatableElementsCollection.Count, "repeatable elements collection check");
        }

        [Test]
        public void LowPrecisionTests()
        {
            const int precision = 1;
            
            var lowPrecisionSamples = new[] {true, false, true, false, false, false, false};
            var lowPrecisionSampleCollection = new EnhancedContainer(precision,
                new DoubleLinkedCollection(lowPrecisionSamples));
            
            Assert.AreEqual(2, lowPrecisionSampleCollection.Count, "low precision sample collection check");
        }
    }
}