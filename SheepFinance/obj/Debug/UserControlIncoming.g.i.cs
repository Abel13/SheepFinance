﻿#pragma checksum "..\..\UserControlIncoming.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3059F0FE9C88B447BB1389322DB6B72DA4446C0D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using SheepFinance;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SheepFinance {
    
    
    /// <summary>
    /// UserControlIncoming
    /// </summary>
    public partial class UserControlIncoming : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 15 "..\..\UserControlIncoming.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxValue;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\UserControlIncoming.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DatePickerData;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\UserControlIncoming.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxAccounts;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\UserControlIncoming.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonAdd;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\UserControlIncoming.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonPreviousMonth;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\UserControlIncoming.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockYear;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\UserControlIncoming.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockMonth;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\UserControlIncoming.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonNextMonth;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\UserControlIncoming.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewTransactions;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\UserControlIncoming.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockIncomingsEmpty;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\UserControlIncoming.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Snackbar SnackbarThree;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SheepFinance;component/usercontrolincoming.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UserControlIncoming.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\UserControlIncoming.xaml"
            ((SheepFinance.UserControlIncoming)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TextBoxValue = ((System.Windows.Controls.TextBox)(target));
            
            #line 15 "..\..\UserControlIncoming.xaml"
            this.TextBoxValue.LostFocus += new System.Windows.RoutedEventHandler(this.TextBoxValue_LostFocus);
            
            #line default
            #line hidden
            
            #line 15 "..\..\UserControlIncoming.xaml"
            this.TextBoxValue.GotFocus += new System.Windows.RoutedEventHandler(this.TextBoxValue_GotFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.DatePickerData = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 4:
            this.ComboBoxAccounts = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.ButtonAdd = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\UserControlIncoming.xaml"
            this.ButtonAdd.Click += new System.Windows.RoutedEventHandler(this.ButtonAdd_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ButtonPreviousMonth = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\UserControlIncoming.xaml"
            this.ButtonPreviousMonth.Click += new System.Windows.RoutedEventHandler(this.ButtonPreviousMonth_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TextBlockYear = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.TextBlockMonth = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.ButtonNextMonth = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\UserControlIncoming.xaml"
            this.ButtonNextMonth.Click += new System.Windows.RoutedEventHandler(this.ButtonNextMonth_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ListViewTransactions = ((System.Windows.Controls.ListView)(target));
            return;
            case 12:
            this.TextBlockIncomingsEmpty = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.SnackbarThree = ((MaterialDesignThemes.Wpf.Snackbar)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 11:
            
            #line 55 "..\..\UserControlIncoming.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonDelete_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
