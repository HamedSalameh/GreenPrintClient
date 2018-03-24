using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPrintClient
{
    public static class Consts
    {
        public const int DEFAULT_MAX_SUPPORTED_SMS_LENGTH = 134;

        // Max supported phone number length, while taking into consideration international and local prefixes.
        public const int DEFAULT_MAX_PHONE_NUMBER_LENGTH = 20;

        public const int DEFAULT_MIN_PHONE_NUMBER_LENGTH = 10;

        // Based on RFC-5321, Errata against RFC-3696 for a valid EMail address length
        public const int DEFAULT_MAX_EMAIL_ADDRESS_LENGTH = 254;

        public const int DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC = 10;
    }
}
