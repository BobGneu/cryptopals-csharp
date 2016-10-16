/*

Break repeating-key XOR

It is officially on, now.
This challenge isn't conceptually hard, but it involves actual error-prone coding. The other challenges in this set are there to bring you up to speed. This one is there to qualify you. If you can do this one, you're probably just fine up to Set 6.

There's a file here. It's been base64'd after being encrypted with repeating-key XOR.

Decrypt it.

Here's how:

    1. Let KEYSIZE be the guessed length of the key; try values from 2 to (say) 40.
    2. Write a function to compute the edit distance/Hamming distance between two strings. The Hamming distance is just the number of differing bits. 

        The distance between:
    
            this is a test
    
        and

            wokka wokka!!!

        is 37. Make sure your code agrees before you proceed.

    3. For each KEYSIZE, take the first KEYSIZE worth of bytes, and the second KEYSIZE worth of bytes, and find the edit distance between them. Normalize this result by dividing by KEYSIZE.
    4. The KEYSIZE with the smallest normalized edit distance is probably the key. You could proceed perhaps with the smallest 2-3 KEYSIZE values. Or take 4 KEYSIZE blocks instead of 2 and average the distances.
    5. Now that you probably know the KEYSIZE: break the ciphertext into blocks of KEYSIZE length.
    6. Now transpose the blocks: make a block that is the first byte of every block, and a block that is the second byte of every block, and so on.
    7. Solve each block as if it was single-character XOR. You already have code to do this.
    8. For each block, the single-byte XOR key that produces the best looking histogram is the repeating-key XOR key byte for that block. Put them together and you have the key.

This code is going to turn out to be surprisingly useful later on. Breaking repeating-key XOR ("Vigenere") statistically is obviously an academic exercise, a "Crypto 101" thing. But more people "know how" to break it than can actually break it, and a similar technique breaks something much more important.

No, that's not a mistake.
We get more tech support questions for this challenge than any of the other ones. We promise, there aren't any blatant errors in this text. In particular: the "wokka wokka!!!" edit distance really is 37. 
 
*/

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