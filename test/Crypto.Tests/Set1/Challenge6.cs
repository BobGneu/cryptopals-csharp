namespace Crypto.Tests.Set1
{
    using NUnit.Framework;
    using Utility;
    using Utility = Crypto.Utility;

    [TestFixture]
    public class Challenge6
    {
        [SetUp]
        public void Setup()
        {
            _data = Translation.Base64ToHex(IO.LoadTestFile("data/cryptoData_6.txt"));
            _target = IO.LoadTestFile("data/cryptoResult_6.txt").Replace("\r\n", "\n").Trim();
        }

        private string _data;
        private string _target;

        [TestCase("a", "A", 1)]
        [TestCase("$", "q", 4)]
        [TestCase("j", "Û", 4)]
        [TestCase("B", "A", 2)]
        [TestCase("this is a test", "wokka wokka!!!", 37)]
        public void ShouldBeAbleToCalculateHammingDistanceBetweenStrings(string a, string b, int distance)
        {
            Assert.AreEqual(distance, Utility.HammingDistance(Translation.ASCIIToBytes(a), Translation.ASCIIToBytes(b)));
        }

        [TestCase("abcdef", 2, new[] {"ace", "bdf"})]
        [TestCase("alphabet soup", 2, new[] {"apae op", "lhbtsu"})]
        public void ShouldBeAbleToTransposeMessageIntoNBlocks(string message, int length, string[] expected)
        {
            var result = Utility.TransposeHexMessage(Translation.ASCIIToHex(message), length);

            Assert.AreEqual(expected.Length, result.Length);
            Assert.AreEqual(length, result.Length);

            for (var i = 0; i < length; i++)
            {
                Assert.AreEqual(expected[i], Translation.HexToASCII(result[i]));
            }
        }

        [Test]
        public void ShouldBeAbleToDecipherTheEncryptedFile()
        {
            var result = Solver.DecryptMultikeyEnglishMessage(_data);

            var message = Utility.XORHexStrings(_data, Translation.ASCIIToHex(result.Key));

            Assert.AreEqual(_target, Translation.HexToASCII(message).Trim());
        }

        [Test]
        public void ShouldBeAbleToFindKeyForEncryptedFile()
        {
            var result = Solver.DecryptMultikeyEnglishMessage(_data);

            Assert.AreEqual("Terminator X: Bring the noise", result.Key);
        }

        [Test]
        public void ShouldBeAbleToFindKeySizeForEncryptedFile()
        {
            var keyLen = Solver.FindOptimalKeyLength(_data);

            Assert.AreEqual(29, keyLen);
        }

        [Test]
        public void ShouldLoadFile()
        {
            Assert.AreEqual(5752, _data.Length);
        }
    }
}