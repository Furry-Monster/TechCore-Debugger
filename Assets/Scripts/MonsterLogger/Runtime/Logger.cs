using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace MonsterLogger.Runtime
{
    internal static class Logger
    {
        private static LogConfig _cfg;

        [Conditional("MST_USE_LOG")]
        internal static void Initialize(LogConfig config = null)
        {
            _cfg = config ?? new LogConfig
            {
                LogFilePath = Application.persistentDataPath + "/",
                LogFileName = Application.productName + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".log",
                EnableLog = EditorPrefs.GetBool("MonsterLogger_IsEnabled", true),
                EnableFileLogger = EditorPrefs.GetBool("MonsterLogger_WriteToFile", false),
                LogLevel = (LogLevel)EditorPrefs.GetInt("MonsterLogger_LogLevel", (int)LogLevel.Info),
                Prefix = "[MonsterLogger]",
                ShowTimeStamp = true,
                ShowThreadId = true,
                ShowColorName = true
            };


            // 启用文件日志记录
            if (_cfg.EnableFileLogger)
            {
                var go = new GameObject("FileLogger");
                if (go == null)
                    throw new Exception(
                        "Failed to create GameObject for FileLogger. Ensure you are running in a Unity environment.");
                var comp = go.AddComponent<FileLogger>();
                Object.DontDestroyOnLoad(go);
                comp.Initialize(_cfg.LogFilePath, _cfg.LogFileName, _cfg.LogLevel);
            }
        }

        #region Log

        [Conditional("MST_USE_LOG")]
        internal static void Log(object obj)
        {
            if (!_cfg.EnableLog || _cfg.LogLevel > LogLevel.Info)
                return;
            Debug.Log(GenerateLog(obj?.ToString()));
        }

        [Conditional("MST_USE_LOG")]
        internal static void Log(object obj, LogColor color)
        {
            if (!_cfg.EnableLog || _cfg.LogLevel > LogLevel.Info)
                return;
            Debug.Log(LogColoring(GenerateLog(obj?.ToString(), color), color));
        }

        [Conditional("MST_USE_LOG")]
        internal static void Log(object obj, params object[] args)
        {
            if (!_cfg.EnableLog || _cfg.LogLevel > LogLevel.Info)
                return;
            Debug.Log(GenerateLog(ConcatMessage(obj, args)));
        }

        [Conditional("MST_USE_LOG")]
        internal static void Log(object obj, LogColor color, params object[] args)
        {
            if (!_cfg.EnableLog || _cfg.LogLevel > LogLevel.Info)
                return;
            Debug.Log(LogColoring(GenerateLog(ConcatMessage(obj, args), color), color));
        }

        #endregion

        #region LogWarning

        [Conditional("MST_USE_LOG")]
        internal static void LogWarning(object obj)
        {
            if (!_cfg.EnableLog || _cfg.LogLevel > LogLevel.Warning)
                return;
            Debug.LogWarning(GenerateLog(obj?.ToString()));
        }

        [Conditional("MST_USE_LOG")]
        internal static void LogWarning(object obj, LogColor color)
        {
            if (!_cfg.EnableLog || _cfg.LogLevel > LogLevel.Warning)
                return;
            Debug.LogWarning(LogColoring(GenerateLog(obj?.ToString(), color), color));
        }

        [Conditional("MST_USE_LOG")]
        internal static void LogWarning(object obj, params object[] args)
        {
            if (!_cfg.EnableLog || _cfg.LogLevel > LogLevel.Warning)
                return;
            Debug.LogWarning(GenerateLog(ConcatMessage(obj, args)));
        }

        [Conditional("MST_USE_LOG")]
        internal static void LogWarning(object obj, LogColor color, params object[] args)
        {
            if (!_cfg.EnableLog || _cfg.LogLevel > LogLevel.Warning)
                return;
            Debug.LogWarning(LogColoring(GenerateLog(ConcatMessage(obj, args), color), color));
        }

        #endregion

        #region LogError

        [Conditional("MST_USE_LOG")]
        internal static void LogError(object obj)
        {
            if (!_cfg.EnableLog || _cfg.LogLevel > LogLevel.Error)
                return;
            Debug.LogError(GenerateLog(obj?.ToString()));
        }

        [Conditional("MST_USE_LOG")]
        internal static void LogError(object obj, LogColor color)
        {
            if (!_cfg.EnableLog || _cfg.LogLevel > LogLevel.Error)
                return;
            Debug.LogError(LogColoring(GenerateLog(obj?.ToString(), color), color));
        }

        [Conditional("MST_USE_LOG")]
        internal static void LogError(object obj, params object[] args)
        {
            if (!_cfg.EnableLog || _cfg.LogLevel > LogLevel.Error)
                return;
            Debug.LogError(GenerateLog(ConcatMessage(obj, args)));
        }

        [Conditional("MST_USE_LOG")]
        internal static void LogError(object obj, LogColor color, params object[] args)
        {
            if (!_cfg.EnableLog || _cfg.LogLevel > LogLevel.Error)
                return;
            Debug.LogError(LogColoring(GenerateLog(ConcatMessage(obj, args), color), color));
        }

        #endregion

        private static string GenerateLog(string message, LogColor color = LogColor.Default)
        {
            var sb = new StringBuilder(_cfg.Prefix, 100);
            if (_cfg.ShowTimeStamp)
                sb.AppendFormat(" [{0:HH:mm:ss-fff}]", DateTime.Now);
            if (_cfg.ShowThreadId)
                sb.AppendFormat(" [Thread:{0}]", Thread.CurrentThread.ManagedThreadId);
            if (_cfg.ShowColorName)
                sb.AppendFormat(" [{0}]", color);
            sb.AppendFormat(" {0}", message);
            return sb.ToString();
        }

        private static string LogColoring(string message, LogColor color)
        {
            return color switch
            {
                LogColor.Red => $"<color=#FF0000>{message}</color>",
                LogColor.Green => $"<color=#00FF00>{message}</color>",
                LogColor.Blue => $"<color=#0000FF>{message}</color>",
                LogColor.Yellow => $"<color=#FFFF00>{message}</color>",
                LogColor.Magenta => $"<color=#FF00FF>{message}</color>",
                LogColor.Cyan => $"<color=#00FFFF>{message}</color>",
                LogColor.White => $"<color=#FFFFFF>{message}</color>",
                LogColor.Black => $"<color=#000000>{message}</color>",
                LogColor.Gray => $"<color=#808080>{message}</color>",
                LogColor.DarkRed => $"<color=#800000>{message}</color>",
                LogColor.DarkGreen => $"<color=#008000>{message}</color>",
                LogColor.DarkBlue => $"<color=#000080>{message}</color>",
                LogColor.DarkYellow => $"<color=#808000>{message}</color>",
                LogColor.DarkMagenta => $"<color=#800080>{message}</color>",
                LogColor.DarkCyan => $"<color=#008080>{message}</color>",
                _ => message
            };
        }

        private static string ConcatMessage(object obj, object[] args)
        {
            var sb = new StringBuilder(obj?.ToString());
            if (args != null)
            {
                foreach (var arg in args)
                    sb.Append(arg);
            }

            return sb.ToString();
        }
    }
}