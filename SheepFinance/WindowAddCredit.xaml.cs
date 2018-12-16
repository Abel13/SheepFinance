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
using System.Windows.Shapes;

namespace SheepFinance
{
    /// <summary>
    /// Interaction logic for WindowAddCredit.xaml
    /// </summary>
    public partial class WindowAddCredit : Window
    {
        ControlAddCredit control;
        public WindowAddCredit(object goal)
        {
            InitializeComponent();
            control = new ControlAddCredit(goal);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlockGoal.DataContext = control.GetGoal();
            ComboBoxGoals.ItemsSource = control.GetGoalCategories();
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

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Double value = Double.Parse(TextBoxValue.Text, NumberStyles.Currency);
            var goal = ComboBoxGoals.SelectedItem;

            if (goal != null)
                control.DebitGoal(goal, value);
            control.AddCredit(value);
            this.Close();
        }
    }
}
