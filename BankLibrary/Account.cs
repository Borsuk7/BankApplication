using System;

namespace BankLibrary
{
    public abstract class Account: IAccount
    {
        //Event occurring during the withdrawal of money
        protected internal event AccountStateHandler Withdrawed;

        //Event occurring when adding to the account
        protected internal event AccountStateHandler Added;

        //Event occurring when opening an account
        protected internal event AccountStateHandler Opened;

        //Event occurring when closing an account
        protected internal event AccountStateHandler Closed;

        //Event arising from interest accrual
        protected internal event AccountStateHandler Calculated;

        protected int id;
        static int counter = 0;

        protected decimal sum; //Variable to store invoice amount
        protected int percentage; //Variable to store percentage

        protected int days = 0; //Time since opening an account

        public Account (decimal sum, int percentage)
        {
            this.sum = sum;
            this.percentage = percentage;
            id = ++counter;
        }

       
        public decimal CurrentSum
        {
            get { return sum; }
        }

        public int Percentage
        {
            get { return percentage; }
        }

        public int Id
        {
            get { return id; }
        }

        private void CallEvent (AccountEventArgs e, AccountStateHandler handler)
        {
            if (handler != null && e != null)
            {
                handler (this, e);
            }
        }

        // Calling individual events      
        protected virtual void OnOpened (AccountEventArgs e) => CallEvent (e, Opened);
        protected virtual void OnWithdrawed (AccountEventArgs e) => CallEvent (e, Withdrawed);
        protected virtual void OnAdded (AccountEventArgs e) => CallEvent (e, Added);
        protected virtual void OnClosed (AccountEventArgs e) => CallEvent (e, Closed);
        protected virtual void OnCalculated (AccountEventArgs e) => CallEvent (e, Calculated);

        public virtual void Put (decimal sum)
        {
            this.sum += sum;
            OnAdded (new AccountEventArgs ("On the bill arrived" + sum, sum));
        }

        public virtual decimal Withdraw (decimal sum)
        {
            decimal result = 0;
            if (sum <= this.sum)
            {
                this.sum -= sum;
                result = sum;
                OnWithdrawed (new AccountEventArgs ("Amount" + sum + "withdrawn" + id, sum));
            }
            else
            {
                OnWithdrawed (new AccountEventArgs ("Not enough money in the account " + id, 0));
            }
            return result;
        }

        //Account opening
        protected internal virtual void Open () => OnOpened (new AccountEventArgs ("Opened a new account! Id accounts: " + id, sum));

        //Account closing
        protected internal virtual void Close () => OnClosed (new AccountEventArgs ("Amount " + id + " closed. Total amount: " + CurrentSum, CurrentSum));

        protected internal void IncrementDays () => days++;

        //Interest accrual
        protected internal virtual void Calculate ()
        {
            decimal increment = sum * percentage / 100;
            sum += increment;

            string strincrement = string.Format ("{0:f2}", increment);


            OnCalculated (new AccountEventArgs ($"Interest accrued in the amount of: {strincrement}" , increment));
        }
    }
}
