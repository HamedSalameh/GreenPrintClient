using System.ComponentModel;

namespace GreenPrintClient.Helpers
{
    public class Enums
    {
        public enum UserStatus
        {
            [Description ("Not Registered")]
            NotRegistered = -1,

            [Description("Active")]
            Active = 0,

            Blocked = 1,

            Disabled = 2,

            [Description("Over Budget")]
            OverBudget = 3,

            [Description("Over Quota for Print Operations")]
            OverQuota_PrintOperations = 4,

            [Description("Over Quota for Storage Usage")]
            OverQuota_Storage = 5
        }

    }
}
