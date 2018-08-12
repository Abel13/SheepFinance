using SheepFinance.Control;
using SheepFinance.Exceptions;
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
    /// Interação lógica para UserControlAccount.xam
    /// </summary>
    public partial class UserControlAccount : UserControl
    {
        ControlAccount control;

        public UserControlAccount()
        {
            InitializeComponent();
            control = new ControlAccount();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            var accounts = control.GetAccountList();
            if (accounts.Count > 0)
            {
                ListViewAccounts.ItemsSource = accounts;
                TextBlockAccountsEmpty.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextBlockAccountsEmpty.Visibility = Visibility.Visible;
            }

            TextBlockTotal.Text = accounts.Sum(a => a.Amount).ToString("C");
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var messageQueue = SnackbarThree.MessageQueue;
            if (TextBoxName.Text == string.Empty)
            {
                TextBoxName.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Informe o nome"));
                return;
            }

            if (TextBoxInitialAmount.Text == string.Empty)
            {
                TextBoxInitialAmount.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Informe o valor inicial"));
                return;
            }

            string name = TextBoxName.Text;
            Double initialValue = Double.Parse(TextBoxInitialAmount.Text, NumberStyles.Currency);

            control.SaveAccount(name, initialValue);
            LoadAccounts();

            TextBoxName.Text = string.Empty;
            TextBoxInitialAmount.Text = string.Empty;
        }

        private void TextBoxInitialValue_LostFocus(object sender, RoutedEventArgs e)
        {
            Double.TryParse(TextBoxInitialAmount.Text, out double result);

            if (!(new Regex(@"\d+(,\d{1,2})?")).Match(result.ToString()).Success)
            {
                TextBoxInitialAmount.Text = string.Empty;
            }
            else
            {
                TextBoxInitialAmount.Text = result.ToString("C");
            }
        }

        private void TextBoxInitialValue_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxInitialAmount.Text = string.Empty;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                control.Delete(((Button)sender).DataContext);
                LoadAccounts();
            }
            catch (DataHasChildrenException)
            {
                var messageQueue = SnackbarThree.MessageQueue;
                Task.Factory.StartNew(() => messageQueue.Enqueue("Existem entradas, saídas ou transferências vinculadas a esta conta!!"));
            }
        }
    }
}
