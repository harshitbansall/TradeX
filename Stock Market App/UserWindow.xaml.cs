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
                currentSelectedTransaction = transactionDataGrid.Items.GetItemAt(transactionDataGrid.SelectedIndex) as Transaction;
                nameTextBox.Text = currentSelectedTransaction.Name;
            };
            transactionDataGrid.PreviewMouseWheel += (s, e) =>
            {
                MyScrollViewer.ScrollToVerticalOffset(MyScrollViewer.VerticalOffset - e.Delta / 3);
            };
            saveTransactionButton.Click += (s, e) =>
            {
                userData.Execute(userID, $"Update Transactions set Name = '{nameTextBox.Text}' where sNum = {currentSelectedTransaction.sNum}");
            };
            //transactionDataGrid.RowEditEnding += (s, e) =>
            //{
            //    if (e.EditAction == DataGridEditAction.Cancel)
            //    {
            //        return;
            //    }
            //    if (e.EditAction == DataGridEditAction.Commit)
            //    {
            //        //var cellInfo = transactionDataGrid.SelectedCells[0];

            //        //var content = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
            //        //MessageBox.Show(content);



            //        //Transaction item = transactionDataGrid.Items.GetItemAt(transactionDataGrid.SelectedIndex) as Transaction;
            //        //MessageBox.Show(item.Name);
            //    }
            //};
        }
        //private void dataGridAddingNewItem(object sender, AddingNewItemEventArgs e)
        //{
        //    e.NewItem = new Transaction()
        //    {
        //        sNum = transactionDataGrid.Items.Count,
        //        Name = "-",
        //        Quantity = "-",
        //        BuyDate = "-",
        //        BuyRate = "-",
        //        SellDate = "-",
        //        SellRate = "-",
        //        ProfitLoss = "-",

        //    };

        //}
    }
}
