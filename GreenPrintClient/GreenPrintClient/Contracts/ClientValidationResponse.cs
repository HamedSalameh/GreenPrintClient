using System;

namespace GreenPrintClient.Contracts
{
    public class ClientValidationResponse
    {
        public int HttpStatusCode { get; set; }

        public int UserStatus { get; set; }

        public string Message { get; set; }

        public string HyperLinkName { get; set; }

        public string HyperLink { get; set; }
    }
}
