using GreenPrintClient.Helpers;
using System;

namespace GreenPrintClient
{
    [Serializable]
    public class DocumentSigningOperationRequest
    {
        public int Id { get; set; }

        public string SenderName { get; set; }

        public string Username { get; set; }

        public string DeviceSign_LinkedDeviceID { get; set; }

        public string DocumentName { get; set; }

        public byte[] DocumentBytes { get; set; }

        public string GuestSign_RecipientSMSNumber { get; set; }

        public string GuestSign_RecipientEmailAddress { get; set; }

        // Contains a comma seperated list of phone numbers that the signed document will be sent to
        // Max length : 10 numbers + 9 comma = 10x20+9 = 209 ==> 210
        public string CarbonCopy_SMSPhoneNumbersList { get; set; }

        // Contains a comma seperated list of email addresses that the signed document wil be sent to
        // Max length = default max X 10 (10 email addresses supported for sharing)
        public string CarbonCopy_EMailAddressesList { get; set; }

        public string Comments { get; set; }
    }
}
