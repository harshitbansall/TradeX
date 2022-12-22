using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;
using System.Windows.Media;
using System.Globalization;
using System.Diagnostics;

namespace Stock_Market_App
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double id = 0;
            if (value != null && double.TryParse(value.ToString(), out id))
            {
                if (id >= 0) { return new SolidColorBrush(Colors.Green); }
                else if (id <0) { return new SolidColorBrush(Colors.Red); }
            }
            return new SolidColorBrush(Colors.Blue);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture){throw new NotImplementedException();}
    }
    public class ProfitLoss : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                decimal buyRate = (values[1] != null && values[1] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[1]) : 0;
                decimal sellRate = (values[0] != null && values[0] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[0]) : 0;
                decimal quantity = (values[2] != null && values[2] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[2]) : 0;
                string profitLoss = ((sellRate - buyRate) * quantity).ToString();
                return profitLoss;
            }
            catch
            {
                return "Not Applicable";
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class Margin : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                decimal Rate = (values[0] != null && values[0] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[0]) : 0;
                decimal quantity = (values[1] != null && values[1] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[1]) : 0;
                string Margin = (Rate * quantity).ToString();
                return Margin;
            }
            catch
            {
                return "Not Applicable";
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public partial class UserWindow : Window, INotifyPropertyChanged
    {
        public UserWindow(string userID)
        {
            InitializeComponent();
            void refreshTable()
            {
                try
                {
                    transactionDataGrid.Items.Refresh();
                }
                catch
                {
                    
                }
            }
            user currentUser = mainDB.getUserData(userID);
            this.Title = $"{currentUser.Name}'s Transactions";
            List<Transaction> totalTransactions = userData.GetAllTransactions(userID);
            totalTransactions.Reverse();

            transactionDataGrid.ItemsSource = totalTransactions;
            profileLossLabel.Content = totalTransactions.Where(item => item.ProfitLoss != "").Sum(item => float.Parse(item.ProfitLoss));

            foreach (Transaction i in userData.getStockNames(userID))
            { nameTextBox.Items.Add(i.Name); }


            Transaction currentSelectedTransaction = null;
            transactionDataGrid.SelectionChanged += (s, e) =>
            {
                transactionDetailsFrame.Visibility = Visibility.Visible;
                Grid.SetColumnSpan(userDataFrame,1);
                currentSelectedTransaction = transactionDataGrid.SelectedItem as Transaction;
            };
            addTransactionButton.Click += (s, e) =>
            {
                Transaction newTransaction = new Transaction() { sNum = transactionDataGrid.Items.Count + 1, Name = "", Quantity = "", BuyDate = "", BuyRate = "", SellDate = "", SellRate = "", ProfitLoss = "" };
                totalTransactions.Insert(0, newTransaction);
                userData.Execute(userID, $"Insert into Transactions values ({newTransaction.sNum},'','','','','','','')");
                transactionDataGrid.SelectedIndex = 0;
                refreshTable();
            };
            saveTransactionButton.Click += (s, e) =>
            {
                string profitLoss = null;
                if (profitLossTextBox.Text == "Not Applicable")
                {
                    profitLoss = "";
                    currentSelectedTransaction.ProfitLoss = "";
                }
                else
                {
                    profitLoss = profitLossTextBox.Text;
                    currentSelectedTransaction.ProfitLoss = profitLossTextBox.Text;

                }
                refreshTable();
                userData.Execute(userID, $"Update Transactions set Name = '{nameTextBox.Text}', Quantity = '{quantityTextBox.Text}', BuyDate = '{buyDatePicker.Text}', BuyRate = '{buyRateTextBox.Text}', SellDate = '{sellDatePicker.Text}', SellRate = '{sellRateTextBox.Text}', ProfitLoss = '{profitLoss}' where sNum = {currentSelectedTransaction.sNum}");
                transactionDetailsFrame.Visibility = Visibility.Collapsed;
                Grid.SetColumnSpan(userDataFrame, 2);
                MessageBox.Show("Saved Transaction");
            };
            deleteTransactionButton.Click += (s, e) =>
            {
                if (MessageBox.Show($"Delete Transaction {currentSelectedTransaction.sNum}?", "Delete Transaction", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    totalTransactions.RemoveAt(transactionDataGrid.Items.Count - currentSelectedTransaction.sNum);
                    userData.Execute(userID, $"Delete from Transactions where sNum = '{currentSelectedTransaction.sNum}'");
                    refreshTable();
                }
            };

            hideTransactionDetailsButton.Click += (s, e) =>
            {
                transactionDetailsFrame.Visibility = Visibility.Collapsed;
                Grid.SetColumnSpan(userDataFrame, 2);
            };
            preferencesButton.Click += (s, e) =>
            {
                Preferences pf = new Preferences() { DataContext = new MainWindow() };
                pf.Show();
            };
            logoutButton.Click += (s, e) =>
            {
                //MainWindow main = new MainWindow();
                //main.Show();
                this.Close();
            };
            //transactionDataGrid.PreviewMouseWheel += (s, e) =>
            //{
            //    userDataScrollViewer.ScrollToVerticalOffset(userDataScrollViewer.VerticalOffset - e.Delta / 3);
            //};


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
