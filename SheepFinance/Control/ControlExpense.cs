using SheepFinance.Data;
using SheepFinance.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SheepFinance.Control
{
    class ControlExpense
    {
        LocalDatabase database;

        public DateTime ActualDate { get; internal set; }

        public ControlExpense()
        {
            ActualDate = DateTime.Now;

            database = LocalDatabase.GetInstance();
        }

        public List<Expense> GetExpenseList()
        {
            var expenses = database.GetExpenses();
            return (from e in expenses
                    where e.Date.Year.Equals(ActualDate.Year) && e.Date.Month.Equals(ActualDate.Month)
                    select e).ToList();
        }

        public ObservableCollection<Account> GetAccountList() => database.GetAccounts();

        internal void SaveExpense(double value, DateTime date, object account, object category)
        {
            var acc = (from a in GetAccountList()
                       where a.Name.Equals(((Account)account).Name)
                       select a).FirstOrDefault();

            database.AddExpense(value, date, acc, (ItemCategory)category);
        }

        internal List<Goal> GetGoalList() => database.GetGoals().Where(g => !g.Done).ToList();

        internal void NextMonth() => ActualDate = ActualDate.AddMonths(1);

        public ListCollectionView GetCategoryList()
        {
            var categories = database.GetCategories().ToList();
            List<ItemCategory> items = new List<ItemCategory>();

            foreach (var item in categories)
            {
                items.Add(new ItemCategory { Group = item.Group.Name, Name = item.Name });
            }

            ListCollectionView lcv = new ListCollectionView(items);
            lcv.GroupDescriptions.Add(new PropertyGroupDescription("Group"));

            return lcv;
        }

        internal void PreviousMonth() => ActualDate = ActualDate.AddMonths(-1);

        internal void DebitGoal(object goal, double value)
        {
            var Goal = (Goal)goal;
            Goal.Debit(value);
            database.UpdateGoal();
        }

        internal void Delete(object expense)
        {
            var e = (Expense)expense;
            var account = (from a in database.GetAccounts()
                           where a.Name.Equals(e.Account.Name)
                           select a).FirstOrDefault();
            account.Credit(e.Value);
            database.DeleteExpense(e);
        }
    }
}
