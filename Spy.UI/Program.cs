using Spy.Intercept;
using System;

namespace Spy.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var uplink = new Uplink(new MyDecryptor());

            //var results = uplink.Run();
            //results.ForEach(Console.WriteLine);

            var results = uplink.RunCaesar("break into test driven development", true);
            Console.WriteLine(results);
            results = uplink.RunCaesar(results, false);
            Console.WriteLine(results);

            results = uplink.RunKeyword("at first it may seem like an enigma", true);
            Console.WriteLine(results);
            results = uplink.RunKeyword(results, false);
            Console.WriteLine(results);

            results = uplink.RunVignere("each day will yield a different result", true);
            Console.WriteLine(results);
            results = uplink.RunVignere(results, false);
            Console.WriteLine(results);

            results = uplink.RunDayOfWeek("but once you understand it will all become clear", true);
            Console.WriteLine(results);
            results = uplink.RunDayOfWeek(results, false);
            Console.WriteLine(results);

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
