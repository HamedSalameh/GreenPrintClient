using Microsoft.Win32;
using System.Collections.Generic;

namespace GreenPrintClient.Helpers
{
    public class SettingManager
    {
        public static void UpdateClientID(string ClientID)
        {
            if (string.IsNullOrEmpty(ClientID))
                return;

            var key = Registry.CurrentUser.OpenSubKey("Software\\GreenPrint", true);
            if (key == null)
                return;

            key.DeleteValue("ClientID");
            key.SetValue("ClientID", ClientID, RegistryValueKind.String);
            key.Close();

        }

        public static Dictionary<string, string> LoadSettings()
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();

            using (RegistryKey root = Registry.CurrentUser.OpenSubKey("Software\\GreenPrint"))
            {
                foreach (string keyname in root.GetValueNames())
                {
                    var value = root.GetValue(keyname) as string;
                    settings.Add(keyname, value);
                }
            }

            return settings;
        }

        public static void updateRCCList(List<string> rcc)
        {
            var exists = Registry.CurrentUser.OpenSubKey("Software\\GreenPrint\\RCC", true);
            if (exists != null)
                Registry.CurrentUser.DeleteSubKeyTree("Software\\GreenPrint\\RCC");

            Registry.CurrentUser.CreateSubKey("Software\\GreenPrint\\RCC");
                

            using (RegistryKey root = Registry.CurrentUser.OpenSubKey("Software\\GreenPrint\\RCC", true))
            {
                // clear all entries
                    

                // write new entries
                var index = 1;
                foreach (string item in rcc)
                {
                    var sIndex = index < 10 ? "0" + index.ToString() :  index.ToString();
                    root.SetValue(sIndex, item, RegistryValueKind.String);
                    index++;
                }
            }
        }

        public static List<string> LoadRCCList()
        {
            var exists = Registry.CurrentUser.OpenSubKey("Software\\GreenPrint\\RCC");
            if (exists == null)
                Registry.CurrentUser.CreateSubKey("Software\\GreenPrint\\RCC");

            List<string> rcc = new List<string>();

            using (RegistryKey root = Registry.CurrentUser.CreateSubKey("Software\\GreenPrint\\RCC"))
            {
                foreach( string keyname in root.GetValueNames())
                {
                    var value = root.GetValue(keyname) as string;
                    if (string.IsNullOrEmpty(value) == false)
                    {
                        rcc.Add(value);
                    }
                }
            }

            return rcc;
        }
    }
}
