using System;
using System.IO;
using System.Threading.Tasks;

namespace IDisposable
{
    class Program
    {
        // Possible since C# 7.1
        // But in essence, it's nothing but compiler sugar, wrapping this into another method called <Main>
        // and calling Main.GetAwaiter().GetResult();
        static async Task Main() 
        {
            // Open a new stream and writer to "{current dir}\test.txt", overwrite the file
            using (var sw = new StreamWriter("test.txt", false))
            {
                // Write some text into the stream
                await sw.WriteLineAsync($"Today is {DateTime.Now:D}");
            } // Close the stream and free resources
        }
    }
}
