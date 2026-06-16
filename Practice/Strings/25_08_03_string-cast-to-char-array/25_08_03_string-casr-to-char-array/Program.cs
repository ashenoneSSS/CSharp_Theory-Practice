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
            // Example 1 (Replacing backslash TO slash)
            Console.WriteLine("Example 1");

            Console.WriteLine("back_slash_TO_slash");
            string x = Console.ReadLine();

            char[] a = x.ToCharArray();         // Syntax: somestring.ToToCharArray()

            for (int i = 0; i < x.Length; i++)
            {
                if (a[i] == '\\')
                {
                    a[i] = '/';
                }
            }

            for (int i = 0; i < x.Length; i++)
            {
                Console.Write(a[i]);
            }



            // Example 2 (Reverse string)
            Console.WriteLine("\n\nExample 2");

            string f = Console.ReadLine();
            string w = ReverseStr(f);           // Methods above

            Console.WriteLine(w);






            Console.ReadLine();
        }
    }
}
