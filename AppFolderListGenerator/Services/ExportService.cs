using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFolderListGenerator.Services {
    public class ExportService {
    
        public void exportToTXT(List<string> seletctedApps) {
            File.WriteAllLines("../Apps.txt", seletctedApps);
        }

        public void exportToCSV(List<string> seletctedApps) {
            var csv = new StringBuilder();
            var header = string.Format("Apps");
            csv.AppendLine(header);
            foreach(string app in seletctedApps) {
                var newLine = string.Format(app);
                csv.AppendLine(newLine);
            }
            File.WriteAllText("../Apps.csv", csv.ToString());
        }
    }
}
