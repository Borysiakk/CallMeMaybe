using System.IO;
using System.Linq;
using NUnit.Framework;

namespace CallMeMaybe.Test
{
    public class CallMeMebeLogTest
    {
        [Test]
        public void WriteSaveLogToFile()
        {
            Log.Write("Test:Log");
            
            var lastLine = File.ReadLines("log.txt").Last();
            
            Assert.AreEqual(lastLine,"Test:Log");
        }
    }
}