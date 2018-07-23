namespace GreenPrintClient.Helpers
{
    public static class Consts
    {
        public const string _inboxfolder = "_inbox";
        public const string _failedfolder = "_failed";
        public const string _submittedfolder = "_submitted";
        // Max supported phone number length, while taking into consideration international and local prefixes.
        public const int DEFAULT_MAX_PHONE_NUMBER_LENGTH = 20;

        public const int DEFAULT_MIN_PHONE_NUMBER_LENGTH = 10;

        // Based on RFC-5321, Errata against RFC-3696 for a valid EMail address length
        public const int DEFAULT_MAX_EMAIL_ADDRESS_LENGTH = 254;

        public const int DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC = 10;

        public const string ConfigurationSetting_InboxFolder = "InboxFolder";

        public const string ConfigurationSetting_SubmittedFolder = "SubmittedFolder";
        public const string ConfigurationSetting_FailedFolder = "FailedFolder";
        public const string ConfigurationSetting_GPServicesBase = "GPServicesBase";
        public const string ConfigurationSetting_GPServerURL = "GPServerURL";
        public const string ConfigurationSetting_USS = "USS";
        public const string ConfigurationSetting_PRS = "PRS";
        public const string ConfigurationSetting_Username = "Username";

        private const int HALF_MEGA_BYTE = 524288;
        private const int ONE_MEGA_BYTE = HALF_MEGA_BYTE * 2;

        public const int MAX_SUPPORTED_SMS_LENGTH = 134;   // Two automatically splitted SMS messages
        public const int DEFAULT_TTL_FOR_DELIVERY = 4320;          // 3 days

        // Max supported phone number length, while taking into consideration international and local prefixes.
        public const int MAX_PHONE_NUMBER_LENGTH = 20;

        // Based on RFC-5321, Errata against RFC-3696 for a valid EMail address length
        public const int MAX_EMAIL_ADDRESS_LENGTH = 254;

        // Based on RFC-2822 - "Internet Message Format" (Recommendation length, without folding)
        public const int MAX_EMAIL_SUBJECT_LENGTH = 78;

        public const int MAX_EMAIL_BODY_LENGTH = 1024;

        // For languages that use UTF, each character used 16 bits, thus limiting the SMS max length to 70
        // The max SMS text size is set to 134, which will automatically be splitted to 2 message, 67 chars each
        public const int MAX_SMS_MESSAGE_TEXT_LENGTH = 134;

        // Based on StackOverflow article https://stackoverflow.com/a/417184/1549608
        public const int MAX_SUPPORTED_URL_LENGTH = 2000;

        // Signature size per page is limited to 0.5 MB
        public const int MAX_SIGNATURE_SIZE_PER_PAGE = HALF_MEGA_BYTE;

        // Max document size
        public const int MAX_DOCUMENT_SIZE_IN_BYTES = 10 * ONE_MEGA_BYTE;

        public const int MAX_USERNAME_LENGTH = MAX_EMAIL_ADDRESS_LENGTH;
        public const int MAX_LINKED_DEVICE_ID_LENGTH = MAX_EMAIL_ADDRESS_LENGTH;
        public const int MAX_DOCUMENT_NAME_LENGTH = 255;
        public const int MAX_COMMENTS_LENGTH = 1024;

        public const string LOGIN_TYPE_WEBAPP = "Webapp";
        public const string LOGIN_TYPE_DEVICE = "Device";

        public const int MAX_CLIENT_APP_DESCRIPTION_LENGTH = 100;
        public const int MAX_CLIENT_APP_VERSION_LENGTH = 10;
        public const int MAX_CLIENT_APP_OS_LENGTH = 64;

    }
}
