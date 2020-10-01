using SheepFinance.Control;
using SheepFinance.Model;
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
            ComboBoxCategory.ItemsSource = control.GetCategoryList();
            this.DataContext = transaction;
            //ComboBoxCategory.SelectedItem = ((Expense)transaction).Category;
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

        private void ComboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null)
            {

                var cat = (ItemCategory)((ComboBox)sender).SelectedItem;
                var data = (Expense)DataContext;
                data.UpdateCategory(cat);
            }
        }
    }
}
