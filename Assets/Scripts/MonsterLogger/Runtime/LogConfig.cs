using System;
using UnityEngine;

namespace MonsterLogger.Runtime
{
    internal class LogConfig
    {
        public string LogFilePath { get; set; } = Application.persistentDataPath + "/";
        public string LogFileName { get; set; } = Application.productName + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".log";
        public bool EnableLog { get; set; } = true;
        public bool EnableFileLogger { get; set; } = true;
        public LogLevel LogLevel { get; set; } = LogLevel.Info;
        public string Prefix { get; set; } = "[MonsterLogger]";
        public bool ShowTimeStamp { get; set; } = true;
        public bool ShowThreadId { get; set; } = true;
        public bool ShowColorName { get; set; } = true;

    }


}
