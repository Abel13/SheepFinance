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
            LoadDollar();
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
            var monthlyStatistics = control.GetItemCharts();
            if (monthlyStatistics.Count > 0)
            {
                var mI = monthlyStatistics.Max(i => i.Incomings);
                var mE = monthlyStatistics.Max(e => e.Expenses);
                var max = mI > mE ? mI : mE;
                foreach (var item in monthlyStatistics)
                {
                    StackMain.Children.Add(new UserControlItemChart(item, max));
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
                TextBlockTotal.Text = accounts.Sum(a => a.Amount).ToString("C");
            }
        }
    }
}
