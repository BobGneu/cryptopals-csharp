/*

Implement PKCS#7 padding

A block cipher transforms a fixed-sized block(usually 8 or 16 bytes) of plaintext into ciphertext.But we almost never want to transform a single block; we encrypt irregularly-sized messages.

One way we account for irregularly-sized messages is by padding, creating a plaintext that is an even multiple of the blocksize. The most popular padding scheme is called PKCS#7.

So: pad any block to a specific block length, by appending the number of bytes of padding to the end of the block. For instance,

"YELLOW SUBMARINE"
... padded to 20 bytes would be:

"YELLOW SUBMARINE\x04\x04\x04\x04"

*/

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
        [TestCase("YELLOW SUBMARINE", 20, "YELLOW SUBMARINE\x04\x04\x04\x04")]
        public void ShouldPadToAppropriateBlockLength(string message, int length, string expected)
        {
            var paddedString = Utility.AddPKCS_7Padding(message, length);

            Assert.AreEqual(expected, paddedString);
        }
    }
}