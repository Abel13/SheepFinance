using MaterialDesignThemes.Wpf;
using SheepFinance.Data;
using SheepFinance.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SheepFinance.ViewModel
{
    public class AccountItemViewModel : BaseViewModel, INotifyPropertyChanged
    {
        LocalDatabase database;

        public ICommand MyCommand
        {
            get;
            set;
        }

        private bool CanExecuteMyMethod(object parameter)
        {
            return true;
        }

        private void ExecuteMyMethod(object parameter)
        {
            Account.ChangeStatus();
            DisableIcon = Account.Enabled ? PackIconKind.Eye : PackIconKind.EyeOff;
            NotifyPropertyChanged("Enabled");

            database.UpdateAccount();
        }

        private PackIconKind disableIcon;
        public PackIconKind DisableIcon
        {
            get { return disableIcon; }
            set { disableIcon = value; NotifyPropertyChanged("DisableIcon"); }
        }

        private Account account;
        public Account Account
        {
            get { return account; }
            set { account = value; DisableIcon = value.Enabled ? PackIconKind.Eye : PackIconKind.EyeOff; NotifyPropertyChanged("Account"); }
        }

        public AccountItemViewModel(Account account)
        {
            database = LocalDatabase.GetInstance();
            MyCommand = new RelayCommand(ExecuteMyMethod, CanExecuteMyMethod);

            Account = account;
        }
    }
}
