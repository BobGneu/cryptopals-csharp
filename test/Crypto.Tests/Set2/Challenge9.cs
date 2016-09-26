namespace Crypto.Tests.Set2
{
    using NUnit.Framework;
    using Utility = Crypto.Utility;

    public class Challenge9
    {
        [TestCase("", 16, "\x10\x10\x10\x10\x10\x10\x10\x10\x10\x10\x10\x10\x10\x10\x10\x10")]
        [TestCase("a", 16, "a\xf\xf\xf\xf\xf\xf\xf\xf\xf\xf\xf\xf\xf\xf\xf")]
        [TestCase("abcdefghijklmnopqrstuvwxyz", 16, "abcdefghijklmnopqrstuvwxyz\x6\x6\x6\x6\x6\x6")]
        [TestCase("abcdefghijklmnopqrstu", 16, "abcdefghijklmnopqrstu\xB\xB\xB\xB\xB\xB\xB\xB\xB\xB\xB")]
        [TestCase("", 8, "\x8\x8\x8\x8\x8\x8\x8\x8")]
        public void ShouldPadToAppropriateBlockLength(string message, int length, string expected)
        {
            var paddedString = Utility.AddPKCS_7Padding(message, length);

            Assert.AreEqual(expected, paddedString);
        }
    }
}