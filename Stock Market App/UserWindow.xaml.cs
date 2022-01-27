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

            transactionDataGrid.ItemsSource = userData.GetAllTransactions(userID);
        }
        private void dataGrid1_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Cancel)
            {
                return;
            }
            if (e.EditAction == DataGridEditAction.Commit)
            {
                MessageBox.Show("1");
            }
        }
        private void DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            MyScrollViewer.ScrollToVerticalOffset(MyScrollViewer.VerticalOffset - e.Delta / 3);
        }

        private void dataGridAddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            e.NewItem = new Transaction()
            {
                sNum = transactionDataGrid.Items.Count,
                Name = "-",
                Quantity = "-",
                BuyDate = "-",
                BuyRate = "-",
                SellDate = "-",
                SellRate = "-",
                ProfitLoss = "-",

            };

        }
    }
}
