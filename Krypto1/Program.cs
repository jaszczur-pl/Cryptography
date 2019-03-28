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

        static int[,] tableS1 = new int[,] { { 14,  4,  13,  1,   2, 15,  11,  8,   3, 10,   6, 12,   5,  9,   0,  7 },
                                             {  0, 15,   7,  4,  14,  2,  13,  1,  10,  6,  12, 11,   9,  5,   3,  8 },
                                             {  4,  1,  14,  8,  13,  6,   2, 11,  15, 12,   9,  7,   3, 10,   5,  0 },
                                             { 15, 12,   8,  2,   4,  9,   1,  7,   5, 11,   3, 14,  10,  0,   6, 13 } };

        static int[,] tableS2 = new int[,] { { 15,  1,   8, 14,   6, 11,   3,  4,   9,  7,   2, 13,  12,  0,   5, 10 },
                                             {  3, 13,   4,  7,  15,  2,   8, 14,  12,  0,   1, 10,   6,  9,  11,  5 },
                                             {  0, 14,   7, 11,  10,  4,  13,  1,   5,  8,  12,  6,   9,  3,   2, 15 },
                                             { 13,  8,  10,  1,   3, 15,   4,  2,  11,  6,   7, 12,   0,  5,  14,  9} };

        static int[,] tableS3 = new int[,] { { 10,  0,   9, 14,   6,  3,  15,  5,   1, 13,  12,  7,  11,  4,   2,  8 },
                                             { 13,  7,   0,  9,   3,  4,   6, 10,   2,  8,   5, 14,  12, 11,  15,  1 },
                                             { 13,  6,   4,  9,   8, 15,   3,  0,  11,  1,   2, 12,   5, 10,  14,  7 },
                                             {  1, 10,  13,  0,   6,  9,   8,  7,   4, 15,  14,  3,  11,  5,   2, 12} };

        static int[,] tableS4 = new int[,] { {  7, 13,  14,  3,   0,  6,   9, 10,   1,  2,   8,  5,  11, 12,   4, 15 },
                                             { 13,  8,  11,  5,   6, 15,   0,  3,   4,  7,   2, 12,   1, 10,  14,  9 },
                                             { 10,  6,   9,  0,  12, 11,   7, 13,  15,  1,   3, 14,   5,  2,   8,  4 },
                                             {  3, 15,   0,  6,  10,  1,  13,  8,   9,  4,   5, 11,  12,  7,   2, 14 } };

        static int[,] tableS5 = new int[,] { {  2, 12,   4,  1,   7, 10,  11,  6,   8,  5,   3, 15,  13,  0,  14,  9 },
                                             { 14, 11,   2, 12,   4,  7,  13,  1,   5,  0,  15, 10,   3,  9,   8,  6 },
                                             {  4,  2,   1, 11,  10, 13,   7,  8,  15,  9,  12,  5,   6,  3,   0, 14 },
                                             { 11,  8,  12,  7,   1, 14,   2, 13,   6, 15,   0,  9,  10,  4,   5,  3 } };

        static int[,] tableS6 = new int[,] { { 12,  1,  10, 15,   9,  2,   6,  8,   0, 13,   3,  4,  14,  7,   5, 11 },
                                             { 10, 15,   4,  2,   7, 12,   9,  5,   6,  1,  13, 14,   0, 11,   3,  8 },
                                             {  9, 14,  15,  5,   2,  8,  12,  3,   7,  0,   4, 10,   1, 13,  11,  6 },
                                             {  4,  3,   2, 12,   9,  5,  15, 10,  11, 14,   1,  7,   6,  0,   8, 13 } };


        static int[,] tableS7 = new int[,] { {  4, 11,   2, 14,  15,  0,   8, 13,   3, 12,   9,  7,   5, 10,   6,  1 },
                                             { 13,  0,  11,  7,   4,  9,   1, 10,  14,  3,   5, 12,   2, 15,   8,  6 },
                                             {  1,  4,  11, 13,  12,  3,   7, 14,  10, 15,   6,  8,   0,  5,   9,  2 },
                                             {  6, 11,  13,  8,   1,  4,  10,  7,   9,  5,   0, 15,  14,  2,   3, 12 } };

        static int[,] tableS8 = new int[,] { { 13,  2,   8,  4,   6, 15,  11,  1,  10,  9,   3, 14,   5,  0,  12,  7 },
                                             {  1, 15,  13,  8,  10,  3,   7,  4,  12,  5,   6, 11,   0, 14,   9,  2 },
                                             {  7, 11,   4,  1,   9, 12,  14,  2,   0,  6,  10, 13,  15,  3,   5,  8 },
                                             {  2,  1,  14,  7,   4, 10,   8, 13,  15, 12,   9,  0,   3,  5,   6, 11 } };


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
