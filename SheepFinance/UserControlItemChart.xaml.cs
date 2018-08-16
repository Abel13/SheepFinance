﻿using SheepFinance.Model;
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
    /// Interação lógica para UserControlItemChart.xam
    /// </summary>
    public partial class UserControlItemChart : UserControl
    {
        public UserControlItemChart(ItemChart item, double maximum)
        {
            InitializeComponent();

            DataContext = item;

            GridIncoming.Height = (item.Incomings * 200) / maximum;
            GridExpense.Height = (item.Expenses * 200) / maximum;
        }
    }
}