using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class DepositAccount: Account
    {
        public DepositAccount (decimal sum, int percentage) : base (sum, percentage)
        {
        }

        protected internal override void Open () => base.OnOpened (new AccountEventArgs ("\nA new deposit account has been opened! Id accounts: " + id, sum));

        public override void Put (decimal sum)
        {
            if (days >= 30)
            {
                base.Put (sum);
            }
            else
            {
                base.OnAdded (new AccountEventArgs ("It is possible to put on the account only after the 30-day period", 0));
            }
        }

        public override decimal Withdraw (decimal sum)
        {
            if (days >= 30)
            {
                return base.Withdraw (sum);
            }
            else
            {
                base.OnWithdrawed (new AccountEventArgs ("You can withdraw funds only after the 30-day period", 0));
            }

            return 0;
        }

        protected internal override void Calculate ()
        {
            if (days >= 30)
            {
                base.Calculate ();
            }
        }
    }
}
