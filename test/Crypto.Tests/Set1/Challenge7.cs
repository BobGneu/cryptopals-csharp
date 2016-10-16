/*
 
AES in ECB mode

The Base64-encoded content in this file has been encrypted via AES-128 in ECB mode under the key

    "YELLOW SUBMARINE".
(case-sensitive, without the quotes; exactly 16 characters; I like "YELLOW SUBMARINE" because it's exactly 16 bytes long, and now you do too).

Decrypt it. You know the key, after all.

Easiest way: use OpenSSL::Cipher and give it AES-128-ECB as the cipher.

Do this with code.
You can obviously decrypt this using the OpenSSL command-line tool, but we're having you get ECB working in code for a reason. You'll need it a lot later on, and not just for attacking ECB.
 
*/
namespace Crypto.Tests.Set1
{
    using System.Security.Cryptography;
    using NUnit.Framework;
    using Utility;

    [TestFixture]
    public class Challenge7
    {
        [SetUp]
        public void SetUp()
        {
            _data = Translation.Base64ToBytes(IO.LoadTestFile("data/cryptoData_7.txt"));
            _target = IO.LoadTestFile("data/cryptoResult_6.txt").Replace("\r\n", "\n").Trim();
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