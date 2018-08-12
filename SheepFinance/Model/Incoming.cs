using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Model
{
    public class Incoming : Transaction
    {
        public Account Account { get; private set; }
        public Incoming(DateTime date, Account account, double value)
        {
            Description = account.Name;
            Date = date;
            Account = account;
            Value = value;
        }

        public override void Move(double value)
        {
            Account.Credit(value);
        }
    }
}
