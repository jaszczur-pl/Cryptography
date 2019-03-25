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
                Console.WriteLine(binaryText);
                Console.WriteLine(binaryKey);
            }

            string revertedString = binaryToString(binaryText);
            Console.WriteLine(revertedString);
        }

        static string stringToBinary(string str) {
            StringBuilder sb = new StringBuilder();
            byte[] buffer = Encoding.GetEncoding(28592).GetBytes(inputText);

            foreach (byte singleByte in buffer) {
                sb.Append(Convert.ToString(singleByte, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
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
    }
}
