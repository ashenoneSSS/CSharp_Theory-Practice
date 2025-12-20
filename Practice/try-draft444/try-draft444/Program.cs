using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

class Program
{
    static public void function(int a, int b)
    {
        int c = a + b;
        string f = c.ToString();

        Console.WriteLine(f);
    }

    static void Main(string[] args)
    {
        Action<int, int> RefToFunc = (int a, int b) =>
        {
            int c = a + b;
            string f = c.ToString();

            Console.WriteLine(f);
        };

        RefToFunc(4, 6);
        
    }
}
