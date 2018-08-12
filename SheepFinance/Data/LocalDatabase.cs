using Newtonsoft.Json;
using SheepFinance.Exceptions;
using SheepFinance.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Data
{
    /// <summary>
    /// Singletom LocalDatabase
    /// </summary>
    public class LocalDatabase
    {
        private static LocalDatabase instance;

        private LocalDatabase()
        { }

        private static readonly string incommingsFile = "incomings.json";
        private static readonly string accountsFile = "accounts.json";
        private static readonly string expensesFile = "expenses.json";
        private static readonly string transfersFile = "transfers.json";
        private static readonly string dollaresFile = "dollares.json";
        private static readonly string goalsFile = "goals.json";

        private static ObservableCollection<Incoming> Incomings { get; set; }
        private static ObservableCollection<Expense> Expenses { get; set; }
        private static ObservableCollection<Account> Accounts { get; set; }
        private static ObservableCollection<TransferCash> Transfers { get; set; }
        private static ObservableCollection<Dollar> Dollares { get; set; }
        private static ObservableCollection<Goal> Goals { get; set; }

        public static LocalDatabase GetInstance()
        {
            if (instance == null)
            {
                instance = new LocalDatabase();
                Incomings = new ObservableCollection<Incoming>();
                Accounts = new ObservableCollection<Account>();
                Expenses = new ObservableCollection<Expense>();
                Transfers = new ObservableCollection<TransferCash>();
                Dollares = new ObservableCollection<Dollar>();
                Goals = new ObservableCollection<Goal>();

                LoadAccounts();
                LoadDollares();
                LoadExpenses();
                LoadGoals();
                LoadIncomings();
                LoadTransfers();
            }
            return instance;
        }

        internal void DeleteExpense(Expense expense)
        {
            Expenses.Remove(expense);
            SaveExpenses();
            SaveAccounts();
        }
        internal void DeleteTransfer(TransferCash transfer)
        {
            Transfers.Remove(transfer);
            SaveTransfers();
            SaveAccounts();
        }
        internal void DeleteIncoming(Incoming incoming)
        {
            Incomings.Remove(incoming);
            SaveIncomings();
            SaveAccounts();
        }
        internal void DeleteAccount(Account account)
        {
            var totalt = (from i in Transfers
                          where i.AccountIn.Name.Equals(account.Name) || i.AccountOut.Name.Equals(account.Name)
                          select i).Count();
            var totali = (from i in Incomings
                             where i.Account.Name.Equals(account.Name)
                             select i).Count();
            var totale = (from e in Expenses
                            where e.Account.Name.Equals(account.Name)
                            select e).Count();

            if (totale == 0 && totali == 0)
            {
                Accounts.Remove(account);
                SaveAccounts();
            }
            else
                throw new DataHasChildrenException();
        }

        /// <summary>
        /// Add an accounto to Accounts List
        /// </summary>
        /// <param name="name">Account Name</param>
        /// <param name="initialAmount">Value to begin this account</param>
        internal void AddAccount(string name, double initialAmount)
        {
            Accounts.Add(new Account(name, initialAmount));
            SaveAccounts();
        }
        /// <summary>
        /// Add an Expense to Expenses List
        /// </summary>
        /// <param name="value"></param>
        /// <param name="date"></param>
        /// <param name="account"></param>
        internal void AddExpense(double value, DateTime date, Account account)
        {
            var expense = new Expense(date, account, value);
            Expenses.Add(expense);
            expense.Move(value);
            SaveAccounts();
            SaveExpenses();
        }
        internal void AddIncoming(double value, DateTime date, Account account)
        {
            var incoming = new Incoming(date, account, value);
            Incomings.Add(incoming);
            incoming.Move(value);
            SaveAccounts();
            SaveIncomings();
        }
        internal void AddTransferCash(double value, DateTime date, Account accOut, Account accIn)
        {
            var transfer = new TransferCash(date, accOut, accIn, value);
            Transfers.Add(transfer);
            transfer.Move(value);
            SaveAccounts();
            SaveTransfers();
        }
        internal void AddDollar(double cotacaoCompra, double cotacaoVenda, DateTime date)
        {
            var dollar = new Dollar(date, cotacaoCompra, cotacaoVenda);
            Dollares.Add(dollar);
            SaveDollares();
        }
        internal void AddGoal(string name, double goalValue, DateTime deadline)
        {
            var goal = new Goal(name, goalValue, deadline);
            Goals.Add(goal);
            SaveGoals();
        }

        public void UpdateGoal()
        {
            SaveGoals();
        }

        private static void LoadIncomings()
        {
            FileInfo f = new FileInfo(incommingsFile);
            if (f.Exists)
            {
                StreamReader sr = new StreamReader(incommingsFile);

                var incomingsJson = sr.ReadToEnd();

                var IncomingsBD = JsonConvert.DeserializeObject<ObservableCollection<Incoming>>(incomingsJson);

                Incomings = IncomingsBD;

                sr.Dispose();
                sr.Close();
            }
        }
        private static void LoadAccounts()
        {
            FileInfo f = new FileInfo(accountsFile);
            if (f.Exists)
            {
                StreamReader sr = new StreamReader(accountsFile);

                var accountsJson = sr.ReadToEnd();

                var AccountsBD = JsonConvert.DeserializeObject<ObservableCollection<Account>>(accountsJson);

                Accounts = AccountsBD;

                sr.Dispose();
                sr.Close();
            }
        }
        private static void LoadGoals()
        {
            FileInfo f = new FileInfo(goalsFile);
            if (f.Exists)
            {
                StreamReader sr = new StreamReader(goalsFile);

                var goalsJson = sr.ReadToEnd();

                var GoalsBD = JsonConvert.DeserializeObject<ObservableCollection<Goal>>(goalsJson);

                Goals = GoalsBD;

                sr.Dispose();
                sr.Close();
            }
        }
        private static void LoadExpenses()
        {
            FileInfo f = new FileInfo(expensesFile);
            if (f.Exists)
            {
                StreamReader sr = new StreamReader(expensesFile);

                var expensesJson = sr.ReadToEnd();

                var ExpensesBD = JsonConvert.DeserializeObject<ObservableCollection<Expense>>(expensesJson);

                Expenses = ExpensesBD;

                sr.Dispose();
                sr.Close();
            }
        }
        private static void LoadTransfers()
        {
            FileInfo f = new FileInfo(transfersFile);
            if (f.Exists)
            {
                StreamReader sr = new StreamReader(transfersFile);

                var transfersJson = sr.ReadToEnd();

                var TransfersBD = JsonConvert.DeserializeObject<ObservableCollection<TransferCash>>(transfersJson);

                Transfers = TransfersBD;

                sr.Dispose();
                sr.Close();
            }
        }
        private static void LoadDollares()
        {
            FileInfo f = new FileInfo(dollaresFile);
            if (f.Exists)
            {
                StreamReader sr = new StreamReader(dollaresFile);

                var dollaresJson = sr.ReadToEnd();

                var DollaresBD = JsonConvert.DeserializeObject<ObservableCollection<Dollar>>(dollaresJson);

                Dollares = DollaresBD;

                sr.Dispose();
                sr.Close();
            }
        }

        private void SaveIncomings()
        {
            var df = JsonConvert.SerializeObject(Incomings);

            StreamWriter sr = new StreamWriter(incommingsFile);

            sr.Write(df);
            sr.Flush();
            sr.Close();
        }
        private void SaveAccounts()
        {
            var df = JsonConvert.SerializeObject(Accounts);

            StreamWriter sr = new StreamWriter(accountsFile);

            sr.Write(df);
            sr.Flush();
            sr.Close();
        }
        private void SaveExpenses()
        {
            var df = JsonConvert.SerializeObject(Expenses);

            StreamWriter sr = new StreamWriter(expensesFile);

            sr.Write(df);
            sr.Flush();
            sr.Close();
        }
        private void SaveTransfers()
        {
            var df = JsonConvert.SerializeObject(Transfers);

            StreamWriter sr = new StreamWriter(transfersFile);

            sr.Write(df);
            sr.Flush();
            sr.Close();
        }
        private void SaveDollares()
        {
            var df = JsonConvert.SerializeObject(Dollares);

            StreamWriter sr = new StreamWriter(dollaresFile);

            sr.Write(df);
            sr.Flush();
            sr.Close();
        }
        private void SaveGoals()
        {
            var df = JsonConvert.SerializeObject(Goals);

            StreamWriter sr = new StreamWriter(goalsFile);

            sr.Write(df);
            sr.Flush();
            sr.Close();
        }

        internal ObservableCollection<Dollar> GetDollares() => Dollares;
        internal ObservableCollection<Expense> GetExpenses() => Expenses;
        internal ObservableCollection<Incoming> GetIncomings() => Incomings;
        internal ObservableCollection<Account> GetAccounts() => Accounts;
        internal ObservableCollection<TransferCash> GetTransfers() => Transfers;
        internal ObservableCollection<Goal> GetGoals() => Goals;
    }
}
