using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;



namespace Stock_Market_App
{
    #region DataClasses
    public class mainDB
    {
        public static List<string> get(string query)
        {
            IDbConnection cnn = new SQLiteConnection("Data Source=mainDB.db;Version=3;");
            return cnn.Query<string>(query).ToList();
        }
        public static List<user> getUsers()
        {
            IDbConnection cnn = new SQLiteConnection("Data Source=mainDB.db;Version=3;");
            return cnn.Query<user>("select * from Users").ToList();
        }
        public static user getUserData(string query)
        {
            IDbConnection cnn = new SQLiteConnection("Data Source=mainDB.db;Version=3;");
            return cnn.Query<user>($"select * from Users where Name like '%{query}%' or UserID like '%{query}%'").ToList()[0];
        }
        public static int getUserCount()
        {
            IDbConnection cnn = new SQLiteConnection("Data Source=mainDB.db;Version=3;");
            return cnn.Query<int>("SELECT max(rowid) from Users").ToList()[0];
        }
        public static void Execute(string query)
        {
            IDbConnection cnn = new SQLiteConnection("Data Source=mainDB.db;Version=3;");
            cnn.Execute(query);
        }
    }
    public class user
    {
        public int sNum { get; set; }
        public string Name { get; set; }
        public string Broker { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
    }
    public class Transaction
    {
        public int sNum { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string BuyDate { get; set; }
        public string BuyRate { get; set; }
        public string SellDate { get; set; }
        public string SellRate { get; set; }
        public string ProfitLoss { get; set; }
    }
    public class userData
    {
        public static List<Transaction> GetAllTransactions(string userID)
        {
            IDbConnection cnn = new SQLiteConnection($"Data Source=Users/{userID}.db;Version=3;");
            return cnn.Query<Transaction>("select * from Transactions").ToList();
        }
        public static void Execute(string userID, string query)
        {
            IDbConnection cnn = new SQLiteConnection($"Data Source=Users/{userID}.db;Version=3;");
            cnn.Execute(query);
        }
        public static List<Transaction> getStockNames(string userID)
        {
            IDbConnection cnn = new SQLiteConnection($"Data Source=Users/{userID}.db;Version=3;");
            return cnn.Query<Transaction>("select * from Transactions where Name != '' group by Name").ToList();
        }
    }
    #endregion
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Variables
        private string _themeColor;
        private string _baseColor;
        private string _backgroundColor;
        private string _fontFamily;
        //private int _buttonFontSize;
        public string themeColor { get { return _themeColor; } set { _themeColor = value; OnPropertyChanged(); } }
        public string baseColor { get { return _baseColor; } set { _baseColor = value; OnPropertyChanged(); } }
        public string backgroundColor { get { return _backgroundColor; } set { _backgroundColor = value; OnPropertyChanged(); } }
        public string fontFamily { get { return _fontFamily; } set { _fontFamily = value; OnPropertyChanged(); } }
        //public int buttonFontSize { get { return _buttonFontSize; } set { _buttonFontSize = value; OnPropertyChanged(); } }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            #region InitializeVariables

            themeColor = mainDB.get("select value from Variables where var = 'themeColor'")[0];
            backgroundColor = mainDB.get("select value from Variables where var = 'backgroundColor'")[0];
            baseColor = mainDB.get("select value from Variables where var = 'baseColor'")[0];
            fontFamily = mainDB.get("select value from Variables where var = 'fontFamily'")[0];

            #endregion

            addUserButton.Click += (s, e) =>
            {
                AddUser newUser = new AddUser();
                newUser.Show();
                newUser.saveUserDetails.Click += (s, e) =>
                {
                    mainDB.Execute($"insert into Users values ({mainDB.getUserCount() + 1},'{newUser.newUserNameTextBox.Text}','{newUser.newUserBrokerTextBox.Text}','{newUser.newUserUserIDTextBox.Text}','{newUser.newUserPasswordTextBox.Text}')");
                    File.Create($"Users/{newUser.newUserUserIDTextBox.Text}.db");
                    userList.Items.Add($"{newUser.newUserNameTextBox.Text} - {newUser.newUserUserIDTextBox.Text}");
                    newUser.Close();

                };
            };

            foreach (user i in mainDB.getUsers())
            {
                userList.Items.Add($"{i.Name} - {i.UserID}");
            }

            loginButton.Click += (s, e) =>
            {
                string userID = userList.SelectedItem.ToString().Split(" - ")[1];
                userData.Execute(userID, "CREATE TABLE IF NOT EXISTS 'Transactions' ('sNum' INTEGER, 'Name' TEXT, 'Quantity' TEXT, 'BuyDate' TEXT, 'BuyRate' TEXT, 'SellDate' TEXT, 'SellRate' TEXT, 'ProfitLoss' TEXT)");
                UserWindow userWindow = new UserWindow(userID) { DataContext = this };
                userWindow.ShowDialog();
            };

            preferencesButton.Click += (s, e) =>
            {
                Preferences pf = new Preferences { DataContext = this };
                pf.ShowDialog();
            };

        }
        #region PropertyChangeFunctions
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        #endregion
    }
}
