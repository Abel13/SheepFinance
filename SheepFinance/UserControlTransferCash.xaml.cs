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
    /// Interação lógica para UserControlTransferCash.xam
    /// </summary>
    public partial class UserControlTransferCash : UserControl
    {
        ControlTransferCash control;
        public UserControlTransferCash()
        {
            InitializeComponent();
            control = new ControlTransferCash();
            ChangeMonth();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var accounts = control.GetAccountList();
            if (accounts.Count > 0)
            {
                ComboBoxOut.ItemsSource = accounts;
                ComboBoxIn.ItemsSource = accounts;
            }

            LoadTransfers();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var messageQueue = SnackbarThree.MessageQueue;
            if (TextBoxValue.Text == string.Empty)
            {
                TextBoxValue.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Informe o valor"));
                return;
            }

            if (DatePickerData.Text == string.Empty)
            {
                DatePickerData.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Informe a data"));
                return;
            }

            if (ComboBoxOut.Text == string.Empty)
            {
                ComboBoxOut.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Informe a conta de saída"));
                return;
            }

            if (ComboBoxIn.Text == string.Empty)
            {
                ComboBoxIn.Focus();
                Task.Factory.StartNew(() => messageQueue.Enqueue("Informe a conta de entrada"));
                return;
            }

            Double value = Double.Parse(TextBoxValue.Text, NumberStyles.Currency);
            DateTime date = DatePickerData.SelectedDate ?? DateTime.Now;
            var accountOut = ComboBoxOut.SelectedItem;
            var accountIn = ComboBoxIn.SelectedItem;

            control.SaveTransferCash(value, date, accountOut, accountIn);
            LoadTransfers();

            TextBoxValue.Text = string.Empty;
            ComboBoxOut.SelectedIndex = -1;
            ComboBoxIn.SelectedIndex = -1;
            DatePickerData.Text = string.Empty;
        }

        private void ButtonPreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            control.PreviousMonth();
            LoadTransfers();
            ChangeMonth();
        }

        private void ButtonNextMonth_Click(object sender, RoutedEventArgs e)
        {
            control.NextMonth();
            LoadTransfers();
            ChangeMonth();
        }

        private void TextBoxValue_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxValue.Text = string.Empty;
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

        void ChangeMonth()
        {
            TextBlockYear.Text = control.ActualDate.Year.ToString();
            TextBlockMonth.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(control.ActualDate.Month);
        }

        private void LoadTransfers()
        {
            var transactions = control.GetTransferList();
            if (transactions.Count > 0)
            {
                ListViewTransactions.ItemsSource = transactions;
            }
            else
            {
                ListViewTransactions.ItemsSource = null;
            }

            TextBlockTotal.Text = transactions.Sum(t => t.Value).ToString("c");

            TextBlockTransfersEmpty.Visibility = transactions.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            control.Delete(((Button)sender).DataContext);

            LoadTransfers();
        }
    }
}
