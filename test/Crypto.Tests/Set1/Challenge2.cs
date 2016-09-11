namespace Crypto.Tests.Set1
{
    using NUnit.Framework;

    [TestFixture]
    public class Challenge2
    {
        [TestCase("FF", "00", "FF")]
        [TestCase("F0", "F0", "00")]
        [TestCase("0F", "F0", "FF")]
        [TestCase("F0", "AF", "5F")]
        [TestCase("AA", "CC", "66")]
        public void ShouldBeAbleToXORTwoStrings(string a, string b, string result)
        {
            Assert.AreEqual(result, Utility.XORHexStrings(a, b));
        }

        [Category("Goal")]
        [Test]
        public void ShouldBeAbleToXORSample()
        {
            var message = "1c0111001f010100061a024b53535009181c";
            var mask = "686974207468652062756c6c277320657965";

            var target = "746865206b696420646f6e277420706c6179";

            Assert.AreEqual(target.ToUpper(), Utility.XORHexStrings(message, mask));
        }
    }
}
