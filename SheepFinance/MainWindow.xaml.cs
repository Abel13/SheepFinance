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
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        ControlMain control;
        public MainWindow()
        {
            InitializeComponent();
            control = new ControlMain();
            ListViewMenu.ItemsSource = control.GetMenuList();
            control.GetDollaresAPI();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = ListViewMenu.SelectedIndex;

            switch (index)
            {
                case 0:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlDashboard());
                    break;
                case 1:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlAccount());
                    break;
                case 2:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlIncoming());
                    break;
                case 3:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlExpense());
                    break;
                case 4:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlTransferCash());
                    break;
                case 5:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlBoxes());
                    break;
                default:
                    break;
            }

            ListViewMenu.SelectedIndex = -1;
            StackMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            StackMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            StackMenu.Visibility = Visibility.Visible;
        }

        private void ButtonShutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
