using System;
using System.Windows;

namespace GreenPrintClient.Contracts
{
    [Serializable]
    public class ClientAppVersion
    {
        public ClientAppVersion()
        {
            ClientOS = Environment.OSVersion.ToString();
            ClientOSBits = Environment.Is64BitOperatingSystem ? "64" : "32";
            ClientVersion = Application.Current.MainWindow.GetType().Assembly.GetName().Version.ToString();

            Description = "First release";
        }

        // Stable version of the client application versbio
        public string ClientVersion { get; private set;  }

        // General text
        public string Description { get; private set; }

        // Name of the current operating syste,
        public string ClientOS { get; private set; }

        // 32bit (x86) or 64bit
        public string ClientOSBits { get; private set; }
    }
}
