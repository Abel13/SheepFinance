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
    class ControlDashboard
    {
        LocalDatabase database;

        private ObservableCollection<Incoming> Incomings { get; set; }
        private ObservableCollection<Expense> Expenses { get; set; }
        private ObservableCollection<TransferCash> Transfers { get; set; }


        public ControlDashboard()
        {
            database = LocalDatabase.GetInstance();

            Incomings = database.GetIncomings();
            Expenses = database.GetExpenses();
            Transfers = database.GetTransfers();
        }

        public List<Account> GetAccountList()
        {
            return database.GetAccounts().OrderByDescending(a=>a.Amount).ToList();
        }

        public List<ItemChartMonthly> GetItensChartMonthly()
        {
            List<ItemChartMonthly> itemCharts = new List<ItemChartMonthly>();

            var groupedIncomings = (from i in Incomings
                                    group i by new { month = i.Date.Month, year = i.Date.Year } into d
                                    select new {
                                        dt = string.Format("{0}/{1}", d.Key.month, d.Key.year),
                                        count = d.Count()
                                   }).OrderByDescending(g => g.dt);

            var groupedExpenses = (from e in Expenses
                                   group e by new { month = e.Date.Month, year = e.Date.Year } into d
                                   select new
                                   {
                                       dt = string.Format("{0}/{1}", d.Key.month, d.Key.year),
                                       count = d.Count()
                                   }).OrderByDescending(g => g.dt);

            foreach (var item in groupedIncomings)
            {
                var ic = itemCharts.Where(i => i.Date.Equals(DateTime.Parse("01/" + item.dt))).FirstOrDefault();
                if (ic == null)
                {
                    ic = new ItemChartMonthly(item.dt);
                    itemCharts.Add(ic);
                }
                ic.SetIncoming(Incomings.Where(i=>i.Date.Month.Equals(ic.Date.Month) && i.Date.Year.Equals(ic.Date.Year)).Sum(i=>i.Value));
            }

            foreach (var item in groupedExpenses)
            {
                var ic = itemCharts.Where(i => i.Date.Equals(DateTime.Parse("01/" + item.dt))).FirstOrDefault();

                if(ic==null)
                {
                    ic = new ItemChartMonthly(item.dt);
                    itemCharts.Add(ic);
                }
                ic.SetExpense(Expenses.Where(e=>e.Date.Month.Equals(ic.Date.Month) && e.Date.Year.Equals(ic.Date.Year)).Sum(e=>e.Value));
            }

            return itemCharts.OrderByDescending(i=>i.Date).ToList();
        }

        public List<ItemChartCategory> GetItensChartCategories()
        {
            List<ItemChartCategory> itemCharts = new List<ItemChartCategory>();

            var groupedExpenses = (from e in Expenses
                                    group e by new { month = e.Date.Month, year = e.Date.Year } into d
                                    select new
                                    {
                                        dt = string.Format("{0}/{1}", d.Key.month, d.Key.year),
                                        count = d.Count()
                                    }).OrderByDescending(g => g.dt);
            
            foreach (var item in groupedExpenses)
            {
                var ic = itemCharts.Where(i => i.Date.Equals(DateTime.Parse("01/" + item.dt))).FirstOrDefault();
                if (ic == null)
                {
                    ic = new ItemChartCategory(item.dt);
                    itemCharts.Add(ic);
                }

                List<string> groups = (from g in Expenses
                                where g.Category != null && g.Date.Month.Equals(ic.Date.Month) && g.Date.Year.Equals(ic.Date.Year)
                                select g.Category.Group).Distinct().ToList();

                foreach (var group in groups)
                {
                    var value = Expenses.Where(i => i.Category != null && i.Date.Month.Equals(ic.Date.Month) && i.Date.Year.Equals(ic.Date.Year) && i.Category.Group.Equals(group)).Sum(g => g.Value);
                    ic.SetSeries(group, value);
                }
            }
            
            return itemCharts.OrderByDescending(i => i.Date).ToList();
        }

        public List<ItemDollar> GetDollares()
        {
            List<ItemDollar> items = new List<ItemDollar>();
            var days = database.GetDollares().OrderByDescending(d => d.DataHoraCotacao).Take(5).ToList();
            
            foreach (var item in days.OrderBy(d=>d.DataHoraCotacao).ToList())
            {
                items.Add(new ItemDollar(item.CotacaoCompra, item.DataHoraCotacao));
            }

            return items;
        }

        public List<ItemTimeLine> GetTransactionsTimeLine()
        {
            ObservableCollection<ItemTimeLine> itl = new ObservableCollection<ItemTimeLine>();

            foreach (var item in Incomings)
            {
                itl.Add(new ItemTimeLine("incoming", item.Description, item.Value.ToString("C"), item.Date));
            }

            foreach (var item in Expenses)
            {
                itl.Add(new ItemTimeLine("expense", item.Description, item.Value.ToString("C"), item.Date));
            }

            foreach (var item in Transfers)
            {
                itl.Add(new ItemTimeLine("transfer", item.Description, item.Value.ToString("C"), item.Date));
            }

            return itl.OrderByDescending(i=>i.Date).ToList();
        }
    }
}
