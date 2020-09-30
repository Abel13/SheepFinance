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
    /// Interação lógica para UserControlIncoming.xam
    /// </summary>
    public partial class UserControlIncoming : UserControl
    {
        ControlIncoming control;

        public UserControlIncoming()
        {
            InitializeComponent();
            control = new ControlIncoming();
            ChangeMonth();
        }

        private void TextBoxValue_LostFocus(object sender, RoutedEventArgs e)
        {
            var messageQueue = SnackbarThree.MessageQueue;
            Double.TryParse(TextBoxValue.Text, out double result);

            if (!(new Regex(@"\d+(,\d{1,2})?")).Match(result.ToString()).Success || result <= 0)
            {
                TextBoxValue.Text = string.Empty;
                Task.Factory.StartNew(() => messageQueue.Enqueue("Informe um valor valido!"));
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

        private void ButtonAddIncoming_Click(object sender, RoutedEventArgs e)
        {
            var messageQueue = SnackbarThree.MessageQueue;
            if (TextBoxValue.Text == string.Empty)
            {
                TextBoxValue.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Informe o valor da entrada"));
                return;
            }

            if (DatePickerData.Text == string.Empty)
            {
                DatePickerData.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Informe a data da entrada"));
                return;
            }

            if (ComboBoxCategory.Text == string.Empty)
            {
                ComboBoxCategory.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Informe a categoria da entrada"));
                return;
            }

            if (ComboBoxAccounts.Text == string.Empty)
            {
                ComboBoxAccounts.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Informe a conta da entrada"));
                return;
            }

            Double value = Double.Parse(TextBoxValue.Text, NumberStyles.Currency);
            DateTime date = DatePickerData.SelectedDate ?? DateTime.Now;
            var account = ComboBoxAccounts.SelectedItem;
            var category = ComboBoxCategory.SelectedItem;

            control.SaveIncoming(value, date, account, category);
            LoadIncomings();

            TextBoxValue.Text = string.Empty;
            ComboBoxAccounts.SelectedIndex = -1;
            DatePickerData.Text = string.Empty;
            ComboBoxCategory.SelectedIndex = -1;
        }

        private void LoadIncomings()
        {
            var transactions = control.GetIncomingList();
            if (transactions.Count > 0)
            {
                ListViewTransactions.ItemsSource = transactions;
            }
            else
            {
                ListViewTransactions.ItemsSource = null;
            }

            TextBlockTotal.Text = transactions.Sum(t => t.Value).ToString("c");

            TextBlockIncomingsEmpty.Visibility = transactions.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var accounts = control.GetAccountList();
            if (accounts.Count > 0)
                ComboBoxAccounts.ItemsSource = accounts;

            ComboBoxCategory.ItemsSource = control.GetCategoryList();
            ComboBoxCategory.SelectedIndex = -1;

            LoadIncomings();
        }

        private void ButtonPreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            control.PreviousMonth();
            LoadIncomings();
            ChangeMonth();
        }

        private void ButtonNextMonth_Click(object sender, RoutedEventArgs e)
        {
            control.NextMonth();
            LoadIncomings();
            ChangeMonth();
        }

        void ChangeMonth()
        {
            TextBlockYear.Text = control.ActualDate.Year.ToString();
            TextBlockMonth.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(control.ActualDate.Month);
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            control.Delete(((Button)sender).DataContext);

            LoadIncomings();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            WindowEditTransaction transaction = new WindowEditTransaction(((Button)sender).DataContext);
            transaction.ShowDialog();

            LoadIncomings();
        }
    }
}