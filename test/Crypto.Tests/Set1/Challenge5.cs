namespace Crypto.Tests.Set1
{
    using NUnit.Framework;

    [TestFixture]
    public class Challenge5
    {
        [Test]
        public void ShouldSupportRepeatingKeyEncryption()
        {
            var message = "Burning 'em, if you ain't quick and nimble\nI go crazy when I hear a cymbal";
            var target = "0b3637272a2b2e63622c2e69692a23693a2a3c6324202d623d63343c2a26226324272765272a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f";
            var key = "ICE";

            Assert.AreEqual(target.ToUpper(), Translation.BytesToHex(Crypto.Utility.XORBytes(Translation.ASCIIToBytes(message), Translation.ASCIIToBytes(key))));
        }
    }
}