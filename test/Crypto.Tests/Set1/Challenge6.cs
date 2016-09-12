namespace Crypto.Tests.Set1
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class Challenge6
    {
        [TestCase("a", "A", 1)]
        [TestCase("$", "q", 4)]
        [TestCase("j", "Û", 4)]
        [TestCase("B", "A", 2)]
        [TestCase("this is a test", "wokka wokka!!!", 37)]
        public void ShouldBeAbleToCalculateHammingDistanceBetweenStrings(string a, string b, int distance)
        {
            Assert.AreEqual(distance, Utility.HammingDistance(Translation.ASCIIToBytes(a), Translation.ASCIIToBytes(b)));
        }
    }
}