using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_08_02_Methods
{
    internal class Program
    {
        // parameter is the name inside the method definition that receives a value when the method is called
        // here: 'variable' is a parameter
        static void ChangeVariable(int variable)
        {
            // ints are value types → the value is copied into the parameter
            // changing this local copy does NOT affect the caller's variable
            variable = 10;
        }


        static void ChangeElement(int[] array)
        {
            // When reference-type passed to a method, a copy of that reference is made.
            // Both references point to the same array, so element changes are visible outside.
            array[0] = 10;
        }


        // Example 1
        static int MostFrequent(int[] arr)
        {
            int f = 0, k = 0, mk = 0, ni = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                f = arr[i];
                k = 0;
                for (int j = 0; j < arr.Length; j++)
                {
                    if (arr[j] == f)
                    {
                        k++;
                    }
                }
                if (mk < k)
                {
                    mk = k;
                    ni = i;
                }
            }

            return arr[ni];
        }

        static void Main(string[] args)
        {
            int var = 0;                // argument is the actual value you pass to a method call (here: 'var')
            int[] arr = { 0 };          // argument can also be a reference-type instance (here: 'arr')

            Console.WriteLine("--- Before ---");
            Console.WriteLine("variable = " + var);
            Console.WriteLine("array[0] = " + arr[0] + "\n");

            // argument 'variable' (value 0) is copied into the parameter 'variable' of ChangeVariable
            // the method changes only its local copy → caller's 'variable' stays 0
            ChangeVariable(var);

            // argument 'array' (a reference) is copied into the parameter 'array' of ChangeElement
            // both references point to the SAME array object → mutating elements is visible to the caller
            ChangeElement(arr);

            Console.WriteLine("--- After ---");
            Console.WriteLine("variable = " + var);
            Console.WriteLine("array[0] = " + arr[0] + " \n\n");




            //Practice (Example 1)

            Console.WriteLine("enter size:");
            int n = int.Parse(Console.ReadLine());

            int[] array = new int[n];
            Console.WriteLine("Enter elements:");
            for (int i = 0; i < n; i++)
            {
                array[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("\nMost frequent is:  " + MostFrequent(array));

            Console.ReadLine();
        }
    }
}
