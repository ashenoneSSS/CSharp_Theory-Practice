using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_12_31_verbatim_string
{
    class Program
    {
        static void Main(string[] args)
        {
            //verbatim string - is string that ignore all escape sequences
            //it usefull for example for out put the Path "C:\Users\Admin\Desktop\Cs_projects" cause it has a lot of '\'


            //to use it put ' @ ' before ' " '
            Console.Write(@" C:\Users\Admin\Desktop\Cs_projects " + "\n\n");


            //way to put ' " ' into verbatim string is put two ' " ' 
            Console.Write(@" ""Some verbatim string"" " + "\n\n");


            //other way to use it is to easier output part of code
            Console.WriteLine("Example of code output");

            Console.Write(@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace try_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""Hello World!"");
        }
    }
}


");



            Console.WriteLine();
        }
    }
}
