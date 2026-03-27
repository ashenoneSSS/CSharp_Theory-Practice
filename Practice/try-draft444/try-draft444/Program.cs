using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_12_30_data_types_literals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> sourse_list = new List<int>() {1,5,2,8,3,7};

            IEnumerable sourse_enumerable = sourse_list;
            IEnumerator sourse_enumerator = sourse_enumerable.GetEnumerator();

            

            while(sourse_enumerator.MoveNext())
            {
                Console.WriteLine(sourse_enumerator.Current);
            }






        }
    }
}


/*
 * 
 * 
 * 
 * List<int> sourse_list = new List<int>() { 1,2,3,4,5,6,7};

            List<int> new_list = sourse_list.Where(x => x % 3 == 0).ToList();


            foreach(int element in sourse_list)
            {
                Console.Write(element + "    ");
            }
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */


