using Microsoft.Win32;
using System.Collections.Generic;

namespace GreenPrintClient.Helpers
{
    public class SettingManager
    {
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
    }
}
