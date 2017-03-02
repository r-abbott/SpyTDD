using Spy.Intercept;
using System;

namespace Spy.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var uplink = new Uplink(new MyDecryptor());

            var results = uplink.Run();
            results.ForEach(Console.WriteLine);

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }
    }

    public class MyDecryptor : IDecryptor
    {
        public string Decrypt(string encrypted)
        {
            return encrypted;
        }
    }
}
