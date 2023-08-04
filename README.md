# ISAACDotNet
C# Implementation of the ISAAC stream cipher.

# What is ISAAC cipher?
ISAAC is a cryptographically secure pseudo-random number generator (CSPRNG) and stream cipher. It was developed by Bob Jenkins from 1993 (http://burtleburtle.net/bob/rand/isaac.html) and placed in the Public Domain. ISAAC is fast - especially when optimised - and portable to most architectures in nearly all programming and scripting languages. It is also simple and succinct, using as it does just two 256-word arrays for its state.

ISAAC stands for "Indirection, Shift, Accumulate, Add, and Count" which are the principal bitwise operations employed. To date - and that's after more than 20 years of existence - ISAAC has not been broken (unless GCHQ or NSA did it, but they wouldn't be telling). ISAAC thus deserves a lot more attention than it has hitherto received and it would be salutary to see it more universally implemented.

# Usecase example

```cs
   
//Creating a seed
int[] seed = new int[] { 2, 5, 1, 6 };

//ISAAC Ciphers for decoding/encoding bytes
var writeCipher = new ISAACCipher(seed);
var readCipher = new ISAACCipher(seed);

//Dummy byte 28 
byte dummyByte = 28;

//Encoding dummyByte with ISAAC
byte isaacByte = (byte)((dummyByte + writeCipher.Value()) & 0xff);
Console.WriteLine($"ISAAC encoded byte: {isaacByte}"); // Output: ISAAC encoded byte: 110

//Decoding dummyByte with ISAAC
var decodedValue = (isaacByte - readCipher.Value()) & 0xff;

Console.WriteLine($"ISAAC decoded value: {decodedValue}"); // Output: ISAAC decoded value: 28 (same as our dummyByte)
```
