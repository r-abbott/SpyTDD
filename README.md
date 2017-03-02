# SpyTDD
A TDD tutorial around processing encrypted messages.

Part 1:

You have been selected to develop decryption modules to help decrypt intercepted messages
 for the government spy agency. The agency has a tool already in place that intercepts 
 the messages,
however it needs to have the decryption loaded onto it to be effective. They want you to
 develop this using Test Driven Development.


- The decryption modules need to implement the IDecryptor interface, which as a single method of:
```csharp
string Decrypt(string encrypted)
```
- Develop a decryptor that will decrypt the following input:
```
brxkdyhsdvvhgwkhiluvwwhvw
```

into this output:
```
youhavepassedthefirsttest
```

- Our analysts believe they a simple cypher may be in play for some of the messages. Try using
 the alphabet and shifting the encrypted message characters by an amount.
- Use this Alphabet to aid you.

```csharp
	private static readonly List<char> Alphabet = new List<char>
	{
		'a','b','c','d','e','f','g','h','i','j',
		'k','l','m','n','o','p','q','r','s','t',
		'u','v','w','x','y','z'
	};
```

- Once you have the module working by proving the test passes, load it into the program and
 see what it comes up with.
```csharp
// Program.cs
	IDecryptor decryptor = null; // <- replace null with your new class.
```

Part 2:
Eurika! Your module has decrypted the first phrase of the message! Everyone is very excited 
about the results, however they realize that the other parts must be
encrypted using different cipher techniques. Some analysts believe there may be a keyword in 
the first phrase you decrypted.

The theory is that the keyword can be used with the original Alphabet along with another
Alphabet similar to the previous cipher, though the keyword takes the place of the
first x Alphabet characters. Then the next available Alphabet character starts, followed
by the remaining original order (with the ones used in the keyword excluded).

For example - if the keyword is triangle, the next letters would be f, h, j, k,... etc.

- This decryptor should take a keyword and the encrypted message as input, and return the 
decrypted output.
- Using this input and keyword:
```
triangle
wenvnipoawnvwfvipmqknwn
```
Will output:
```
thesecondtestiscomplete
```

- Once you have the module working by proving the test passes, load it into the program and 
see what it comes up with, the same as you did for Part 1.
```csharp
// Program.cs
	IDecryptor decryptor = null; // <- replace null with your new class.
```

Part 3
Excellent! The second phrase has been decrypted! However, there's been a snag: the first phrase 
is no longer being decrypted.
Since the uplink is already in place and can't be modified, we'll have to improvise a solution.

- Create a new module that implements IDecryptor, but can contain one or many Decryptors in it.
- The new decryptor should be able to decrypt both messages from Part 1 and Part 2
- It is okay to return multiple results, some of which are not correctly decrypted, as long as
 the real decrypted messages exist

```
Part 1 Input
brxkdyhsdvvhgwkhiluvwwhvw

Part 1 Output
youhavepassedthefirsttest
```
```
Part 2 Input
triangle
wenvnipoawnvwfvipmqknwn

Part 2 Output
thesecondtestiscomplete
```

Part 4
Great, now when a new decryptor is created, we can simply add it to the list of decryptors and see
what works. While you were developing that, our analysts theorized that multiple Alphabetic ciphers
could be used to generate the encrypted message. Some have even used the term "square" and drawn
it on a whiteboard to visually understand. They theorize that this grid along with a key can
be used to decrypt a message by cycling through each letter of the message alongside each
letter in the key to find the column and row.

```
A B C D E F G H I J K L M N O P Q R S T U V W X Y Z
B C D E F G H I J K L M N O P Q R S T U V W X Y Z A
C D E F G H I J K L M N O P Q R S T U V W X Y Z A B
D E F G H I J K L M N O P Q R S T U V W X Y Z A B C
E F G H I J K L M N O P Q R S T U V W X Y Z A B C D
F G H I J K L M N O P Q R S T U V W X Y Z A B C D E
G H I J K L M N O P Q R S T U V W X Y Z A B C D E F
H I J K L M N O P Q R S T U V W X Y Z A B C D E F G
I J K L M N O P Q R S T U V W X Y Z A B C D E F G H
J K L M N O P Q R S T U V W X Y Z A B C D E F G H I
K L M N O P Q R S T U V W X Y Z A B C D E F G H I J
L M N O P Q R S T U V W X Y Z A B C D E F G H I J K
M N O P Q R S T U V W X Y Z A B C D E F G H I J K L
N O P Q R S T U V W X Y Z A B C D E F G H I J K L M
O P Q R S T U V W X Y Z A B C D E F G H I J K L M N
P Q R S T U V W X Y Z A B C D E F G H I J K L M N O
Q R S T U V W X Y Z A B C D E F G H I J K L M N O P
R S T U V W X Y Z A B C D E F G H I J K L M N O P Q
S T U V W X Y Z A B C D E F G H I J K L M N O P Q R
T U V W X Y Z A B C D E F G H I J K L M N O P Q R S
U V W X Y Z A B C D E F G H I J K L M N O P Q R S T
V W X Y Z A B C D E F G H I J K L M N O P Q R S T U
W X Y Z A B C D E F G H I J K L M N O P Q R S T U V
X Y Z A B C D E F G H I J K L M N O P Q R S T U V W
Y Z A B C D E F G H I J K L M N O P Q R S T U V W X
Z A B C D E F G H I J K L M N O P Q R S T U V W X Y

```
They believe that, similar to how Part 2 was solved, that a keyword may exist in the previous 
decrypted phrase.

- Develop a decryptor that uses this "square" and a keyword.

Input:
```
flttvvwitcxhnhpxbtkicnpvlzupykxigkmd
```
Output:
```
multiplealphabeticciphersimplemented
```

- Once you have the module working by proving the test passes, add it to your list of ciphers
and run the program again.

Part 5
The entire message is within our grasp! Only one more message to be decrypted. There appears to
be a pattern here: each previously decrypted phrase contains a keyword to help decrypt the next
message. After some analysis, our analysts believe the last phrase follows the same algorithm
as Part 4, however they have tried all the words in the phrase to no avail. Perhaps there
is some hidden meaning in the message itself?

- Decrypt the last message.
