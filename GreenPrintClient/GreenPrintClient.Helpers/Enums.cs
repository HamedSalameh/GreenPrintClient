using System.ComponentModel;

namespace GreenPrintClient.Helpers
{
    public class Enums
    {
        public enum UserStatus
        {
            [Description("Not Registered")]
            NotRegistered = -1,

            Active = 0,

            Blocked = 1,

            Disabled = 2,

            OverBudget = 3,

            OverQuota_PrintOperations = 4,

            OverQuota_Storage = 5
        }
    }
}
