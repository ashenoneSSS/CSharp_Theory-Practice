using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_12_30_text_output
{
    class Program
    {
        static void Main(string[] args)
        {
            //two ways to put text to a new line


            //first way
            Console.WriteLine("         Firs Way");
            Console.WriteLine();
            Console.WriteLine("some text");
            Console.WriteLine();
            Console.WriteLine("some other text");


            //second way (by using escape sequences)
            Console.Write("\n\n\t Second Way \n\n");
            Console.Write("\'\'\'\'\'\'\'\'\'\'\'\'\'\'\'\'\' \n");
            Console.Write("+\t\t+ \n");
            Console.Write("+\t\t+ \n");
            Console.Write("+\t\t+ \n");
            Console.Write("\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\" \n");
            //btw \a is a sound siglal


            //character encoding standarts (UTF8 for examle)s
            Console.WriteLine("\n\n" + Console.OutputEncoding.HeaderName + "\n українська виглядає так");

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("\n" + Console.OutputEncoding.HeaderName + "\n тепер я можу писати українською\n\n\n\n");




            //Merry New Year))

            Console.Write("\t");
            string first_line = "        *        ";
            

            int central_spaces;

            Console.Write(first_line + "\n");

            for (int i = first_line.Length / 2 - 1; i > 0; i--)
            {
                Console.Write("\t");
                for (int j = 0; j < i; j++)
                {
                    Console.Write(" ");
                }

                Console.Write("*");

                central_spaces = (first_line.Length - 2) - i * 2;
                for (int j = 0; j < central_spaces; j++)
                {
                    Console.Write(" ");
                }

                Console.Write("*");

                Console.WriteLine();
            }

            Console.Write("\t");
            for (int i = 0; i < first_line.Length; i++)
            {
                Console.Write("*");
            }


        }
    }
}
