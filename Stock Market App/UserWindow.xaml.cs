using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Stock_Market_App
{
    public partial class UserWindow : Window
    {
        public UserWindow(string userID)
        {
            InitializeComponent();
            user currentUser = userDB.getUserData(userID);
            this.Title = $"{currentUser.Name}'s Transactions";
            List<Transaction> totalTransactions = userData.GetAllTransactions(userID);
            transactionDataGrid.ItemsSource = totalTransactions;

            Transaction currentSelectedTransaction = null;
            transactionDataGrid.SelectionChanged += (s, e) =>
            {
                if (sellRateTextBox.Text != "")
                {
                    profitLossTextBox.Text = ((float.Parse(sellRateTextBox.Text) - float.Parse(buyRateTextBox.Text)) * float.Parse(quantityTextBox.Text)).ToString();
                }
                transactionDetailsFrame.Visibility = Visibility.Visible;
                Grid.SetColumnSpan(userDataFrame,1);
                currentSelectedTransaction = transactionDataGrid.Items.GetItemAt(transactionDataGrid.SelectedIndex) as Transaction;
                nameTextBox.Text = currentSelectedTransaction.Name;
            };
            transactionDataGrid.PreviewMouseWheel += (s, e) =>
            {
                userDataFrame.ScrollToVerticalOffset(userDataFrame.VerticalOffset - e.Delta / 3);
            };
            saveTransactionButton.Click += (s, e) =>
            {
                userData.Execute(userID, $"Update Transactions set Name = '{nameTextBox.Text}', Quantity = '{quantityTextBox.Text}', BuyDate = '{buyDatePicker.Text}', BuyRate = '{buyRateTextBox.Text}', SellDate = '{sellDatePicker.Text}', SellRate = '{sellRateTextBox.Text}', ProfitLoss = '{profitLossTextBox.Text}' where sNum = {currentSelectedTransaction.sNum}");
            };
            hideTransactionDetailsButton.Click += (s, e) =>
            {
                transactionDetailsFrame.Visibility = Visibility.Collapsed;
                Grid.SetColumnSpan(userDataFrame, 2);
            };
            
        }
    }
}
