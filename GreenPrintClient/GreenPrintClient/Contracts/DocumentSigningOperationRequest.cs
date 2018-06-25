using System;

namespace GreenPrintClient
{
    [Serializable]
    public class DocumentSigningOperationRequest
    {
        public string ClientID { get; set; }

        public string DocumentName { get; set; }

        public byte[] DocumentBytes { get; set; }

        public string GuestSign_RecipientSMSNumber { get; set; }

        public string GuestSign_RecipientEmailAddress { get; set; }

        public string CarbonCopy_SMSPhoneNumbersList { get; set; }

        public string CarbonCopy_EMailAddressesList { get; set; }
    }
}
