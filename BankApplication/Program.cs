using System;
using BankLibrary;

namespace BankApplication
{
    class Program
    {
        static void Main (string[] args)
        {
            Bank<Account> bank = new Bank<Account> ("Bank");
            bool alive = true;
            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen; 

                Console.WriteLine ("\n1. Open account \t 2. Withdraw funds \t 3. Add funds");
                Console.WriteLine ("4. Close the account \t 5. Skip the day \t 6. Exit the program");
                Console.Write ("\nSpecify the desired item: ");

                Console.ForegroundColor = color;

                try
                {
                    int command = Convert.ToInt32 (Console.ReadLine ());

                    switch (command)
                    {
                        case 1:
                            OpenAccount (bank);
                            break;

                        case 2:
                            Withdraw (bank);
                            break;

                        case 3:
                            Put (bank);
                            break;

                        case 4:
                            CloseAccount (bank);
                            break;

                        case 5:
                            break;

                        case 6:
                            alive = false;
                            continue;                    

                    }
                    bank.CalculatePercentage ();
                }
                catch (Exception ex)
                {
                    //Display error message in red
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine (ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }

        private static void OpenAccount (Bank<Account> bank)
        {
            Console.Write ("\nInitial amount on account: ");

            decimal sum = Convert.ToDecimal (Console.ReadLine ());
            Console.WriteLine ("\nSelect an account type: 1.Demand 2.Deposit ");
            AccountType accountType;

            int type = Convert.ToInt32 (Console.ReadLine ());

            if (type == 2)
                accountType = AccountType.Deposit;
            else
                accountType = AccountType.Ordinary;

            bank.Open (accountType,
                sum,
                AddSumHandler,
                WithdrawSumHandler,
                (o, e) => Console.WriteLine (e.Message), 
                CloseAccountHandler, 
                OpenAccountHandler); 
        }

        private static void Withdraw (Bank<Account> bank)
        {
            Console.Write ("\nSpecify the amount to withdraw from the account: ");

            decimal sum = Convert.ToDecimal (Console.ReadLine ());
            Console.Write ("\nEnter the account id: ");
            int id = Convert.ToInt32 (Console.ReadLine ());

            bank.Withdraw (sum, id);
        }

        private static void Put (Bank<Account> bank)
        {
            Console.Write ("\nSpecify the amount to put on the account: ");
            decimal sum = Convert.ToDecimal (Console.ReadLine ());
            Console.Write ("\nEnter the account id: ");
            int id = Convert.ToInt32 (Console.ReadLine ());
            bank.Put (sum, id);
        }

        private static void CloseAccount (Bank<Account> bank)
        {
            Console.Write ("\nEnter the account id to close: ");
            int id = Convert.ToInt32 (Console.ReadLine ());

            bank.Close (id);
        }

      
        private static void OpenAccountHandler (object sender, AccountEventArgs e)
        {
            Console.WriteLine (e.Message);
        }

        private static void AddSumHandler (object sender, AccountEventArgs e)
        {
            Console.WriteLine (e.Message);
        }
       
        private static void WithdrawSumHandler (object sender, AccountEventArgs e)
        {
            Console.WriteLine (e.Message);
            if (e.Sum > 0)
                Console.WriteLine ("\nWe go to spend money");
        }
        
        private static void CloseAccountHandler (object sender, AccountEventArgs e)
        {
            Console.WriteLine (e.Message);
        }
    }
}