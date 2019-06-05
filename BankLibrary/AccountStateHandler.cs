using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public delegate void AccountStateHandler (object sender, AccountEventArgs e);

    public class AccountEventArgs
    {
       
        public string Message { get; }

        //The amount by which the account has changed
        public decimal Sum { get; }

        public AccountEventArgs (string mes, decimal sum)
        {
            Message = mes;
            Sum = sum;
        }
    }
}