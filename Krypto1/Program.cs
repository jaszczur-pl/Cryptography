using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypto1
{
    class Program
    {
        static string inputText;
        static string inputKey;
        static string permutedPC1Key;
        static string permutedIPText;
        static List<string> subKeysPC1 = new List<string>();
        static List<string> keysPC2 = new List<string>();

        static int[] tablePC1 = new int[]  {57, 49, 41, 33, 25, 17,  9,
                                             1, 58, 50, 42, 34, 26, 18,
                                            10,  2, 59, 51, 43, 35, 27,
                                            19, 11,  3, 60, 52, 44, 36,
                                            63, 55, 47, 39, 31, 23, 15,
                                             7, 62, 54, 46, 38, 30, 22,
                                            14,  6, 61, 53, 45, 37, 29,
                                            21, 13,  5, 28, 20, 12,  4};

        static int[] tablePC2 = new int[] {14, 17, 11, 24,  1,  5,
                                            3, 28, 15,  6, 21, 10,
                                           23, 19, 12,  4, 26,  8,
                                           16,  7, 27, 20, 13,  2,
                                           41, 52, 31, 37, 47, 55,
                                           30, 40, 51, 45, 33, 48,
                                           44, 49, 39, 56, 34, 53,
                                           46, 42, 50, 36, 29, 32};

        static int[] tableIP = new int[] {58, 50, 42, 34, 26, 18, 10,  2,
                                          60, 52, 44, 36, 28, 20, 12,  4,
                                          62, 54, 46, 38, 30, 22, 14,  6,
                                          64, 56, 48, 40, 32, 24, 16,  8,
                                          57, 49, 41, 33, 25, 17,  9,  1,
                                          59, 51, 43, 35, 27, 19, 11,  3,
                                          61, 53, 45, 37, 29, 21, 13,  5,
                                          63, 55, 47, 39, 31, 23, 15,  7};

        static int[] tableEBit = new int[] {32,  1,  2,  3,  4,  5,
                                             4,  5,  6,  7,  8,  9,
                                             8,  9, 10, 11, 12, 13,
                                            12, 13, 14, 15, 16, 17,
                                            16, 17, 18, 19, 20, 21,
                                            20, 21, 22, 23, 24, 25,
                                            24, 25, 26, 27, 28, 29,
                                            28, 29, 30, 31, 32,  1};

        static int[] tableP = new int[] {16,  7, 20, 21,
                                         29, 12, 28, 17,
                                          1, 15, 23, 26,
                                          5, 18, 31, 10,
                                          2,  8, 24, 14,
                                         32, 27,  3,  9,
                                         19, 13, 30,  6,
                                         22, 11,  4, 25 };

        static string[,] tableS1 = new string[,] { { "1110",  "0100",  "1101",  "0001",   "0010", "1111",  "1011",  "1000",   "0011", "1010",   "0110", "1100",   "0101",  "1001",   "0000",  "0111" },
                                             {  "0000", "1111",   "0111",  "0100",  "1110",  "0010",  "1101",  "0001",  "1010",  "0110",  "1100", "1011",   "1001",  "0101",   "0011",  "1000" },
                                             {  "0100",  "0001",  "1110",  "1000",  "1101",  "0110",   "0010", "1011",  "1111", "1100",   "1001",  "0111",   "0011", "1010",   "0101",  "0000" },
                                             { "1111", "1100",   "1000",  "0010",   "0100",  "1001",   "0001",  "0111",   "0101", "1011",   "0011", "1110",  "1010",  "0000",   "0110", "1101" } };

        static string[,] tableS2 = new string[,] { { "1111",  "0001",   "1000", "1110",   "0110", "1011",   "0011",  "0100",   "1001",  "0111",   "0010", "1101",  "1100",  "0000",   "0101", "1010" },
                                             {  "0011", "1101",   "0100",  "0111",  "1111",  "0010",   "1000", "1110",  "1100",  "0000",   "0001", "1010",   "0110",  "1001",  "1011",  "0101" },
                                             {  "0000", "1110",   "0111", "1011",  "1010",  "0100",  "1101",  "0001",   "0101",  "1000",  "1100",  "0110",   "1001",  "0011",   "0010", "1111" },
                                             { "1101",  "1000",  "1010",  "0001",   "0011", "1111",   "0100",  "0010",  "1011",  "0110",   "0111", "1100",   "0000",  "0101",  "1110",  "1001"} };

        static string[,] tableS3 = new string[,] { { "1010",  "0000",   "1001", "1110",   "0110",  "0011",  "1111",  "0101",   "0001", "1101",  "1100",  "0111",  "1011",  "0100",   "0010",  "1000" },
                                             { "1101",  "0111",   "0000",  "1001",   "0011",  "0100",   "0110", "1010",   "0010",  "1000",   "0101", "1110",  "1100", "1011",  "1111",  "0001" },
                                             { "1101",  "0110",   "0100",  "1001",   "1000", "1111",   "0011",  "0000",  "1011",  "0001",   "0010", "1100",   "0101", "1010",  "1110",  "0111" },
                                             {  "0001", "1010",  "1101",  "0000",   "0110",  "1001",   "1000",  "0111",   "0100", "1111",  "1110",  "0011",  "1011",  "0101",   "0010", "1100"} };

        static string[,] tableS4 = new string[,] { {  "0111", "1101",  "1110",  "0011",   "0000",  "0110",   "1001", "1010",   "0001",  "0010",   "1000",  "0101",  "1011", "1100",   "0100", "1111" },
                                             { "1101",  "1000",  "1011",  "0101",   "0110", "1111",   "0000",  "0011",   "0100",  "0111",   "0010", "1100",   "0001", "1010",  "1110",  "1001" },
                                             { "1010",  "0110",   "1001",  "0000",  "1100", "1011",   "0111", "1101",  "1111",  "0001",   "0011", "1110",   "0101",  "0010",   "1000",  "0100" },
                                             {  "0011", "1111",   "0000",  "0110",  "1010",  "0001",  "1101",  "1000",   "1001",  "0100",   "0101", "1011",  "1100",  "0111",   "0010", "1110" } };

        static string[,] tableS5 = new string[,] { {  "0010", "1100",   "0100",  "0001",   "0111", "1010",  "1011",  "0110",   "1000",  "0101",   "0011", "1111",  "1101",  "0000",  "1110",  "1001" },
                                             { "1110", "1011",   "0010", "1100",   "0100",  "0111",  "1101",  "0001",   "0101",  "0000",  "1111", "1010",   "0011",  "1001",   "1000",  "0110" },
                                             {  "0100",  "0010",   "0001", "1011",  "1010", "1101",   "0111",  "1000",  "1111",  "1001",  "1100",  "0101",   "0110",  "0011",   "0000", "1110" },
                                             { "1011",  "1000",  "1100",  "0111",   "0001", "1110",   "0010", "1101",   "0110", "1111",   "0000",  "1001",  "1010",  "0100",   "0101",  "0011" } };

        static string[,] tableS6 = new string[,] { { "1100",  "0001",  "1010", "1111",   "1001",  "0010",   "0110",  "1000",   "0000", "1101",   "0011",  "0100",  "1110",  "0111",   "0101", "1011" },
                                             { "1010", "1111",   "0100",  "0010",   "0111", "1100",   "1001",  "0101",   "0110",  "0001",  "1101", "1110",   "0000", "1011",   "0011",  "1000" },
                                             {  "1001", "1110",  "1111",  "0101",   "0010",  "1000",  "1100",  "0011",   "0111",  "0000",   "0100", "1010",   "0001", "1101",  "1011",  "0110" },
                                             {  "0100",  "0011",   "0010", "1100",   "1001",  "0101",  "1111", "1010",  "1011", "1110",   "0001",  "0111",   "0110",  "0000",   "1000", "1101" } };


        static string[,] tableS7 = new string[,] { {  "0100", "1011",   "0010", "1110",  "1111",  "0000",   "1000", "1101",   "0011", "1100",   "1001",  "0111",   "0101", "1010",   "0110",  "0001" },
                                             { "1101",  "0000",  "1011",  "0111",   "0100",  "1001",   "0001", "1010",  "1110",  "0011",   "0101", "1100",   "0010", "1111",   "1000",  "0110" },
                                             {  "0001",  "0100",  "1011", "1101",  "1100",  "0011",   "0111", "1110",  "1010", "1111",   "0110",  "1000",   "0000",  "0101",   "1001",  "0010" },
                                             {  "0110", "1011",  "1101",  "1000",   "0001",  "0100",  "1010",  "0111",   "1001",  "0101",   "0000", "1111",  "1110",  "0010",   "0011", "1100" } };

        static string[,] tableS8 = new string[,] { { "1101",  "0010",   "1000",  "0100",   "0110", "1111",  "1011",  "0001",  "1010",  "1001",   "0011", "1110",   "0101",  "0000",  "1100",  "0111" },
                                             {  "0001", "1111",  "1101",  "1000",  "1010",  "0011",   "0111",  "0100",  "1100",  "0101",   "0110", "1011",   "0000", "1110",   "1001",  "0010" },
                                             {  "0111", "1011",   "0100",  "0001",   "1001", "1100",  "1110",  "0010",   "0000",  "0110",  "1010", "1101",  "1111",  "0011",   "0101",  "1000" },
                                             {  "0010",  "0001",  "1110",  "0111",   "0100", "1010",   "1000", "1101",  "1111", "1100",   "1001",  "0000",   "0011",  "0101",   "0110", "1011" } };


        static int[] tableIPReverse = new int[] {40,  8, 48, 16, 56, 24, 64, 32,
                                                 39,  7, 47, 15, 55, 23, 63, 31,
                                                 38,  6, 46, 14, 54, 22, 62, 30,
                                                 37,  5, 45, 13, 53, 21, 61, 29,
                                                 36,  4, 44, 12, 52, 20, 60, 28,
                                                 35,  3, 43, 11, 51, 19, 59, 27,
                                                 34,  2, 42, 10, 50, 18, 58, 26,
                                                 33,  1, 41,  9, 49, 17, 57, 25};
   
        static int[] leftShifts = new int[] { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        static void Main(string[] args) {
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("* Podstawy kryptografii - szyfrowanie/deszyfrowanie algorytmem DES*");
            Console.WriteLine("* Maciej Jaszczura     211808                                     *");
            Console.WriteLine("* Małgorzata Kucharska 211808                                     *");
            Console.WriteLine("* Ewa Maszkowska       211808                                     *");
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("");

            Console.WriteLine("Podaj tekst do zaszyfrowania algorytmem DES");
            inputText = Console.ReadLine();

            Console.WriteLine("Podaj 64-bitowy klucz w postaci heksadecymalnej");
            inputKey = Console.ReadLine();

            string binaryText = stringToBinary(inputText);
            string binaryKey = hexStringToBinary(inputKey);

            if (binaryKey.Count() != 64) {
                Console.WriteLine("Wpisano klucz o niepoprawnej długości");
            }
            else {
                // create 16 subkeys, each of which is 48-bits long
                permutedPC1Key = performPermutation(binaryKey, tablePC1);
                subKeysPC1.Add(permutedPC1Key);

                foreach (int shift in leftShifts) {
                    string prevKey = subKeysPC1.Last();
                    subKeysPC1.Add(createSubKeys(prevKey, shift));

                    string keyPC2 = performPermutation(subKeysPC1.Last(), tablePC2);
                    keysPC2.Add(keyPC2);
                }

                //Encode each 64-bit block of data
                permutedIPText = performPermutation(binaryText.Substring(0, 64), tableIP);
                int bitCounter = 64;
                while (permutedIPText.Length != binaryText.Length) {
                    string nextPartOfPermutedIPText = performPermutation(binaryText.Substring(bitCounter, 64), tableIP);

                    permutedIPText += nextPartOfPermutedIPText;
                    bitCounter += 64;
                }

                Console.WriteLine(binaryText);
                Console.WriteLine("");
                Console.WriteLine(permutedIPText);


            }
        }

        static string stringToBinary(string str) {
            StringBuilder sb = new StringBuilder();
            byte[] buffer = Encoding.GetEncoding(28592).GetBytes(inputText);

            foreach (byte singleByte in buffer) {
                sb.Append(Convert.ToString(singleByte, 2).PadLeft(8, '0'));
            }

            string text = sb.ToString();

            while (text.Length %64 != 0) {
                text += "0";
            }

            return text;
        }

        static string hexStringToBinary(string str) {
            string temp = Convert.ToString(Convert.ToInt64(str, 16), 2);
            int numberOfBits = temp.Count();

            while ((numberOfBits % 4) != 0) {
                temp = temp.Insert(0, "0");
                numberOfBits = temp.Count();
            }

            if (str.ElementAt(0) == '0' && str.Count() > 1) {
                temp = temp.Insert(0, "0000");
            }

            return temp;
        }

        static string binaryToString(string str) {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < str.Length; i += 8) {
                byteList.Add(Convert.ToByte(str.Substring(i, 8), 2));
            }
            return Encoding.GetEncoding(28592).GetString(byteList.ToArray());
        }

        static string createSubKeys(string key, int shift) {

            string subKeyA = key.Substring(0, 28);
            string subKeyB = key.Substring(28, 28);
            char firstBitA = subKeyA.ElementAt(0);
            char firstBitB = subKeyB.ElementAt(0);
            char secondBitA = subKeyA.ElementAt(1);
            char secondBitB = subKeyB.ElementAt(1);

            StringBuilder sbA = new StringBuilder(subKeyA);
            StringBuilder sbB = new StringBuilder(subKeyB);

            for (int i = 0; i < 28 - shift; i++) {
                sbA[i] = sbA[i + shift];
                sbB[i] = sbB[i + shift];
            }

            if (shift == 1) {
                sbA[27] = firstBitA;
                sbB[27] = firstBitB;
            }
            else if (shift == 2) {
                sbA[26] = firstBitA;
                sbA[27] = secondBitA;
                sbB[26] = firstBitB;
                sbB[27] = secondBitB;
            }

            string transformedKey = sbA.ToString() + sbB.ToString();

            return transformedKey;
        }

        static string performPermutation(string key, int[] table) {
            string permutedKey = "";

            foreach(int i in table) {
               permutedKey += key.ElementAt(i-1);
            }

            return permutedKey;
        }
    }
}
