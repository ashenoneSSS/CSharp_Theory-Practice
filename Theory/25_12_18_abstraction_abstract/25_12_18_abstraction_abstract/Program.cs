using System;

namespace _25_09_17_Abstraction_AbstractClasses
{
    // ===========================
    // ABSTRACTION (OOP concept) + ABSTRACT CLASSES
    // ===========================
    // - Abstraction means: expose only the important behavior, hide internal details
    // - Abstract class is a "template" for derived classes
    // - You cannot create an object of an abstract class directly BUT u can create variable of abstract class
    // - Abstract classes are good when you want
    //   - shared state (fields) inside the base class
    //   - shared base logic (implemented methods)
    //   - required members (abstract methods/properties) that derived must implement
    //
    // Method differences:
    // - normal method (non-virtual) cannot be overridden
    // - virtual method has a base implementation and can be overridden optionally
    // - abstract method has NO implementation and MUST be overridden in non-abstract derived class

    // ===========================
    // Abstract base class
    // ===========================
    abstract class BankAccount
    {
        // - Shared state (this is a big reason to prefer abstract class over interface here)
        protected decimal balance;

        // - Abstract property: derived must provide implementation
        public abstract string AccountType { get; }

        // - Normal property with protected set:
        // - derived can set it, but external code cannot
        public string AccountNumber { get; protected set; }

        // - Get-only property (base calculates / exposes common state)
        public decimal Balance
        {
            get { return balance; }
        }

        // - Base constructor can initialize shared state
        public BankAccount(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            balance = initialBalance < 0 ? 0 : initialBalance;
        }

        // - Non-abstract (normal) method: same for all accounts
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit must be positive");
                return;
            }

            balance += amount;
        }

        // - Virtual method: base provides default, derived may override
        public virtual decimal MonthlyFee()
        {
            return 0m;
        }

        // - Abstract method: must be implemented in derived (non-abstract) class
        public abstract bool Withdraw(decimal amount);

        // - Non-abstract method that uses abstract members (template method style)
        public void PrintReport()
        {
            Console.WriteLine($"Type: {AccountType}, Number: {AccountNumber}, Balance: {Balance}, Fee: {MonthlyFee()}");
        }
    }

    // ===========================
    // Non-abstract derived class
    // ===========================
    class SavingsAccount : BankAccount
    {
        // - Extra state specific to SavingsAccount
        private decimal minimumBalance;

        // - Implement abstract property
        public override string AccountType
        {
            get { return "Savings"; }
        }

        public SavingsAccount(string accountNumber, decimal initialBalance, decimal minimumBalance): base(accountNumber, initialBalance)
        {
            this.minimumBalance = minimumBalance < 0 ? 0 : minimumBalance;
        }

        // - Must implement abstract method
        public override bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdraw must be positive");
                return false;
            }

            if (balance - amount < minimumBalance)
            {
                Console.WriteLine("Cannot withdraw: would go below minimum balance");
                return false;
            }

            balance -= amount;
            return true;
        }

        // - Optional override of virtual method
        public override decimal MonthlyFee()
        {
            return 0m;
        }
    }

    // ===========================
    // Another non-abstract derived class
    // ===========================
    class CheckingAccount : BankAccount
    {
        private decimal monthlyFee;

        public override string AccountType
        {
            get { return "Checking"; }
        }

        public CheckingAccount(string accountNumber, decimal initialBalance, decimal monthlyFee): base(accountNumber, initialBalance)
        {
            this.monthlyFee = monthlyFee < 0 ? 0 : monthlyFee;
        }

        public override bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdraw must be positive");
                return false;
            }

            if (amount > balance)
            {
                Console.WriteLine("Cannot withdraw: insufficient funds");
                return false;
            }

            balance -= amount;
            return true;
        }

        public override decimal MonthlyFee()
        {
            return monthlyFee;
        }
    }

    // ===========================
    // Abstract derived class (still abstract)
    // ===========================
    // - Shows that an abstract class can inherit from another abstract class
    // - It may implement some abstract members, but can keep others abstract
    abstract class CreditAccount : BankAccount
    {
        protected decimal creditLimit;

        public override string AccountType
        {
            get { return "Credit"; }
        }

        public CreditAccount(string accountNumber, decimal initialBalance, decimal creditLimit): base(accountNumber, initialBalance)
        {
            this.creditLimit = creditLimit < 0 ? 0 : creditLimit;
        }

        // - Still abstract here, so CreditAccount stays abstract
        // - Derived non-abstract class must implement it
        public abstract override bool Withdraw(decimal amount);

        // - Can override virtual method and provide a shared rule for all credit accounts
        public override decimal MonthlyFee()
        {
            return 50m;
        }
    }

    // ===========================
    // Non-abstract derived from abstract derived
    // ===========================
    class BasicCreditAccount : CreditAccount
    {
        public BasicCreditAccount(string accountNumber, decimal initialBalance, decimal creditLimit): base(accountNumber, initialBalance, creditLimit)
        {
        }

        public override bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdraw must be positive");
                return false;
            }

            // - Credit logic: you can go below 0 down to -creditLimit
            if (balance - amount < -creditLimit)
            {
                Console.WriteLine("Cannot withdraw: credit limit exceeded");
                return false;
            }

            balance -= amount;
            return true;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // BankAccount x = new BankAccount("A1", 100); // ERROR: cannot create abstract class object

            SavingsAccount s = new SavingsAccount("S-100", 500, minimumBalance: 200);
            CheckingAccount c = new CheckingAccount("C-200", 300, monthlyFee: 20);
            BasicCreditAccount cr = new BasicCreditAccount("CR-300", 0, creditLimit: 1000);

            // - Upcasting: base class variable can store derived object
            // - This is where polymorphism works with abstract/virtual members
            BankAccount a1 = s;
            BankAccount a2 = c;
            BankAccount a3 = cr;

            a1.PrintReport();
            a2.PrintReport();
            a3.PrintReport();

            Console.WriteLine();

            a1.Deposit(100);
            a2.Withdraw(50);
            a3.Withdraw(700);

            a1.PrintReport();
            a2.PrintReport();
            a3.PrintReport();

            Console.ReadLine();
        }
    }
}

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/objects
