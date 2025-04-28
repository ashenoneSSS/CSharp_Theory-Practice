using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace try_draft444
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int fn2 = 0, fn1 = 1, fn = 1;

            while (fn2<=74000)
            {
                fn2 = fn1+fn;

                
                fn = fn1;
                fn1 = fn2;


            }

            Console.Write(fn2 + "  ");

            Console.ReadLine();
        }
    }
}
