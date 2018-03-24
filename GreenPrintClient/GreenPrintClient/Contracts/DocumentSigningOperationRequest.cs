using GreenPrintClient.Contracts;
using System;

namespace GreenPrintClient.Contracts
{
    [Serializable]
    public class DocumentSigningOperationRequest
    {
        public DocumentSigningOperationRequest(ClientAppVersionInfo clientAppVersion)
        {
            if (clientAppVersion == null)
                throw new ArgumentException("ClientAppVersion is null", nameof(clientAppVersion));

            this.ClientAppVersion = clientAppVersion.ClientAppVersion;
            this.ClientAppOSBits = clientAppVersion.ClientAppOSBits;
            this.ClientAppOS = clientAppVersion.ClientAppOS;
            this.ClientAppDescription = clientAppVersion.ClientAppDescription;
        }

        public string ClientID { get; set; }

        public string DocumentName { get; set; }

        public byte[] DocumentBytes { get; set; }

        public string GuestSign_RecipientSMSNumber { get; set; }

        public string GuestSign_RecipientEmailAddress { get; set; }

        public string CarbonCopy_SMSPhoneNumbersList { get; set; }

        public string CarbonCopy_EMailAddressesList { get; set; }

        public string Comments { get; set; }

        // product version
        // Stable version of the client application versbio
        public string ClientAppVersion { get; set; }

        // General text
        public string ClientAppDescription { get; set; }

        // Name of the current operating syste,
        public string ClientAppOS { get; set; }

        // 32bit (x86) or 64bit
        public string ClientAppOSBits { get; set; }
    }
}
