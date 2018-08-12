using SheepFinance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Data
{
    public class ORM
    {
        private static ORM uniqueInstance;
        private ORM()
        {
            Transactions = new List<Transaction>();
        }

        public static ORM getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new ORM();

            return uniqueInstance;
        }

        public List<Transaction> Transactions { get; private set; }
        public void NewTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }
    }
}
