using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
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


namespace Stock_Market_App
{
    public class userDB
    {
        public static List<user> getUsers()
        {
            IDbConnection cnn = new SQLiteConnection("Data Source=userDB.db;Version=3;");
            return cnn.Query<user>("select * from Users").ToList();
        }
        public static user getUserData(string query)
        {
            IDbConnection cnn = new SQLiteConnection("Data Source=userDB.db;Version=3;");
            return cnn.Query<user>($"select * from Users where Name like '%{query}%' or UserID like '%{query}%'").ToList()[0];
        }
        public static int getUserCount()
        {
            IDbConnection cnn = new SQLiteConnection("Data Source=userDB.db;Version=3;");
            return cnn.Query<int>("SELECT max(rowid) from Users").ToList()[0];
        }
        public static void Execute(string query)
        {
            IDbConnection cnn = new SQLiteConnection("Data Source=userDB.db;Version=3;");
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
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            addUserButton.Click += (s, e) =>
            {
                AddUser newUser = new AddUser();
                newUser.Show();
                newUser.saveUserDetails.Click += (s, e) =>
                {
                    userDB.Execute($"insert into Users values ({userDB.getUserCount() + 1},'{newUser.newUserNameTextBox.Text}','{newUser.newUserBrokerTextBox.Text}','{newUser.newUserUserIDTextBox.Text}','{newUser.newUserPasswordTextBox.Text}')");
                    File.Create($"Users/{newUser.newUserUserIDTextBox.Text}.db");
                    userList.Items.Add($"{newUser.newUserNameTextBox.Text} - {newUser.newUserUserIDTextBox.Text}");
                    newUser.Close();

                };
            };

            foreach (user i in userDB.getUsers())
            {
                userList.Items.Add($"{i.Name} - {i.UserID}");
            }
            loginButton.Click += (s, e) =>
            {
                string userID = userList.SelectedItem.ToString().Split(" - ")[1];
                IDbConnection cnn = new SQLiteConnection($"Data Source=Users/{userID}.db;Version=3;");
                cnn.Execute("CREATE TABLE IF NOT EXISTS Transactions (sNum INTEGER, Name TEXT, Quantity INTEGER, BuyDate TEXT, BuyRate INTEGER, SellDate TEXT, SellRate INTEGER, ProfitLoss INTEGER)");
                UserWindow userWindow = new UserWindow(userID);
                userWindow.Show();
                this.Close();
            };

        }
    }
}
