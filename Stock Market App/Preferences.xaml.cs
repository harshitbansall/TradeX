using System;
using System.Collections.Generic;
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
    public partial class Preferences : Window
    {
        public Preferences()
        {
            InitializeComponent();
        }
        private void themeColorInputBoxEnterEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                mainDB.Execute($"update Variables set value  = '{themeColorInputBox.Text}' where var = 'themeColor'");
            }
        }
        private void baseColorInputBoxEnterEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                mainDB.Execute($"update Variables set value  = '{baseColorInputBox.Text}' where var = 'baseColor'");
            }
        }
        private void backgroundColorInputBoxEnterEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                mainDB.Execute($"update Variables set value  = '{backgroundColorInputBox.Text}' where var = 'backgroundColor'");
            }
        }
    }
}
