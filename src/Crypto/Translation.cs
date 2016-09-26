namespace Crypto
{
    using System;
    using System.Text;

    public class Translation
    {
        #region To Base64 Encoding

        // ReSharper disable once InconsistentNaming
        public static string ASCIIToBase64(string input)
        {
            return BytesToBase64(ASCIIToBytes(input));
        }

        public static string BytesToBase64(byte[] input)
        {
            return Convert.ToBase64String(input);
        }

        public static string HexToBase64(string input)
        {
            var bytes = HexToBytes(input);

            return Convert.ToBase64String(bytes);
        }

        #endregion

        #region To Byte Conversion

        // ReSharper disable once InconsistentNaming
        public static byte[] ASCIIToBytes(string input)
        {
            return Encoding.ASCII.GetBytes(input);
        }

        public static byte[] Base64ToBytes(string input)
        {
            return Convert.FromBase64String(input);
        }

        public static byte[] HexToBytes(string input)
        {
            var arr = new string[input.Length/2];

            for (var i = 0; i < input.Length; i += 2)
            {
                arr[i/2] = input.Substring(i, 2);
            }

            return Array.ConvertAll(arr, s => Convert.ToByte(s, 16));
        }

        #endregion

        #region To ASCII Conversion

        // ReSharper disable once InconsistentNaming
        public static string BytesToASCII(byte[] input)
        {
            var tmpString = new StringBuilder();

            foreach (var character in input)
            {
                tmpString.Append(Convert.ToChar(character));
            }

            return tmpString.ToString();
        }

        // ReSharper disable once InconsistentNaming
        public static string Base64ToASCII(string input)
        {
            return BytesToASCII(Convert.FromBase64String(input));
        }

        // ReSharper disable once InconsistentNaming
        public static string HexToASCII(string input)
        {
            return BytesToASCII(HexToBytes(input));
        }

        #endregion

        #region To HEX Conversion

        public static string BytesToHex(byte[] input)
        {
            var lookup = "0123456789ABCDEF";

            int i = -1, walker = -1, length = input.Length;

            var result = new char[length*2];

            --length;

            while (i < length)
            {
                var character = input[++i];

                result[++walker] = lookup[character >> 4]; // First byte
                result[++walker] = lookup[character & 0xF]; // Second byte 
            }

            return new string(result, 0, result.Length);
        }

        // ReSharper disable once InconsistentNaming
        public static string ASCIIToHex(string input)
        {
            return BytesToHex(ASCIIToBytes(input));
        }

        public static string Base64ToHex(string input)
        {
            return BytesToHex(Base64ToBytes(input));
        }

        #endregion
    }
}