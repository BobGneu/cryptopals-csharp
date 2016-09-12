namespace Crypto
{
    using System;

    public class SolverResult
    {
        public string Key = "";
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
            Key = Convert.ToChar(key).ToString();
            Message = message;
            Score = score;
        }

        public override string ToString()
        {
            return Key + " " + Score + " " + Message;
        }
    }
}