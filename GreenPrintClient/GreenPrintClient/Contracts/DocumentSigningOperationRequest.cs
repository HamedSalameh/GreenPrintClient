using GreenPrintClient.Contracts;
using System;

namespace GreenPrintClient.Contracts
{
    [Serializable]
    public class DocumentSigningOperationRequest
    {
        public DocumentSigningOperationRequest(ClientAppVersion clientAppVersion)
        {
            if (clientAppVersion == null)
                throw new ArgumentException("ClientAppVersion is null", nameof(clientAppVersion));

            this.ClientVersion = clientAppVersion.ClientVersion;
            this.ClientOSBits = clientAppVersion.ClientOSBits;
            this.ClientOS = clientAppVersion.ClientOS;
            this.Description = clientAppVersion.Description;
        }

        public string ClientID { get; set; }

        public string DocumentName { get; set; }

        public byte[] DocumentBytes { get; set; }

        public string GuestSign_RecipientSMSNumber { get; set; }

        public string GuestSign_RecipientEmailAddress { get; set; }

        public string CarbonCopy_SMSPhoneNumbersList { get; set; }

        public string CarbonCopy_EMailAddressesList { get; set; }

        // product version
        // Stable version of the client application versbio
        public string ClientVersion { get; set; }

        // General text
        public string Description { get; set; }

        // Name of the current operating syste,
        public string ClientOS { get; set; }

        // 32bit (x86) or 64bit
        public string ClientOSBits { get; set; }
    }
}
