using System.Diagnostics;

namespace GreenPrintClient.Helpers
{
    public static class Logger
    {
        public static  void LogError(string Message, int EventID = 0, short Category = 0)
        {
            _log(Message, EventLogEntryType.Error, EventID, Category);
        }

        public static void LogInfo(string Message, int EventID = 0, short Category = 0)
        {
            _log(Message, EventLogEntryType.Information, EventID, Category);
        }

        public static void LogWarning(string Message, int EventID = 0, short Category = 0)
        {
            _log(Message, EventLogEntryType.Warning, EventID, Category);
        }

        private static void _log(string Message,EventLogEntryType eventLogEntryType, int EventID, short Category)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                if (EventID != 0 && Category != 0)
                    eventLog.WriteEntry(Message, eventLogEntryType, EventID, Category);
                else
                    eventLog.WriteEntry(Message, eventLogEntryType); //, EventID, Category);
            }
        }
    }
}
