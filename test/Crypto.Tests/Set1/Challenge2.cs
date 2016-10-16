/*

Fixed XOR

Write a function that takes two equal-length buffers and produces their XOR combination.

If your function works properly, then when you feed it the string:

    1c0111001f010100061a024b53535009181c
... after hex decoding, and when XOR'd against:

    686974207468652062756c6c277320657965
... should produce:

    746865206b696420646f6e277420706c6179

*/

namespace Crypto.Tests.Set1
{
    using NUnit.Framework;
    using Utility = Crypto.Utility;

    [TestFixture]
    public class Challenge2
    {
        [TestCase("FF", "00", "FF")]
        [TestCase("F0", "F0", "00")]
        [TestCase("0F", "F0", "FF")]
        [TestCase("F0", "AF", "5F")]
        [TestCase("AA", "CC", "66")]
        [TestCase("FFAA", "CC66", "33CC")]
        // ReSharper disable once InconsistentNaming
        public void ShouldBeAbleToXORTwoHexStrings(string a, string b, string result)
        {
            Assert.AreEqual(result, Utility.XORHexStrings(a, b));
        }

        [Category("Goal")]
        [Test]
        // ReSharper disable once InconsistentNaming
        public void ShouldBeAbleToXORSample()
        {
            var message = "1c0111001f010100061a024b53535009181c";
            var mask = "686974207468652062756c6c277320657965";

            var target = "746865206b696420646f6e277420706c6179";

            Assert.AreEqual(target.ToUpper(), Utility.XORHexStrings(message, mask));
        }
    }
}