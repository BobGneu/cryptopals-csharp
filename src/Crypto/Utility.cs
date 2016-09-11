namespace Crypto
{
    using System;
    using System.Linq;

    public class Utility
    {
        public static string XORHexStrings(string message, string mask)
        {
            return Translation.BytesToHex(XORBytes(Translation.HexToBytes(message), Translation.HexToBytes(mask)));
        }

        private static byte[] XORBytes(byte[] message, byte[] mask)
        {
            var result = new byte[message.Length];

            for (var ndx = 0; ndx < message.Length; ndx++)
            {
                Console.WriteLine(message[ndx]);
                Console.WriteLine(mask[ndx % mask.Length]);
                Console.WriteLine((byte)(message[ndx] ^ mask[ndx % mask.Length]));

                result[ndx] = (byte)(message[ndx] ^ mask[ndx%mask.Length]);
            }

            Console.Write(String.Join(", ", result));

            return result;
        }
    }
}
