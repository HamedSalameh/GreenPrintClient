using GreenPrintClient.Helpers;
using System;

namespace GreenPrintClient.Contracts
{
    [Serializable]
    public class DocumentSigningOperationRequest
    {
        private int _id;
        private string _senderName;
        private string _username;
        private string _deviceSign_LinkedDeviceID;
        private string _documentName;
        private byte[] _documentBytes;
        private string _guestSign_RecipientSMSNumber;
        private string _guestSign_RecipientEmailAddress;
        private string _carbonCopy_SMSPhoneNumbersList;
        private string _carbonCopy_EMailAddressesList;
        private string _comments;
        private string _clientAppVersion;
        private string _clientAppDescription;
        private string _clientAppOS;
        private string _clientAppOSBits;

        public DocumentSigningOperationRequest(ClientAppVersionInfo clientAppVersion)
        {
            if (clientAppVersion == null)
                throw new ArgumentException("ClientAppVersion is null", nameof(clientAppVersion));

            this.ClientAppVersion = clientAppVersion.ClientAppVersion;
            this.ClientAppOSBits = clientAppVersion.ClientAppOSBits;
            this.ClientAppOS = clientAppVersion.ClientAppOS;
            this.ClientAppDescription = clientAppVersion.ClientAppDescription;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string SenderName
        {
            get
            {
                return _senderName;
            }
            set
            {
                if (value.Length > Consts.MAX_USERNAME_LENGTH)
                    throw new ArgumentException("Sender name is too long");

                _senderName = value;
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Username is null or empty");

                if (value.Length > Consts.MAX_USERNAME_LENGTH)
                    throw new ArgumentException("Username is too long");

                _username = value;
            }
        }

        public string DeviceSign_LinkedDeviceID
        {
            get
            {
                return _deviceSign_LinkedDeviceID;
            }
            set
            {
                if (value.Length > Consts.MAX_LINKED_DEVICE_ID_LENGTH)
                    throw new ArgumentException("Linked device ID is too long");

                _deviceSign_LinkedDeviceID = value;
            }
        }

        public string DocumentName
        {
            get
            {
                return _documentName;
            }
            set
            {
                if (value.Length > Consts.MAX_DOCUMENT_NAME_LENGTH)
                    throw new ArgumentException("Document name is too long");

                _documentName = value;
            }
        }

        public byte[] DocumentBytes
        {
            get
            {
                return _documentBytes;
            }
            set
            {
                if (value != null && value.Length > Consts.MAX_DOCUMENT_SIZE_IN_BYTES)
                    throw new ArgumentException("Document size is too big");

                _documentBytes = value;
            }
        }

        public string GuestSign_RecipientSMSNumber
        {
            get { return _guestSign_RecipientSMSNumber; }
            set
            {
                if (value.Length > Consts.DEFAULT_MAX_PHONE_NUMBER_LENGTH)
                    throw new ArgumentException("Phone number is too long");

                _guestSign_RecipientSMSNumber = value;
            }
        }

        public string GuestSign_RecipientEmailAddress
        {
            get
            {
                return _guestSign_RecipientEmailAddress;
            }
            set
            {
                if (value.Length > Consts.MAX_EMAIL_ADDRESS_LENGTH)
                    throw new ArgumentException("Email address is too long");

                _guestSign_RecipientEmailAddress = value;
            }
        }

        // Contains a comma seperated list of phone numbers that the signed document will be sent to
        // Max length : 10 numbers + 9 comma = 10x20+9 = 209 ==> 210
        public string CarbonCopy_SMSPhoneNumbersList
        {
            get
            {
                return _carbonCopy_SMSPhoneNumbersList;
            }
            set
            {
                if (value.Length > Consts.DEFAULT_MAX_PHONE_NUMBER_LENGTH * Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC + 9)
                    throw new ArgumentException("Phone numbers carbon copy (CC) list is too long");

                _carbonCopy_SMSPhoneNumbersList = value;
            }
        }

        // Contains a comma seperated list of email addresses that the signed document wil be sent to
        // Max length = default max X 10 (10 email addresses supported for sharing)
        public string CarbonCopy_EMailAddressesList
        {
            get
            {
                return _carbonCopy_EMailAddressesList;
            }
            set
            {
                if (value.Length > Consts.MAX_EMAIL_ADDRESS_LENGTH * Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC + 9)
                    throw new ArgumentException("Email Carbon copy (CC) list is too long");

                _carbonCopy_EMailAddressesList = value;
            }
        }

        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                if (value.Length > Consts.MAX_COMMENTS_LENGTH)
                    throw new ArgumentException("Comments text is too long");

                _comments = value;
            }
        }

        // product version
        // Stable version of the client application version
        public string ClientAppVersion
        {
            get { return _clientAppVersion; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    value = "0.0.0";

                if (value.Length > Consts.MAX_CLIENT_APP_VERSION_LENGTH)
                    throw new ArgumentException("Client app version value is too long");

                _clientAppVersion = value;
            }
        }

        // General text
        public string ClientAppDescription
        {
            get { return _clientAppDescription; }
            set
            {
                if (value.Length > Consts.MAX_CLIENT_APP_DESCRIPTION_LENGTH)
                    throw new ArgumentException("Client app description value is too long");

                _clientAppDescription = value;
            }
        }

        // Name of the current operating syste,
        public string ClientAppOS
        {
            get { return _clientAppOS; }
            set
            {
                if (value.Length > Consts.MAX_CLIENT_APP_OS_LENGTH)
                    throw new ArgumentException("Client app OS value is too long");

                _clientAppOS = value;
            }
        }

        // 32bit (x86) or 64bit (x64)
        public string ClientAppOSBits
        {
            get
            {
                return _clientAppOSBits;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    value = "32";

                if (value.Length > 4)
                    throw new ArgumentException("Client app OS bits value is too long");

                if (value.Contains("32") == false &&
                    value.Contains("64") == false &&
                    value.Contains("86") == false)
                    throw new ArgumentException("Client app OS bits contains unsupported values");

                if (value.Contains("86") || value.Contains("32"))
                    value = "32";

                if (value.Contains("64"))
                    value = "64";

                _clientAppOSBits = value;
            }
        }
    }
}
