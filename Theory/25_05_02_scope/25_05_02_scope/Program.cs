using System;

class Program
{
    static void Main()
    {
        // Scope is the region of code where a variable is accessible.
        // scope is defined by `{ }` — these are called blocks.
        // A variable exists only within the block where it was declared.
        // Inner blocks can access variables from outer blocks, 
        // but outer blocks cannot access variables from inner blocks.
        // Variables declared in the same block must have unique names.



        //Example 1: local scope
        int a = 10; // visible only inside Main
        {
            int b = 20;                         // only visible inside this block
            Console.WriteLine(a);
            Console.WriteLine(b + "\n");
        }
        // Console.WriteLine(b);                // Error: b does not exist here



        //Example 2: same variable name in separate blocks

        //1st scope level
        {
            // 2nd scope level 
            int x = 33;                         // allowed
        }

        {
            // 2nd scope level 
            int x = 44;                         // allowed, different block — no conflict
        }



        //Example 3: variable shadowing
        int number = 100;

        ShowNumber();                           // prints 200

        Console.WriteLine(number);              // 100



        Console.ReadLine();
    }


    static void ShowNumber()
    {
        int number = 200;                       //separate scope
        Console.WriteLine(number);
    }

}
