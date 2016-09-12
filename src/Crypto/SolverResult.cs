namespace Crypto
{
    using System;

    public class SolverResult
    {
        public byte Key = byte.MaxValue;
        public string Message;
        public double Score = -1;

        public string Cipher { get; set; }

        public SolverResult(string cipher)
        {
            Cipher = cipher;
        }

        public SolverResult(string cipher, byte key, string message, double score)
        {
            Cipher = cipher;
            Key = key;
            Message = message;
            Score = score;
        }

        public override string ToString()
        {
            return Convert.ToChar(Key) + " " + Score + " " + Message;
        }
    }
}