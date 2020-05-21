using SheepFinance.Data;
using SheepFinance.Model;
using SheepFinance.ViewModel;
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

        public List<AccountItemViewModel> GetAccountList()
        {
            List<AccountItemViewModel> accountsVM = new List<AccountItemViewModel>();
            var accounts = database.GetAccounts().OrderByDescending(a => a.Amount).ToList();
            foreach (var item in accounts)
            {
                var account = new AccountItemViewModel(item);
                accountsVM.Add(account);
            }

            return accountsVM;
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
