using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace CallMeMaybe
{
    public class Log
    {
        public static void Write(string text)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "log.txt");
            File.AppendAllText(path, text + "\n");
        }
        
        public static async Task WriteAsync(string text)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "log.txt");
            await File.AppendAllTextAsync(path, text + "\n");
        }
    }
}