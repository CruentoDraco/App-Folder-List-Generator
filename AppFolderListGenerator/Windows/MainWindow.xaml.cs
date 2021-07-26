using System;
using System.Collections.Generic;
using System.Data;
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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace AppFolderListGenerator.Windows {
    /// <summary>
    /// Interaction Logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public MainWindow() {
            InitializeComponent();
        }

        private void startSearchBtn_Click(object sender, RoutedEventArgs e) {
            //Installed Applications
            if (SearchTypeCB.SelectedIndex == 0) {
                this.ShowMessageAsync("Test", "Installed Applications");
            }
            //Installed MS Store Applications
            else if (SearchTypeCB.SelectedIndex == 1) {
                this.ShowMessageAsync("Test", "Installed MS Store Applications");
            }
            //Folder
            else if (SearchTypeCB.SelectedIndex == 2) {
                this.ShowMessageAsync("Test", "Folder");
            }
        }

        private void startConvertionBtn_Click(object sender, RoutedEventArgs e) {
            //TextFile Export
            if (fileTypeCB.SelectedIndex == 0) {
                this.ShowMessageAsync("Test", "TextFile");
            }
            
            //CSV Export
            else if (fileTypeCB.SelectedIndex == 1) {
                this.ShowMessageAsync("Test", "CSV");
            }
        }


        /* dr = Data Row ; dt = DataTable ; dg = DataGrid
            dr["ID"] = item.Attributes["id"].Value;
            dr["Grund"] = item.ChildNodes[(int)RuntimeConfig.DataColumns.Purpose].InnerText;
            dr["Art"] = item.ChildNodes[(int)RuntimeConfig.DataColumns.Type].InnerText;
            dr["Betrag"] = item.ChildNodes[(int)RuntimeConfig.DataColumns.Value].InnerText;
            dr["Vorgeplant"] = item.ChildNodes[(int)RuntimeConfig.DataColumns.Preplanned].InnerText;
            dr["Datum"] = item.ChildNodes[(int)RuntimeConfig.DataColumns.Date].InnerText;
            dt.Rows.Add(dr);
            dg.DataContext = dt.DefaultView;
         */
    }
}
