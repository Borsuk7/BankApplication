using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class Bank<T> where T : Account
    {
        List<T> accounts = new List<T> ();

        public string Name { get; }

        public Bank (string name)
        {
            Name = name;
        }

        public void Open (AccountType accountType, decimal sum,
            AccountStateHandler addSumHandler, AccountStateHandler withdrawSumHandler,
            AccountStateHandler calculationHandler, AccountStateHandler closeAccountHandler,
            AccountStateHandler openAccountHandler)
        {
            T newAccount = null;

            switch (accountType)
            {
                case AccountType.Ordinary:
                    newAccount = new DemandAccount (sum, 1) as T;
                    break;

                case AccountType.Deposit:
                    newAccount = new DepositAccount (sum, 40) as T;
                    break;
            }

            if (newAccount == null)
                throw new Exception ("Error creating account");
   
            accounts.Add (newAccount);

            newAccount.Added += addSumHandler;
            newAccount.Withdrawed += withdrawSumHandler;
            newAccount.Closed += closeAccountHandler;
            newAccount.Opened += openAccountHandler;
            newAccount.Calculated += calculationHandler;

            newAccount.Open ();
        }

        public void Put (decimal sum, int id)
        {
            T account = FindAccount (id);
            if (account == null)
                throw new Exception ("Account not found");
            account.Put (sum);
        }

        public void Withdraw (decimal sum, int id)
        {
            T account = FindAccount (id);
            if (account == null)
                throw new Exception ("Account not found");
            account.Withdraw (sum);
        }

        public void Close (int id)
        {
            T account = FindAccount (id, out int index);

            if (account == null)
                throw new Exception ("Account not found");

            account.Close ();
            accounts.RemoveAt (index);
        }

        public void CalculatePercentage ()
        {
            if (accounts == null)
                return; 

            foreach (Account account in accounts)
            {
                account.IncrementDays ();
                account.Calculate ();
            }
        }
      
        public T FindAccount (int id)
        {
            return accounts.Find (account => account.Id == id);
        }
       
        public T FindAccount (int id, out int index)
        {
            index = accounts.FindIndex (account => account.Id == id);
            return FindAccount (id);
        }

    }
  
    public enum AccountType
    {
        Ordinary,
        Deposit
    }
}
