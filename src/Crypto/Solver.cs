namespace Crypto
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;

    public class Solver
    {
        public static SolverResult DecryptEnglish(string cipher)
        {
            var tmp = new byte[1];
            var bestSolution = new SolverResult(cipher);

            for (int i = byte.MinValue; i <= byte.MaxValue; i++)
            {
                tmp[0] = (byte) i;
                var message = Translation.HexToASCII(Utility.XORHexStrings(cipher, Translation.BytesToHex(tmp)));
                var score = EnglishScore(message);

                if (score > bestSolution.Score)
                {
                    bestSolution.Key = Convert.ToChar(tmp[0]).ToString();
                    bestSolution.Message = message;
                    bestSolution.Score = score;
                }
            }

            return bestSolution;
        }

        private static double EnglishScore(string message)
        {
            return message.Aggregate(0.0, (current, c) =>
            {
                if (char.IsControl(c))
                {
                    return current - 5;
                }

                if (c == ' ')
                {
                    return current + 10;
                }

                if (char.IsUpper(c))
                {
                    return current + 2;
                }

                if (char.IsLetterOrDigit(c))
                {
                    return current + 5;
                }

                return current;
            });
        }

        public static SolverResult FindBestEnglishMessage(string[] data)
        {
            var bestSolution = new SolverResult("", 0, "Unknown Message", 0.0);

            foreach (var cipher in data)
            {
                var tmpResult = DecryptEnglish(cipher);

                if (tmpResult.Score > bestSolution.Score)
                {
                    bestSolution = tmpResult;
                }
            }

            return bestSolution;
        }

        public static int FindOptimalKeyLength(string data)
        {
            var optimalLength = 0;
            var optimalDistance = double.PositiveInfinity;

            var sampleSize = 12;

            for (var prospectiveKeyLength = 1; prospectiveKeyLength < 41; prospectiveKeyLength++)
            {
                var sum = 0.0;

                for (var sample = 0; sample < sampleSize; sample++)
                {
                    var a = data.Substring(2*sample*prospectiveKeyLength*2, prospectiveKeyLength*2);
                    var b = data.Substring((2*sample + 1)*prospectiveKeyLength*2, prospectiveKeyLength*2);

                    sum += Utility.HammingDistance(Translation.HexToBytes(a), Translation.HexToBytes(b));
                }

                sum /= prospectiveKeyLength*sampleSize;

                if (sum >= optimalDistance)
                {
                    continue;
                }
                optimalDistance = sum;
                optimalLength = prospectiveKeyLength;
            }

            return optimalLength;
        }

        public static SolverResult DecryptMultikeyEnglishMessage(string data)
        {
            var result = new SolverResult(data);

            var key = new List<string>();

            var keyLength = FindOptimalKeyLength(data);
            var entries = Utility.TransposeHexMessage(data, keyLength);
            
            foreach (string entry in entries)
            {
                var result2 = DecryptEnglish(entry);

                key.Add(result2.Key);
            }

            result.Key = String.Join("", key.ToArray());

            return result;
        }

        public static SolverResult IsAESEncrypted(string line)
        {
            var bytes = Translation.HexToBytes(line);
            var tmpResult = new SolverResult(line);

            for (int offset = 0; offset < bytes.Length - 16; offset += 16)
            {
                var sampleA = bytes.Skip(offset).Take(16).ToArray();
                for (int walker = offset + 16; walker < bytes.Length; walker += 16)
                {
                    var sampleB = bytes.Skip(walker).Take(16).ToArray();
                    var same = true;
                    for (var i = 0; i < sampleA.Length; i ++)
                    {
                        if (sampleA[i] != sampleB[i])
                        {
                            same = false;
                            break;
                        }    
                    }

                    if (same)
                    {
                        tmpResult.Score++;
                    }
                }
            }

            return tmpResult;
        }

        public static string DecryptEnglishAES(string key, CipherMode type, byte[] data)
        {
            var aes = new AesManaged
            {
                Key = Translation.ASCIIToBytes(key),
                Mode = type
            };

            string plaintext;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (var msDecrypt = new MemoryStream(data))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt
                    , decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(
                        csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

            return plaintext.Trim('\u0004').Trim();
        }
    }
}