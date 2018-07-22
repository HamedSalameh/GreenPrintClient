using System;
using System.Windows;

namespace GreenPrintClient.Contracts
{
    [Serializable]
    public class ClientAppVersionInfo
    {
        public ClientAppVersionInfo()
        {
            try
            {
                ClientAppOS = Environment.OSVersion.ToString();
            }
            catch (Exception)
            {
                ClientAppOS = "N/A";
            }

            try
            {
                ClientAppOSBits = Environment.Is64BitOperatingSystem ? "64" : "32";
            }
            catch (Exception)
            {
                ClientAppOSBits = "N/A";
            }

            try
            {
                ClientAppVersion = Application.Current.MainWindow.GetType().Assembly.GetName().Version.ToString();
            }
            catch (Exception)
            {
                ClientAppVersion = "0";
            }

            ClientAppDescription = "First release";
        }

        // Stable version of the client application versbio
        public string ClientAppVersion { get; private set;  }

        // General text
        public string ClientAppDescription { get; private set; }

        // Name of the current operating syste,
        public string ClientAppOS { get; private set; }

        // 32bit (x86) or 64bit
        public string ClientAppOSBits { get; private set; }
    }
}
