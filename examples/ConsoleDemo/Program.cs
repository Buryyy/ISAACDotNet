//Creating a seed
int[] seed = new int[] { 2, 5, 1, 6 };

//ISAAC Ciphers
var writeCipher = new ISAACCipher(seed);
var readCipher = new ISAACCipher(seed);

//Dummy byte 28 
byte dummyByte = 28;

//Encoding dummyByte with ISAAC
byte isaacByte = (byte)((dummyByte + writeCipher.Value()) & 0xff);
Console.WriteLine($"ISAAC encoded byte: {isaacByte}"); //Output: ISAAC encoded byte: 110

//Decoding dummyByte with ISAAC
var decodedValue = (isaacByte - readCipher.Value()) & 0xff;

Console.WriteLine($"ISAAC decoded value: {decodedValue}"); //Output: ISAAC decoded value: 28 (same as our dummyByte)

Console.ReadKey();