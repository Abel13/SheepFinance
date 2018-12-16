using SheepFinance.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SheepFinance
{
    /// <summary>
    /// Interação lógica para UserControlDashboard.xam
    /// </summary>
    public partial class UserControlDashboard : UserControl
    {
        ControlDashboard control;

        public UserControlDashboard()
        {
            InitializeComponent();
            control = new ControlDashboard();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAccounts();
            LoadTransactions();
            LoadMonthlyStatistics();
            LoadMonthlyIncomes();
            LoadCategoryCosts();
            LoadDollar();
        }

        private void LoadMonthlyIncomes()
        {
            var monthlyIncomes = control.GetMonthlyIncomes();
            if (monthlyIncomes.Count > 0)
            {
                var max = monthlyIncomes.Max(i => i.Incomings);
                foreach (var item in monthlyIncomes)
                {
                    StackMonthlyIncome.Children.Add(new UserControlItemMonthlyIncome(item, max));
                }
            }

            TextBlockMonthlyIncomeEmpty.Visibility = monthlyIncomes.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        private void LoadCategoryCosts()
        {
            var categories = control.GetItensChartCategories();
            if (categories.Count > 0)
            {
                foreach (var item in categories)
                {
                    StackCategoryCost.Children.Add(new UserControlItemCostByCategory(item));
                }
            }

            TextBlockCategoryCostEmpty.Visibility = categories.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        private void LoadTransactions()
        {
            var transactions = control.GetTransactionsTimeLine();
            if (transactions.Count > 0)
                ListViewTimeLine.ItemsSource = transactions;
        }

        private void LoadDollar()
        {
            var dollares = control.GetDollares();
            if (dollares.Count > 0)
                ListViewDollares.ItemsSource = dollares;
        }

        private void LoadMonthlyStatistics()
        {
            var monthlyStatistics = control.GetItensChartMonthly();
            if (monthlyStatistics.Count > 0)
            {
                var mI = monthlyStatistics.Max(i => i.Incomings);
                var mE = monthlyStatistics.Max(e => e.Expenses);
                var max = mI > mE ? mI : mE;
                foreach (var item in monthlyStatistics)
                {
                    StackMonthlyChart.Children.Add(new UserControlItemChart(item, max));
                }   
            }

            TextBlockMonthlyChartsEmpty.Visibility = monthlyStatistics.Count>0 ? Visibility.Collapsed : Visibility.Visible;
        }

        private void LoadAccounts()
        {
            var accounts = control.GetAccountList();
            if (accounts.Count > 0)
            {
                ListViewAccounts.ItemsSource = accounts;
                TextBlockTotal.Content = accounts.Sum(a => a.Amount).ToString("C");
            }
        }
    }
}
