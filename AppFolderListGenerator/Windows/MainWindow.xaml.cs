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
using ControlzEx.Theming;
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
        private ExportService exportService;
        private GUIHandler guiHandler;
        private List<string> cachedApps;
        private List<string> cachedSelectedApps;
        public MainWindow() {
            InitializeComponent();
            this.searchService = new SearchService();
            this.exportService = new ExportService();
            this.guiHandler = new GUIHandler(this.searchResult_lv);
            cachedApps = new List<string>();
            cachedSelectedApps = new List<string>();
            this.search_ResultLv_TB.SetValue(MahApps.Metro.Controls.TextBoxHelper.ClearTextButtonProperty, true);
            this.search_ResultLv_TB.SetValue(MahApps.Metro.Controls.TextBoxHelper.WatermarkProperty, "Suche nach Apps");
            this.search_ExportLv_TB.SetValue(MahApps.Metro.Controls.TextBoxHelper.ClearTextButtonProperty, true);
            this.search_ExportLv_TB.SetValue(MahApps.Metro.Controls.TextBoxHelper.WatermarkProperty, "Suche nach ausgewählten Apps");
        }
        private void setWinPosition(MetroWindow window, int addToLeft, int addToTop) {
            window.Left = Left + addToLeft;
            window.Top = Top + addToTop;
        }

        private void startSearchBtn_Click(object sender, RoutedEventArgs e) {
            //Installed Applications
            if (SearchTypeCB.SelectedIndex == 0) {
                cachedApps = this.searchService.startInstalledAppSearch();
                this.guiHandler.showData(cachedApps);
            }
            //Folder
            else if (SearchTypeCB.SelectedIndex == 1) {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;
                if(dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                    cachedApps = this.searchService.startFolderSearch(dialog.FileName);
                    this.guiHandler.showData(cachedApps);
                }
                    
            }
        }            

        private void startConvertionBtn_Click(object sender, RoutedEventArgs e) {
            //TextFile Export
            List<string> selectedApps = new List<string>();
            foreach(string item in export_lv.Items) {
                selectedApps.Add(item);
                cachedApps.Remove(item);
            }
            searchResult_lv.Items.Clear();
            export_lv.Items.Clear();
            if (fileTypeCB.SelectedIndex == 0) {
                this.exportService.exportToTXT(selectedApps);
                this.ShowMessageAsync("Finish!!", "Apps.txt erstellt!!");
            }
            //CSV Export
            else if (fileTypeCB.SelectedIndex == 1) {
                this.exportService.exportToCSV(selectedApps);
                this.ShowMessageAsync("Finish!!", "Apps.csv erstellt!!");
            }
        }

        private void btn_add_all_Click(object sender, RoutedEventArgs e) {
            if(this.searchResult_lv.Items.Count != 0) {
                foreach(string item in this.searchResult_lv.Items) {
                    this.export_lv.Items.Add(item);
                    cachedSelectedApps.Add(item);
                }
                this.searchResult_lv.Items.Clear();                
            } 
        }

        private void btn_add_Click(object sender, RoutedEventArgs e) {
            this.changeListView(this.searchResult_lv, this.export_lv, "toExport");
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e) {
            this.changeListView(this.export_lv, this.searchResult_lv, "toSearchResult");
        }

        private void changeListView(ListView fromListView, ListView toListView, string mode) {
            if(fromListView.SelectedItems.Count != 0) {
                List<string> sItems = new List<String>();
                foreach(string item in fromListView.SelectedItems) {
                    toListView.Items.Add(item);
                    if(mode == "toExport") {
                        cachedSelectedApps.Add(item);
                        cachedApps.Remove(item);
                    } else {
                        cachedSelectedApps.Remove(item);
                        cachedApps.Add(item);
                    }
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
                cachedSelectedApps.Clear();
            }
        }

        private void searchResult_lv_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            setBtnEnabledState(btn_add, searchResult_lv);            
        }

        private void export_lv_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            setBtnEnabledState(btn_remove, export_lv);
        }

        private void setBtnEnabledState(Button btn, ListView listview) {
            if(listview.SelectedItems.Count != 0) {
                btn.IsEnabled = true;
            } else {
                btn.IsEnabled = false;
            }
        }

        private void clearSearchResultLv_Btn_Click(object sender, RoutedEventArgs e) {
            this.searchResult_lv.Items.Clear();
            cachedApps.Clear();
        }

        private void clearSearchExportLv_Btn_Click(object sender, RoutedEventArgs e) {
            foreach(string item in export_lv.Items) {
                cachedApps.Remove(item);
            }
            this.export_lv.Items.Clear();
            cachedSelectedApps.Clear();
        }

        private void search_ResultLv_TB_KeyUp(object sender, KeyEventArgs e) {
            this.search(search_ResultLv_TB, searchResult_lv, cachedApps);
        }

        private void search_ExportLv_TB_KeyUp(object sender, KeyEventArgs e) {
            this.search(search_ExportLv_TB, export_lv, cachedSelectedApps);           
        }

        private void search(TextBox textBox, ListView listView, List<string> list) {
            if(textBox.Text == "") {
                listView.Items.Clear();
                foreach(string item in list) {
                    listView.Items.Add(item);
                }
            } else {
                foreach(string item in list) {
                    if(!item.ToLower().Contains(textBox.Text.ToLower())) {
                        listView.Items.Remove(item);
                    } else {
                        if(!listView.Items.Contains(item)) {
                            listView.Items.Add(item);
                        }

                    }
                }
            }

        }
    }
}
