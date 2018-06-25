using Microsoft.Win32;
using System.Collections.Generic;

namespace GreenPrintClient.Helpers
{
    public class SettingManager
    {
        public static void UpdateUsername(string Username)
        {
            if (string.IsNullOrEmpty(Username))
                return;

            var key = Registry.CurrentUser.OpenSubKey("Software\\GreenPrint", true);
            if (key == null)
                return;

            key.DeleteValue(Consts.ConfigurationSetting_Username);
            key.SetValue(Consts.ConfigurationSetting_Username, Username, RegistryValueKind.String);
            key.Close();

        }

        public static Dictionary<string, string> LoadSettings()
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();

            using (RegistryKey root = Registry.CurrentUser.OpenSubKey("Software\\GreenPrint"))
            {
                if (root != null && root.ValueCount > 0)
                {
                    foreach (string keyname in root.GetValueNames())
                    {
                        var value = root.GetValue(keyname) as string;
                        settings.Add(keyname, value);
                    }
                }
                else
                {
                    // assuming no configuration at all exists
                    createRegistryDefaultSettings();
                }
            }

            return settings;
        }

        private static void createRegistryDefaultSettings()
        {
            try
            {
                var gpclientBase = Registry.CurrentUser.CreateSubKey("Software\\GreenPrint");
                Registry.CurrentUser.CreateSubKey("Software\\GreenPrint\\RCC");

                gpclientBase.SetValue(Consts.ConfigurationSetting_InboxFolder, "Inbox");
                gpclientBase.SetValue(Consts.ConfigurationSetting_SubmittedFolder, "Submitted");
                gpclientBase.SetValue(Consts.ConfigurationSetting_FailedFolder, "Failed");
                gpclientBase.SetValue(Consts.ConfigurationSetting_GPServicesBase, "GPServicesBase");
                gpclientBase.SetValue(Consts.ConfigurationSetting_GPServerURL, "GPServerURL");
                gpclientBase.SetValue(Consts.ConfigurationSetting_USS, "USS");
                gpclientBase.SetValue(Consts.ConfigurationSetting_PRS, "PRS");
                gpclientBase.SetValue(Consts.ConfigurationSetting_Username, string.Empty);
            }
            catch (System.Exception)
            {
                throw;
            }
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
                    var sIndex = index < 10 ? "0" + index.ToString() : index.ToString();
                    root.SetValue(sIndex, item, RegistryValueKind.String);
                    index++;
                }
            }
        }

        public static string LoadSetting(string Key)
        {
            string value = string.Empty ;
            using (RegistryKey root = Registry.CurrentUser.OpenSubKey("Software\\GreenPrint"))
            {
                if (root != null && root.ValueCount > 0)
                {
                    value = root.GetValue(Key) as string;
                }
                else
                {
                    // assuming no configuration at all exists
                    createRegistryDefaultSettings();
                }
            }

            return value;
        }

        public static List<string> LoadRCCList()
        {
            var exists = Registry.CurrentUser.OpenSubKey("Software\\GreenPrint\\RCC");
            if (exists == null)
                Registry.CurrentUser.CreateSubKey("Software\\GreenPrint\\RCC");

            List<string> rcc = new List<string>();

            using (RegistryKey root = Registry.CurrentUser.CreateSubKey("Software\\GreenPrint\\RCC"))
            {
                foreach (string keyname in root.GetValueNames())
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
