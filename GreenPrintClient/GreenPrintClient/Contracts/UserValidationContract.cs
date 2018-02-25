using Newtonsoft.Json;
using System;

namespace GreenPrintClient.Contracts
{
    [Serializable]
    public class UserValidation
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("Password")]
        public string password { get; set; }
    }
}
