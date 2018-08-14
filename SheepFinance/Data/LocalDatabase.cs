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
                CategoryGroups = new ObservableCollection<CategoryGroup>();
                Categories = new ObservableCollection<Category>();

                LoadAccounts();
                LoadDollares();
                LoadExpenses();
                LoadGoals();
                LoadIncomings();
                LoadTransfers();

                LoadCategories();
            }
            return instance;
        }

        #region FILES
        private static readonly string incommingsFile = "incomings.json";
        private static readonly string accountsFile = "accounts.json";
        private static readonly string expensesFile = "expenses.json";
        private static readonly string transfersFile = "transfers.json";
        private static readonly string dollaresFile = "dollares.json";
        private static readonly string goalsFile = "goals.json";
        private static readonly string categoryGroupsFile = "categoryGroups.json";

        private static readonly string categoriesFile = "categories.json";
        #endregion

        #region LISTS
        private static ObservableCollection<Incoming> Incomings { get; set; }
        private static ObservableCollection<Expense> Expenses { get; set; }
        private static ObservableCollection<Account> Accounts { get; set; }
        private static ObservableCollection<TransferCash> Transfers { get; set; }
        private static ObservableCollection<Dollar> Dollares { get; set; }
        private static ObservableCollection<Goal> Goals { get; set; }
        private static ObservableCollection<Category> Categories { get; set; }
        private static ObservableCollection<CategoryGroup> CategoryGroups { get; set; }
        #endregion
        
        #region DELETE
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
        #endregion

        #region ADD
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
        internal void AddExpense(double value, DateTime date, Account account, ItemCategory category)
        {
            var expense = new Expense(date, account, value, category);
            Expenses.Add(expense);
            expense.Move(value);
            SaveAccounts();
            SaveExpenses();
        }
        internal void AddIncoming(double value, DateTime date, Account account, ItemCategory category)
        {
            var incoming = new Incoming(date, account, value, category);
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
        #endregion

        #region UPDATE
        public void UpdateGoal()
        {
            SaveGoals();
        }
        #endregion

        #region LOAD
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
            else
            {
                Accounts.Add(new Account("Carteira", 0.00));
                var df = JsonConvert.SerializeObject(Accounts);

                StreamWriter sr = new StreamWriter(accountsFile);

                sr.Write(df);
                sr.Flush();
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
        private static void LoadCategories()
        {
            LoadCategoryGroups();

            FileInfo f = new FileInfo(categoriesFile);
            if (f.Exists)
            {
                StreamReader sr = new StreamReader(categoriesFile);

                var categoriesJson = sr.ReadToEnd();

                var CategoriesBD = JsonConvert.DeserializeObject<ObservableCollection<Category>>(categoriesJson);

                Categories = CategoriesBD;

                sr.Dispose();
                sr.Close();
            }
            else
            {
                var group = (from cg in CategoryGroups
                             where cg.Name.Equals("Renda")
                             select cg).FirstOrDefault();
                Categories.Add(new Category("Remuneração", group));
                Categories.Add(new Category("Bônus", group));
                Categories.Add(new Category("Rendimento", group));
                Categories.Add(new Category("Outras Rendas", group));
                Categories.Add(new Category("Empréstimo", group));

                group = (from cg in CategoryGroups
                         where cg.Name.Equals("Gastos Essenciais")
                         select cg).FirstOrDefault();
                Categories.Add(new Category("Moradia", group));
                Categories.Add(new Category("Contas Residenciais", group));
                Categories.Add(new Category("Saúde", group));
                Categories.Add(new Category("Educação", group));
                Categories.Add(new Category("Transporte", group));
                Categories.Add(new Category("Mercado", group));

                group = (from cg in CategoryGroups
                         where cg.Name.Equals("Estilo de Vida")
                         select cg).FirstOrDefault();
                Categories.Add(new Category("Empregados Domésticos", group));
                Categories.Add(new Category("TV/Internet/Telefone", group));
                Categories.Add(new Category("Taxas Bancárias", group));
                Categories.Add(new Category("Saques", group));
                Categories.Add(new Category("Bares/Restaurantes", group));
                Categories.Add(new Category("Lazer", group));
                Categories.Add(new Category("Compras", group));
                Categories.Add(new Category("Cuidados Pessoais", group));
                Categories.Add(new Category("Serviços", group));
                Categories.Add(new Category("Viagem", group));
                Categories.Add(new Category("Presentes/Doações", group));
                Categories.Add(new Category("Família/Filhos", group));
                Categories.Add(new Category("Despesas do Trabalho", group));
                Categories.Add(new Category("Outros Gastos", group));
                Categories.Add(new Category("Impostos", group));

                group = (from cg in CategoryGroups
                         where cg.Name.Equals("Empréstimos")
                         select cg).FirstOrDefault();
                Categories.Add(new Category("Juros de Cartão", group));
                Categories.Add(new Category("Crédito", group));
                Categories.Add(new Category("Cheque Especial", group));
                Categories.Add(new Category("Crédito Consignado", group));
                Categories.Add(new Category("Carnê", group));
                Categories.Add(new Category("Outros Empréstimos", group));
                Categories.Add(new Category("Juros", group));

                group = (from cg in CategoryGroups
                         where cg.Name.Equals("Não Classificado")
                         select cg).FirstOrDefault();
                Categories.Add(new Category("Categorizar", group));

                var df = JsonConvert.SerializeObject(Categories);

                StreamWriter sr = new StreamWriter(categoriesFile);

                sr.Write(df);
                sr.Flush();
                sr.Close();
            }
        }
        private static void LoadCategoryGroups()
        {
            FileInfo f = new FileInfo(categoryGroupsFile);
            if (f.Exists)
            {
                StreamReader sr = new StreamReader(categoryGroupsFile);

                var categoryGroupsJson = sr.ReadToEnd();

                var CategoryGroupsBD = JsonConvert.DeserializeObject<ObservableCollection<CategoryGroup>>(categoryGroupsJson);

                CategoryGroups = CategoryGroupsBD;

                sr.Dispose();
                sr.Close();
            }
            else
            {
                CategoryGroups.Add(new CategoryGroup("Renda"));
                CategoryGroups.Add(new CategoryGroup("Gastos Essenciais"));
                CategoryGroups.Add(new CategoryGroup("Estilo de Vida"));
                CategoryGroups.Add(new CategoryGroup("Empréstimos"));
                CategoryGroups.Add(new CategoryGroup("Não Classificado"));

                var df = JsonConvert.SerializeObject(CategoryGroups);

                StreamWriter sr = new StreamWriter(categoryGroupsFile);

                sr.Write(df);
                sr.Flush();
                sr.Close();
            }
        }
        #endregion

        #region SAVE
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
        #endregion

        #region GET
        internal ObservableCollection<Dollar> GetDollares() => Dollares;
        internal ObservableCollection<Expense> GetExpenses() => Expenses;
        internal ObservableCollection<Incoming> GetIncomings() => Incomings;
        internal ObservableCollection<Account> GetAccounts() => Accounts;
        internal ObservableCollection<TransferCash> GetTransfers() => Transfers;
        internal ObservableCollection<Goal> GetGoals() => Goals;
        internal ObservableCollection<Category> GetCategories() => Categories;
        #endregion
    }
}
