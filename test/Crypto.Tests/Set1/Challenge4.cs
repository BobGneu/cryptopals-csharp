namespace Crypto.Tests.Set1
{
    using System;
    using System.IO;
    using System.Reflection;
    using NUnit.Framework;

    [TestFixture]
    public class Challenge4
    {
        string[] data;

        [SetUp]
        public void Setup()
        {
            data = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data/cryptoData_4.txt"));

            Console.WriteLine(data);
        }

        [Test]
        public void ShouldLoadFile()
        {
            Assert.AreEqual(327, data.Length);
        }

        [Test]
        public void ShouldFindBestSolutionInSampleSet()
        {
            var result = Solver.FindBestEnglishMessage(data);

            Assert.AreEqual("5", result.Key, "Found: " + result);
            Assert.AreEqual("Now that the party is jumping", result.Message.Trim());
        }
    }
}
