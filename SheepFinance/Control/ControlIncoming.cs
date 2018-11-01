using SheepFinance.Data;
using SheepFinance.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SheepFinance.Control
{
    class ControlIncoming
    {
        LocalDatabase database;
        
        public DateTime ActualDate { get; private set; }

        public ControlIncoming()
        {
            ActualDate = DateTime.Now;

            database = LocalDatabase.GetInstance();
        }
        
        public List<Incoming> GetIncomingList()
        {
            var incomings = database.GetIncomings();
            return (from i in incomings
                   where i.Date.Year.Equals(ActualDate.Year) && i.Date.Month.Equals(ActualDate.Month)
                   select i).OrderByDescending(i=>i.Date).ToList();
        }

        public ObservableCollection<Account> GetAccountList()
        {
            return database.GetAccounts();
        }

        internal void SaveIncoming(double value, DateTime date, object account, object category)
        {
            var acc = (from a in GetAccountList()
                      where a.Name.Equals(((Account)account).Name)
                      select a).FirstOrDefault();

            database.AddIncoming(value, date, acc, (ItemCategory)category);
        }

        internal void NextMonth()
        {
            ActualDate = ActualDate.AddMonths(1);
        }

        internal void PreviousMonth()
        {
            ActualDate = ActualDate.AddMonths(-1);
        }

        internal void Delete(object incoming)
        {
            var i = (Incoming)incoming;
            var account = (from a in database.GetAccounts()
                          where a.Name.Equals(i.Account.Name)
                          select a).FirstOrDefault();
            account.Debit(i.Value);
            database.DeleteIncoming(i);
        }

        public ListCollectionView GetCategoryList()
        {
            var categories = database.GetCategories().ToList();
            List<ItemCategory> items = new List<ItemCategory>();

            foreach (var item in categories)
            {
                items.Add(new ItemCategory{ Group = item.Group.Name, Name = item.Name });
            }

            ListCollectionView lcv = new ListCollectionView(items);
            lcv.GroupDescriptions.Add(new PropertyGroupDescription("Group"));

            return lcv;
        }
    }
}
