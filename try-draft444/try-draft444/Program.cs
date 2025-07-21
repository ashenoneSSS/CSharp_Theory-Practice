using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace try_draft444
{
    internal class Program
    {
        static bool isPrime(int n)
        {
            for (int i = 3; i < n; i++)
            {
                if (n% i == 0)
                    return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            

            for (int i = 2; i < 100; i++)
            {
                if(isPrime(i) == true)
                {
                    Console.WriteLine(i);
                }
            }

            Console.ReadLine();
        }
    }
}
