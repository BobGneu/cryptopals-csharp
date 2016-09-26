namespace Crypto.Tests.Set1
{
    using System;
    using NUnit.Framework;
    using Utility;

    [TestFixture]
    public class Challenge4
    {
        [SetUp]
        public void Setup()
        {
            _data = IO.LoadTestFileLines("data/cryptoData_4.txt");

            Console.WriteLine(_data);
        }

        private string[] _data;

        [Test]
        public void ShouldFindBestSolutionInSampleSet()
        {
            var result = Solver.FindBestEnglishMessage(_data);

            Assert.AreEqual("5", result.Key, "Found: " + result);
            Assert.AreEqual("Now that the party is jumping", result.Message.Trim());
        }

        [Test]
        public void ShouldLoadFile()
        {
            Assert.AreEqual(327, _data.Length);
        }
    }
}