using System;
using System.Diagnostics;


namespace DLMApp_ModulesLayer
{
    public enum enLogType : byte { Error, Warning, Information }

    public class clsEventLog
    {
        private static string _sourceName = "Code Generator App";


        public static void WriteToEventLog(string Message, enLogType LogType)
        {
            if (!EventLog.SourceExists(_sourceName))
            {
                EventLog.CreateEventSource(_sourceName, "Application");
            }

            ChooseCorrectLog(Message, LogType);
        }

        private static void ChooseCorrectLog(string Message, enLogType LogType)
        {
            switch (LogType)
            {
                case enLogType.Error:
                    {
                        EventLog.WriteEntry(_sourceName, Message, EventLogEntryType.Error);
                    }
                    break;
                case enLogType.Warning:
                    {
                        EventLog.WriteEntry(_sourceName, Message, EventLogEntryType.Warning);
                    }
                    break;
                default:
                    {
                        EventLog.WriteEntry(_sourceName, Message, EventLogEntryType.Information);
                    }
                    break;
            }
        }
    }
}
