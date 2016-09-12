namespace Crypto
{
    using System;
    using System.Linq;

    public class Solver
    {
        public static SolverResult DecryptEnglish(string cipher)
        {
            byte[] tmp = new byte[1];
            var bestSolution = new SolverResult(cipher);

            for (int i = byte.MinValue; i <= byte.MaxValue; i++)
            {
                tmp[0] = (byte)i;
                var message = Translation.HexToASCII(Utility.XORHexStrings(cipher, Translation.BytesToHex(tmp)));
                var score = EnglishScore(message);

                if (score > bestSolution.Score)
                {
                    bestSolution.Key = tmp[0];
                    bestSolution.Message = message;
                    bestSolution.Score = score;
                }
            }

            Console.WriteLine(bestSolution);

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
            SolverResult bestSolution = new SolverResult("", 0, "Unknown Message", 0.0);

            foreach (string cipher in data)
            {
                var tmpResult = DecryptEnglish(cipher);

                if (tmpResult.Score > bestSolution.Score)
                {
                    bestSolution = tmpResult;
                }
            }

            return bestSolution;
        }
    }
}