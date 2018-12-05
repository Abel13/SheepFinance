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
using System.Windows.Shapes;

namespace SheepFinance
{
    /// <summary>
    /// Interaction logic for WindowEditTransaction.xaml
    /// </summary>
    public partial class WindowEditTransaction : Window
    {
        ControlEditTransaction control;
        public WindowEditTransaction(object transaction)
        {
            InitializeComponent();
            control = new ControlEditTransaction();
            this.DataContext = transaction;
            ComboBoxCategory.ItemsSource = control.GetCategoryList();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerData.Text == string.Empty)
            {
                DatePickerData.Focus();
                return;
            }
            DateTime date = DatePickerData.SelectedDate ?? DateTime.Now;

            control.Salvar(date, DataContext);
            this.Close();
        }
    }
}
