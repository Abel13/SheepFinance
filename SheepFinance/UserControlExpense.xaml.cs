using SheepFinance.Control;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interação lógica para UserControlExpense.xam
    /// </summary>
    public partial class UserControlExpense : UserControl
    {
        ControlExpense control;

        public UserControlExpense()
        {
            InitializeComponent();
            control = new ControlExpense();
            ChangeMonth();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var accounts = control.GetAccountList();
            if (accounts.Count > 0)
                ComboBoxAccounts.ItemsSource = accounts;

            var goals = control.GetGoalList();
            ComboBoxGoals.Visibility = goals.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            if (goals.Count > 0)
                ComboBoxGoals.ItemsSource = goals;

            ComboBoxCategory.ItemsSource = control.GetCategoryList();
            ComboBoxCategory.SelectedIndex = -1;

            LoadExpenses();
        }

        private void LoadExpenses()
        {
            var transactions = control.GetExpenseList();
            if (transactions.Count > 0)
            {
                ListViewTransactions.ItemsSource = transactions;
            }
            else
            {
                ListViewTransactions.ItemsSource = null;
            }

            TextBlockTotal.Text = transactions.Sum(t => t.Value).ToString("c");

            TextBlockExpensesEmpty.Visibility = transactions.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var messageQueue = SnackbarThree.MessageQueue;
            if (TextBoxValue.Text == string.Empty)
            {
                TextBoxValue.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Informe o valor da saída"));
                return;
            }

            if (DatePickerData.Text == string.Empty)
            {
                DatePickerData.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Informe a data saída"));
                return;
            }

            if (ComboBoxCategory.Text == string.Empty)
            {
                ComboBoxCategory.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Informe a categoria da saída"));
                return;
            }

            if (ComboBoxAccounts.Text == string.Empty)
            {
                ComboBoxAccounts.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Informe a conta da saída"));
                return;
            }

            Double value = Double.Parse(TextBoxValue.Text, NumberStyles.Currency);
            DateTime date = DatePickerData.SelectedDate ?? DateTime.Now;
            var account = ComboBoxAccounts.SelectedItem;
            var goal = ComboBoxGoals.SelectedItem;
            var category = ComboBoxCategory.SelectedItem;
            
            if(goal != null)
                control.DebitGoal(goal, value);

            control.SaveExpense(value, date, account, category);
            LoadExpenses();

            TextBoxValue.Text = string.Empty;
            ComboBoxAccounts.SelectedIndex = -1;
            DatePickerData.Text = string.Empty;
            ComboBoxGoals.SelectedIndex = -1;
            ComboBoxCategory.SelectedIndex = -1;
        }

        private void TextBoxValue_LostFocus(object sender, RoutedEventArgs e)
        {
            Double.TryParse(TextBoxValue.Text, out double result);

            if (!(new Regex(@"\d+(,\d{1,2})?")).Match(result.ToString()).Success)
            {
                TextBoxValue.Text = string.Empty;
            }
            else
            {
                TextBoxValue.Text = result.ToString("C");
            }
        }

        private void TextBoxValue_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxValue.Text = string.Empty;
        }

        private void ButtonPreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            control.PreviousMonth();
            LoadExpenses();
            ChangeMonth();
        }

        private void ChangeMonth()
        {
            TextBlockYear.Text = control.ActualDate.Year.ToString();
            TextBlockMonth.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(control.ActualDate.Month);
        }

        private void ButtonNextMonth_Click(object sender, RoutedEventArgs e)
        {
            control.NextMonth();
            LoadExpenses();
            ChangeMonth();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            control.Delete(((Button)sender).DataContext);
            LoadExpenses();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            WindowEditTransaction transaction = new WindowEditTransaction(((Button)sender).DataContext);
            transaction.ShowDialog();

            LoadExpenses();
        }
    }
}
