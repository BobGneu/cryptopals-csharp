namespace Crypto.Tests.Set1
{
    using System.IO;
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
            var aes = new AesManaged
            {
                Key = Translation.ASCIIToBytes("YELLOW SUBMARINE"),
                Mode = CipherMode.ECB
            };

            string plaintext;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (var msDecrypt = new MemoryStream(_data))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt
                    , decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(
                        csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

            Assert.AreEqual(_target.Trim(), plaintext.Trim('\u0004').Trim());
        }

        [Test]
        public void ShouldBeAbleToLoadFile()
        {
            Assert.AreEqual(2880, _data.Length);
        }
    }
}