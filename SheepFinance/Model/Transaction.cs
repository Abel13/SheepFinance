using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Model
{
    public abstract class Transaction
    {
        public string Description { get; protected set; }
        public double Value { get; protected set; }
        public DateTime Date { get; protected set; }
        public ItemCategory Category { get; protected set; }

        public void UpdateDate(DateTime date)
        {
            Date = date;
        }
        public abstract void Move(Double value);
    }
}
