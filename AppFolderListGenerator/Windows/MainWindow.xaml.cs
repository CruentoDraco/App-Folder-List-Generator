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
using AppFolderListGenerator.Services;
using AppFolderListGenerator.UI;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace AppFolderListGenerator.Windows {
    /// <summary>
    /// Interaction Logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        
        private SearchService searchService;
        private GUIHandler guiHandler;
        public MainWindow() {
            InitializeComponent();
            this.searchService = new SearchService();
            this.guiHandler = new GUIHandler(this.searchResult_lv);
        }

        private void startSearchBtn_Click(object sender, RoutedEventArgs e) {
            //Installed Applications
            if (SearchTypeCB.SelectedIndex == 0) {
                this.guiHandler.showData(this.searchService.startInstalledAppSearch());
            }
            //Folder
            else if (SearchTypeCB.SelectedIndex == 1) {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;
                if(dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                    this.guiHandler.showData(this.searchService.startFolderSearch(dialog.FileName));
                }
                
                    
            }
        }

        private void startConvertionBtn_Click(object sender, RoutedEventArgs e) {
            //TextFile Export
            if (fileTypeCB.SelectedIndex == 0) {
                this.ShowMessageAsync("Test", "Textfile");
            }
            //CSV Export
            else if (fileTypeCB.SelectedIndex == 1) {
                this.ShowMessageAsync("Test", "CSV");
            }
        }

        private void btn_add_all_Click(object sender, RoutedEventArgs e) {
            if(this.searchResult_lv.Items.Count != 0) {
                foreach(string item in this.searchResult_lv.Items) {
                    this.export_lv.Items.Add(item);
                    
                }
                this.searchResult_lv.Items.Clear();                
            } 
        }

        private void btn_add_Click(object sender, RoutedEventArgs e) {
            this.changeListView(this.searchResult_lv, this.export_lv);
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e) {
            this.changeListView(this.export_lv, this.searchResult_lv);
        }

        private void changeListView(ListView fromListView, ListView toListView) {
            if(fromListView.SelectedItems.Count != 0) {
                List<string> sItems = new List<String>();
                foreach(string item in fromListView.SelectedItems) {
                    toListView.Items.Add(item);
                    sItems.Add(item);
                }
                foreach(string item in sItems) {
                    fromListView.Items.Remove(item);
                }
            }
        }

        private void btn_remove_all_Click(object sender, RoutedEventArgs e) {
            if(this.export_lv.Items.Count != 0) {
                foreach(string item in this.export_lv.Items) {
                    this.searchResult_lv.Items.Add(item);
                }
                this.export_lv.Items.Clear();
            }
        }

        private void searchResult_lv_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(this.searchResult_lv.SelectedItems.Count != 0) {
                this.btn_add.IsEnabled = true;
            } else {
                this.btn_add.IsEnabled = false;
            }
            
        }

        private void export_lv_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(this.export_lv.SelectedItems.Count != 0) {
                this.btn_remove.IsEnabled = true;
            } else {
                this.btn_remove.IsEnabled = false;
            }
        }
    }
}
