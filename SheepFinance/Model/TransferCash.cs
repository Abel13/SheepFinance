using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Model
{
    public class TransferCash : Transaction
    {
        public Account AccountIn { get; private set; }
        public Account AccountOut { get; private set; }

        public TransferCash(DateTime date, Account accountOut, Account accountIn, double value)
        {
            Description = accountOut.Name + " para " + accountIn.Name;
            Date = date;
            AccountIn = accountIn;
            AccountOut = accountOut;
            Value = value;

        }

        public override void Move(double value)
        {
            AccountIn.Credit(value);
            AccountOut.Debit(value);
        }
    }
}
