namespace Crypto.Tests.Set1
{
    using System.Security.Cryptography;
    using NUnit.Framework;

    [TestFixture]
    public class Challenge7
    {
        [SetUp]
        public void SetUp()
        {
            _data = Translation.Base64ToBytes(Utility.IO.LoadTestFile("data/cryptoData_7.txt"));
            _target = Utility.IO.LoadTestFile("data/cryptoResult_6.txt").Replace("\r\n", "\n").Trim();
        }

        private byte[] _data;
        private string _target;

        [Test]
        public void ShouldBeAbleToDecryptTheFile()
        {
            Assert.AreEqual(_target.Trim(), Solver.DecryptEnglishAES("YELLOW SUBMARINE", CipherMode.ECB, _data));
        }

        [Test]
        public void ShouldBeAbleToLoadFile()
        {
            Assert.AreEqual(2880, _data.Length);
        }
    }
}