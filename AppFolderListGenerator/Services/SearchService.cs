using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFolderListGenerator.Services {
    public class SearchService {



        public List<string> startFolderSearch(string path) {
            var directories = Directory.GetDirectories(path);
            List<string> folders = new List<string>();
            foreach(var directory in directories) {
                folders.Add(directory.Replace(path + "\\", ""));
            }
            return folders;
        }

        public List<string> startInstalledAppSearch() {
           //On an 32 Bit Computer these two Strings are changed
           string registry_key_64 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
           string registry_key_32 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
           List<string> installedApps = new List<string>();
           using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key_32)) {
                foreach (string name in key.GetSubKeyNames()) {
                    using (RegistryKey subkey = key.OpenSubKey(name)) {
                        if(subkey.GetValue("DisplayName") != null) {
                            installedApps.Add(subkey.GetValue("DisplayName").ToString());
                        }
                    }
                }
            }
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key_64)) {
                foreach (string name in key.GetSubKeyNames()) {
                    using (RegistryKey subkey = key.OpenSubKey(name)) {
                        if(subkey.GetValue("DisplayName") != null) {
                            installedApps.Add(subkey.GetValue("DisplayName").ToString());
                        }
                    }
                }
            }
            return installedApps;
        }
    }
}
