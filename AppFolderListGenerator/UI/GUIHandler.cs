using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace AppFolderListGenerator.UI {
    class GUIHandler {

        private ListView listView;

        public GUIHandler(ListView listView) { 
            this.listView = listView;
        }
        public void showData(List<string> installedApps) {
            foreach(string app in installedApps) {
                listView.Items.Add(app);
            }
        }
    }
}
