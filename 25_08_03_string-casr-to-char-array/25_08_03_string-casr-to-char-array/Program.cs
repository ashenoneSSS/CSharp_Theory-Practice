using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_08_02_Methods
{
    internal class Program
    {

        static string ReverseStr(string x)
        {
            char[] rev_chars = new char[x.Length];

            for (int i = 0; i < x.Length; i++)
            {
                rev_chars[i] = x[x.Length - i - 1];
            }

            string reversed = new string(rev_chars);

            return reversed;
        }

        static string AlternativeReverse(string x)
        {
            string reversed = "";
            for (int i = x.Length - 1; i >= 0; i--)
            {
                reversed += x[i];
            }
            return reversed;
        }

        static void Main(string[] args)
        {
            string f = Console.ReadLine();

            string w = ReverseStr(f);

            Console.WriteLine(w);





            Console.ReadLine();
        }
    }
}
