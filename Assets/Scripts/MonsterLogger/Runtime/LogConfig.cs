using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MonsterLogger.Runtime
{
    internal class LogConfig
    {
        public bool EnableLog { get; set; } = true;
        public string Prefix { get; set; } = "[MonsterLogger]";
        public bool ShowTimeStamp { get; set; } = true;
        public bool ShowThreadId { get; set; } = true;
        public bool EnableLogFileSave { get; set; } = true;
        public bool ShowColorName { get; set; } = true;
        public string LogFilePath { get; set; } = Application.persistentDataPath + "/";
        public string LogFileName { get; set; } = Application.productName + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".log";
    }
}
