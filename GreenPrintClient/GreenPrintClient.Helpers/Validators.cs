using System;

namespace GreenPrintClient.Helpers
{
    public static class Validators
    {
        public static bool IsValidURI(string URI)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(URI, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }

    }
}
