namespace Crypto
{
    public class Utility
    {
        // ReSharper disable once InconsistentNaming
        public static string XORHexStrings(string message, string mask)
        {
            return Translation.BytesToHex(XORBytes(Translation.HexToBytes(message), Translation.HexToBytes(mask)));
        }

        // ReSharper disable once InconsistentNaming
        private static byte[] XORBytes(byte[] message, byte[] mask)
        {
            var result = new byte[message.Length];

            for (var ndx = 0; ndx < message.Length; ndx++)
            {
                result[ndx] = (byte)(message[ndx] ^ mask[ndx%mask.Length]);
            }

            return result;
        }
    }
}
