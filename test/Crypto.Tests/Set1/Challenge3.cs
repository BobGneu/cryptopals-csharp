/*

Single-byte XOR cipher

The hex encoded string:

    1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736

... has been XOR'd against a single character. Find the key, decrypt the message.

You can do this by hand. But don't: write code to do it for you.

How? Devise some method for "scoring" a piece of English plaintext. Character frequency is a good metric. Evaluate each output and choose the one with the best score.

Achievement Unlocked
You now have our permission to make "ETAOIN SHRDLU" jokes on Twitter.

*/
namespace Crypto.Tests.Set1
{
    using NUnit.Framework;
    using Utility = Crypto.Utility;

    [TestFixture]
    public class Challenge3
    {
        [TestCase("this is just a test", "$")]
        // Favors lowercase currently b/c they tend to occur more frequently in english writing.
        [TestCase("abcdefghijklmnopqrstuvwxyz", "$")]
        [TestCase("this is just a test", " ")]
        public void ShouldBeAbleToDecipherKeyForGivenEnglishString(string message, string key)
        {
            var cipher = Utility.XORHexStrings(Translation.ASCIIToHex(message), Translation.ASCIIToHex(key));

            var result = Solver.DecryptEnglish(cipher);

            Assert.AreEqual(key, result.Key, result.ToString());
            Assert.AreEqual(message, result.Message.ToLower());
        }

        [Category("Goal")]
        [Test]
        public static void ShouldBeAbleToIdentifyMessageAndKey()
        {
            var cipher = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";

            var result = Solver.DecryptEnglish(cipher);

            Assert.AreEqual("X", result.Key);
            Assert.AreEqual("Cooking MC's like a pound of bacon", result.Message);
        }
    }
}