﻿namespace Crypto
{
    using System.Collections.Generic;
    using System.Linq;

    public class Utility
    {
        private static readonly Dictionary<byte, int> ByteLookup;

        static Utility()
        {
            ByteLookup = new Dictionary<byte, int>();

            for (var i = 0; i <= byte.MaxValue; i++)
            {
                ByteLookup[(byte) i] =
                    ((i & 1 << 0) != 0 ? 1 : 0) +
                    ((i & 1 << 1) != 0 ? 1 : 0) +
                    ((i & 1 << 2) != 0 ? 1 : 0) +
                    ((i & 1 << 3) != 0 ? 1 : 0) +
                    ((i & 1 << 4) != 0 ? 1 : 0) +
                    ((i & 1 << 5) != 0 ? 1 : 0) +
                    ((i & 1 << 6) != 0 ? 1 : 0) +
                    ((i & 1 << 7) != 0 ? 1 : 0);
            }
        }

        // ReSharper disable once InconsistentNaming
        public static string XORHexStrings(string message, string mask)
        {
            return Translation.BytesToHex(XORBytes(Translation.HexToBytes(message), Translation.HexToBytes(mask)));
        }

        // ReSharper disable once InconsistentNaming
        public static byte[] XORBytes(byte[] message, byte[] mask)
        {
            var result = new byte[message.Length];

            for (var ndx = 0; ndx < message.Length; ndx++)
            {
                result[ndx] = (byte) (message[ndx] ^ mask[ndx%mask.Length]);
            }

            return result;
        }

        public static int HammingDistance(byte[] a, byte[] b)
        {
            return a.Select((t, i) => ByteLookup[(byte) (t ^ b[i])]).Sum();
        }

        public static string[] TransposeHexMessage(string data, int keyLength)
        {
            var result = new string[keyLength];
            var bytes = Translation.HexToBytes(data);
            var buckets = new List<byte>[keyLength];

            for (var i = 0; i < keyLength; i++)
            {
                buckets[i] = new List<byte>();
            }

            for (var i = 0; i < bytes.Length; i++)
            {
                buckets[i%keyLength].Add(bytes[i]);
            }

            for (var i = 0; i < keyLength; i++)
            {
                result[i] = Translation.BytesToHex(buckets[i].ToArray());
            }

            return result;
        }

        public static string AddPKCS_7Padding(string message, int length)
        {
            var bytes = Translation.ASCIIToBytes(message);
            var paddedElementCount = length - bytes.Length%length;

            var paddedMessage = new byte[bytes.Length + paddedElementCount];

            for (var i = 0; i < bytes.Length; i++)
            {
                paddedMessage[i] = bytes[i];
            }

            for (var i = bytes.Length; i < bytes.Length + paddedElementCount; i++)
            {
                paddedMessage[i] = (byte) paddedElementCount;
            }

            return Translation.BytesToASCII(paddedMessage);
        }
    }
}