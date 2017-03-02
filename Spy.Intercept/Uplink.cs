using System;
using System.Collections.Generic;
using System.Linq;

namespace Spy.Intercept
{
    public class Uplink
    {
        private readonly IDecryptor _decryptor;
        private readonly List<string> _encryptedMessages;

        public Uplink(IDecryptor decryptor)
        {
            _decryptor = decryptor;
            _encryptedMessages = EncryptMessages();
        }

        public List<string> Run()
        {
            var results = new List<string>();
            _encryptedMessages.ForEach(m =>
            {
                results.Add(_decryptor.Decrypt(m));
            });
            return results;
        }

        private List<string> EncryptMessages()
        {
            var caesar = new CaesarCipher();
            var keyword = new KeywordCipher();
            var vignere = new VignereCipher();
            var dayOfWeek = new DayOfWeekCipher();

            var encrypted = new List<string>
            {
                caesar.Encrypt(PlaintextMessages[0]),
                keyword.Encrypt(PlaintextMessages[1]),
                vignere.Encrypt(PlaintextMessages[2]),
                dayOfWeek.Encrypt(PlaintextMessages[3])
            };
            return encrypted;
        }

        private static readonly List<string> PlaintextMessages = new List<string>
        {
            "break into test driven development",
            "at first it may seem like an enigma",
            "each day will yield a different result",
            "but once you understand it will all become clear"
        };
    }

    internal class CaesarCipher : IDecryptor
    {
        private const int ShiftKey = 3;
        private static readonly List<char> Alphabet = new List<char>
        {
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'
        };

        public string Encrypt(string message)
        {
            message = message.Replace(" ", "");
            string encrypted = "";

            foreach(var c in message.ToLower())
            {
                var position = Alphabet.IndexOf(c);
                var shift = position + ShiftKey;
                var shiftPosition = shift >= Alphabet.Count
                    ? shift - Alphabet.Count
                    : shift;
                encrypted += Alphabet[shiftPosition];
            }
            return encrypted;
        }

        public string Decrypt(string message)
        {
            message = message.Replace(" ", "");
            string decrypted = "";

            foreach (var c in message.ToLower())
            {
                var position = Alphabet.IndexOf(c);
                var shift = position - ShiftKey;
                var shiftPosition = shift < 0
                    ? Alphabet.Count - 1
                    : shift;
                decrypted += Alphabet[shiftPosition];
            }
            return decrypted;
        }
    }

    internal class KeywordCipher : IDecryptor
    {
        private static readonly List<char> Alphabet = new List<char>
        {
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'
        };
        private static readonly List<char> KeywordAlphabet = new List<char>
        {
            'b','r','e','a','k','l','m','n','o','p','q','s','t','u','v','w','x','y','z','c','d','f','g','h','i','j'
        };

        public string Encrypt(string message)
        {
            message = message.Replace(" ", "");
            string encrypted = "";

            foreach (var c in message.ToLower())
            {
                var position = Alphabet.IndexOf(c);
                encrypted += KeywordAlphabet[position];
            }
            return encrypted;
        }

        public string Decrypt(string message)
        {
            message = message.Replace(" ", "");
            string decrypted = "";

            foreach (var c in message.ToLower())
            {
                var position = KeywordAlphabet.IndexOf(c);
                decrypted += Alphabet[position];
            }
            return decrypted;
        }
    }

    internal class VignereCipher : IDecryptor
    {
        private static readonly List<char> Alphabet = new List<char>
        {
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'
        };
        private readonly Dictionary<char, List<char>> _square;
        private const string Key = "enigma";

        public VignereCipher()
        {
            _square = CreateSquare();
        }

        public string Encrypt(string message)
        {
            message = message.Replace(" ", "");

            string encrypted = "";
            int keyPosition = 0;

            foreach(var c in message)
            {
                var keyChar = Key[keyPosition++];
                var column = Alphabet.IndexOf(c);
                encrypted += _square[keyChar][column];
                if (keyPosition == Key.Length)
                {
                    keyPosition = 0;
                }
            }
            return encrypted;
        }

        public string Decrypt(string message)
        {
            message = message.Replace(" ", "");

            string decrypted = "";
            int keyPosition = 0;

            foreach (var c in message)
            {
                var keyChar = Key[keyPosition++];
                var column = _square[keyChar].IndexOf(c);
                decrypted += Alphabet[column];
                if (keyPosition == Key.Length)
                {
                    keyPosition = 0;
                }
            }
            return decrypted;
        }

        private Dictionary<char,List<char>> CreateSquare()
        {
            var square = new Dictionary<char, List<char>>();

            foreach(var c in Alphabet)
            {
                square.Add(c, new List<char> { c });
                var charPosition = Alphabet.IndexOf(c);
                var nextPosition = GetNextPosition(c);
                while(nextPosition != charPosition)
                {
                    var nextCharacter = Alphabet.ElementAt(nextPosition);
                    square[c].Add(nextCharacter);
                    nextPosition = GetNextPosition(nextCharacter);
                }
            }
            return square;
        }

        private int GetNextPosition(char c)
        {
            var charPosition = Alphabet.IndexOf(c);
            charPosition++;
            if(charPosition >= Alphabet.Count)
            {
                charPosition = charPosition - Alphabet.Count;
            }
            return charPosition;
        }
    }

    internal class DayOfWeekCipher : IDecryptor
    {
        private static readonly List<char> Alphabet = new List<char>
        {
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'
        };
        private readonly Dictionary<char, List<char>> _square;

        private readonly string _key;

        public DayOfWeekCipher()
        {
            _key = CreateKey();
            _square = CreateSquare();
        }

        public string Encrypt(string message)
        {
            message = message.Replace(" ", "");

            string encrypted = "";
            int keyPosition = 0;

            foreach (var c in message)
            {
                var keyChar = _key[keyPosition++];
                var column = Alphabet.IndexOf(c);
                encrypted += _square[keyChar][column];
                if (keyPosition == _key.Length)
                {
                    keyPosition = 0;
                }
            }
            return encrypted;
        }

        public string Decrypt(string message)
        {
            message = message.Replace(" ", "");

            string decrypted = "";
            int keyPosition = 0;

            foreach (var c in message)
            {
                var keyChar = _key[keyPosition++];
                var column = _square[keyChar].IndexOf(c);
                decrypted += Alphabet[column];
                if (keyPosition == _key.Length)
                {
                    keyPosition = 0;
                }
            }
            return decrypted;
        }

        private Dictionary<char, List<char>> CreateSquare()
        {
            var square = new Dictionary<char, List<char>>();

            foreach (var c in Alphabet)
            {
                square.Add(c, new List<char> { c });
                var charPosition = Alphabet.IndexOf(c);
                var nextPosition = GetNextPosition(c);
                while (nextPosition != charPosition)
                {
                    var nextCharacter = Alphabet.ElementAt(nextPosition);
                    square[c].Add(nextCharacter);
                    nextPosition = GetNextPosition(nextCharacter);
                }
            }
            return square;
        }

        private int GetNextPosition(char c)
        {
            var charPosition = Alphabet.IndexOf(c);
            charPosition++;
            if (charPosition >= Alphabet.Count)
            {
                charPosition = charPosition - Alphabet.Count;
            }
            return charPosition;
        }

        private string CreateKey()
        {
            var day = DateTime.UtcNow.DayOfWeek;
            return day.ToString().ToLower();
        }
    }
}
