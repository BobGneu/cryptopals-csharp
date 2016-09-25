namespace Crypto.Tests.Utility
{
    using System.IO;
    using static System.Reflection.Assembly;

    public static class IO
    {
        public static string LoadTestFile(string path)
        {
            return File.ReadAllText(Path.Combine(Path.GetDirectoryName(GetExecutingAssembly().Location), path));
        }
        public static string[] LoadTestFileLines(string path)
        {
            return File.ReadAllLines(Path.Combine(Path.GetDirectoryName(GetExecutingAssembly().Location), path));
        }
    }
}