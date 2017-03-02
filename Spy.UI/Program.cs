using Spy.Intercept;
using System;

namespace Spy.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO - change with your decryptor
            IDecryptor decryptor = null;
            var uplink = new Uplink(decryptor);

            var results = uplink.Run();
            results.ForEach(Console.WriteLine);

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }
    }
}
