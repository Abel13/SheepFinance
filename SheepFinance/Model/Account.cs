using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Model
{
    public class Account
    {
        public string Name { get; private set; }
        public double Amount { get; private set; }
        public bool Enabled { get; private set; }

        public Account(string name, double amount, bool enabled = true)
        {
            Name = name;
            Amount = amount;
            Enabled = enabled;
        }

        internal void Debit(double value)
        {
            Amount -= value;
        }

        internal void ChangeStatus()
        {
            Enabled = !Enabled;
        }

        internal void Credit(double value)
        {
            Amount += value;
        }
    }
}
