using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public interface IAccount
    {
        // Put money
        void Put (decimal sum);

        // Withdraw money
        decimal Withdraw (decimal sum);
    }
}
