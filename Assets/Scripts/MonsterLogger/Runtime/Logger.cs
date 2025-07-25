using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonsterLogger.Runtime
{
    internal class Logger
    {
        public static LogConfig cfg;

        internal static void Initialize(LogConfig config = null)
        {
            cfg = config ?? new LogConfig();
        }

        #region Log
        internal static void Log(object obj)
        {
            if (!cfg.EnableLog) return;
            UnityEngine.Debug.Log(GenerateLog(obj?.ToString()));
        }

        internal static void Log(object obj, LogColor color)
        {
            if (!cfg.EnableLog) return;
            var content = GenerateLog(obj?.ToString(), color);
            UnityEngine.Debug.Log(LogColoring(content, color));
        }

        internal static void Log(object obj, params object[] args)
        {
            if (!cfg.EnableLog) return;
            var message = GenerateLog(ConcatMessage(obj, args));
            UnityEngine.Debug.Log(message);
        }

        internal static void Log(object obj, LogColor color, params object[] args)
        {
            if (!cfg.EnableLog) return;
            var message = GenerateLog(ConcatMessage(obj, args), color);
            UnityEngine.Debug.Log(LogColoring(message, color));
        }
        #endregion

        #region LogWarning
        internal static void LogWarning(object obj)
        {
            if (!cfg.EnableLog) return;
            UnityEngine.Debug.LogWarning(GenerateLog(obj?.ToString()));
        }

        internal static void LogWarning(object obj, LogColor color)
        {
            if (!cfg.EnableLog) return;
            var content = GenerateLog(obj?.ToString(), color);
            UnityEngine.Debug.LogWarning(LogColoring(content, color));
        }

        internal static void LogWarning(object obj, params object[] args)
        {
            if (!cfg.EnableLog) return;
            var message = GenerateLog(ConcatMessage(obj, args));
            UnityEngine.Debug.LogWarning(message);
        }

        internal static void LogWarning(object obj, LogColor color, params object[] args)
        {
            if (!cfg.EnableLog) return;
            var message = GenerateLog(ConcatMessage(obj, args), color);
            UnityEngine.Debug.LogWarning(LogColoring(message, color));
        }
        #endregion

        #region LogError
        internal static void LogError(object obj)
        {
            if (!cfg.EnableLog) return;
            UnityEngine.Debug.LogError(GenerateLog(obj?.ToString()));
        }

        internal static void LogError(object obj, LogColor color)
        {
            if (!cfg.EnableLog) return;
            var content = GenerateLog(obj?.ToString(), color);
            UnityEngine.Debug.LogError(LogColoring(content, color));
        }

        internal static void LogError(object obj, params object[] args)
        {
            if (!cfg.EnableLog) return;
            var message = GenerateLog(ConcatMessage(obj, args));
            UnityEngine.Debug.LogError(message);
        }

        internal static void LogError(object obj, LogColor color, params object[] args)
        {
            if (!cfg.EnableLog) return;
            var message = GenerateLog(ConcatMessage(obj, args), color);
            UnityEngine.Debug.LogError(LogColoring(message, color));
        }
        #endregion

        private static string GenerateLog(string message, LogColor color = LogColor.Default)
        {
            var sb = new StringBuilder(cfg.Prefix, 100);
            if (cfg.ShowTimeStamp)
                sb.AppendFormat(" [{0}]", DateTime.Now.ToString("HH:mm:ss-fff"));
            if (cfg.ShowThreadId)
                sb.AppendFormat(" [Thread:{0}]", Thread.CurrentThread.ManagedThreadId);
            if (cfg.ShowColorName)
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
                {
                    sb.Append(arg);
                }
            }
            return sb.ToString();
        }
    }
}
