namespace Crypto.Tests.Set1
{
    using NUnit.Framework;
    using Crypto;

    [TestFixture]
    public class Challenge1
    {
        [Category("To Bytes")]
        [TestCase(" ", new byte[] { 32 })]
        [TestCase("!", new byte[] { 33 })]
        [TestCase("A", new byte[] { 65 })]
        [TestCase("Man", new byte[] { 77, 97, 110 })]
        // ReSharper disable once InconsistentNaming
        public void ShouldBeAbleToConvertASCIItoByteArray(string input, byte[] expected)
        {
            Assert.AreEqual(expected, Translation.ASCIIToBytes(input));
        }

        [Category("To ASCII")]
        [TestCase(" ", new byte[] { 32 })]
        [TestCase("!", new byte[] { 33 })]
        [TestCase("A", new byte[] { 65 })]
        [TestCase("Man", new byte[] { 77, 97, 110 })]
        // ReSharper disable once InconsistentNaming
        public void ShouldBeAbleToConvertByteArrayToASCII(string expected, byte[] input)
        {
            Assert.AreEqual(expected, Translation.BytesToASCII(input));
        }

        [Category("To Hex")]
        [TestCase(new byte[] { 0 }, "00")]
        [TestCase(new byte[] { 16 }, "10")]
        [TestCase(new byte[] { 32 }, "20")]
        [TestCase(new byte[] { 33 }, "21")]
        [TestCase(new byte[] { 65 }, "41")]
        [TestCase(new byte[] { 128 }, "80")]
        [TestCase(new byte[] { 255 }, "FF")]
        [TestCase(new byte[] { 77, 97, 110 }, "4D616E")]
        public void ShouldBeAbleToConvertByteArrayToHexString(byte[] input, string expected)
        {
            Assert.AreEqual(expected, Translation.BytesToHex(input));
        }

        [Category("To Bytes")]
        [TestCase(new byte[] { 0 }, "00")]
        [TestCase(new byte[] { 16 }, "10")]
        [TestCase(new byte[] { 32 }, "20")]
        [TestCase(new byte[] { 33 }, "21")]
        [TestCase(new byte[] { 65 }, "41")]
        [TestCase(new byte[] { 128 }, "80")]
        [TestCase(new byte[] { 255 }, "FF")]
        [TestCase(new byte[] { 77, 97, 110 }, "4D616E")]
        public void ShouldBeAbleToConvertHexStringToByteArray(byte[] expected, string input)
        {
            Assert.AreEqual(expected, Translation.HexToBytes(input));
        }

        [Category("To Base64")]
        [TestCase(new byte[] { 65 }, "QQ==")]
        [TestCase(new byte[] { 77, 97, 110 }, "TWFu")]
        public void ShouldBeAbleToConvertByteArrayToBase64String(byte[] input, string expected)
        {
            Assert.AreEqual(expected, Translation.BytesToBase64(input));
        }

        [Category("To Bytes")]
        [TestCase(new byte[] { 65 }, "QQ==")]
        [TestCase(new byte[] { 77, 97, 110 }, "TWFu")]
        public void ShouldBeAbleToConvertBase64StringToByteArray(byte[] expected, string input)
        {
            Assert.AreEqual(expected, Translation.Base64ToBytes(input));
        }

        [Category("To Hex")]
        [TestCase("A", "41")]
        [TestCase("Man", "4D616E")]
        // ReSharper disable once InconsistentNaming
        public void ShouldBeAbleToConvertASCIIiToHex(string input, string expected)
        {
            Assert.AreEqual(expected, Translation.ASCIIToHex(input));
        }

        [Category("To ASCII")]
        [TestCase("A", "41")]
        [TestCase("Man", "4D616E")]
        // ReSharper disable once InconsistentNaming
        public void ShouldBeAbleToConvertHexToASCII(string expected, string input)
        {
            Assert.AreEqual(expected, Translation.HexToASCII(input));
        }

        [Category("To Base64")]
        [TestCase("41", "QQ==")]
        [TestCase("4D616E", "TWFu")]
        public void ShouldBeAbleToConvertHexToBase64(string input, string expected)
        {
            Assert.AreEqual(expected, Translation.HexToBase64(input));
        }
        
        [Category("To Hex")]
        [TestCase("41", "QQ==")]
        [TestCase("4D616E", "TWFu")]
        public void ShouldBeAbleToConvertBase64ToHex(string expected, string input)
        {
            Assert.AreEqual(expected, Translation.Base64ToHex(input));
        }

        [Category("To Base64")]
        [TestCase("A", "QQ==")]
        [TestCase("Man", "TWFu")]
        public void ShouldBeAbleToConvertAsciiToBase64(string input, string expected)
        {
            Assert.AreEqual(expected, Translation.ASCIIToBase64(input));
        }

        [Category("To ASCII")]
        [TestCase("A", "QQ==")]
        [TestCase("Man", "TWFu")]
        // ReSharper disable once InconsistentNaming
        public void ShouldBeAbleToConverBase64ToASCII(string expected, string input)
        {
            Assert.AreEqual(expected, Translation.Base64ToASCII(input));
        }

        [Category("To Hex")]
        [TestCase("H", "48")]
        [TestCase("e", "65")]
        [TestCase("l", "6C")]
        [TestCase("o", "6F")]
        [TestCase(" ", "20")]
        [TestCase("W", "57")]
        [TestCase("r", "72")]
        [TestCase("d", "64")]
        [TestCase("!", "21")]
        public void ShouldBeAbleToEncodeAsciiToHex(string input, string expected)
        {
            var result = Translation.ASCIIToHex(input);

            Assert.AreEqual(expected, result);
        }

        [Category("To Hex"), Category("Goal")]
        [TestCase("49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d", "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t")]
        public void ShouldBeAbleToChangeEncodingOfSampleProblem(string input, string expected)
        {
            var result = Translation.HexToBase64(input);

            Assert.AreEqual(expected, result);
        }
    }
}
