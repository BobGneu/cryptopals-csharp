namespace Crypto.Tests.Set1
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class Challenge3
    {
        [Category("Goal")]
        [Test]
        public static void ShouldBeAbleToIdentifyMessageAndKey()
        {
            var cipher = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";

            var result = Solver.DecryptEnglish(cipher);

            Assert.AreEqual(Convert.ToByte('X'), result.Key); 
            Assert.AreEqual("Cooking MC's like a pound of bacon", result.Message);
        }

        [TestCase("this is just a test", '$')]
        [TestCase("abcdefghijklmnopqrstuvwxyz", '$')] // Favors lowercase currently b/c they tend to occur more frequently in english writing.
        [TestCase("this is just a test", ' ')]
        public void ShouldBeAbleToDecipherKeyForGivenEnglishString(string message, char key)
        {
            var cipher = Utility.XORHexStrings(Translation.ASCIIToHex(message), Translation.ASCIIToHex(key.ToString()));

            var result = Solver.DecryptEnglish(cipher);

            Assert.AreEqual(Convert.ToByte(key), result.Key, "Found: " + result.Score + " " + result.Message);
            Assert.AreEqual(message, result.Message.ToLower());
        }
    }
}
