using SheepFinance.Data;
using SheepFinance.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Control
{
    public class ControlAccount
    {
        LocalDatabase database;

        public ControlAccount()
        {
            database = LocalDatabase.GetInstance();
        }

        public ObservableCollection<Account> GetAccountList()
        {
            return database.GetAccounts();
        }

        internal void SaveAccount(string name, double initialAmount)
        {
            database.AddAccount(name, initialAmount);
        }

        internal void Delete(object account)
        {
            database.DeleteAccount((Account)account);

        }
    }
}
