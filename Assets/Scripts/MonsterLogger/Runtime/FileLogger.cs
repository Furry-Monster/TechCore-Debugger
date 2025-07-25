using MonsterLogger.Runtime;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using UnityEngine;

public class LogData
{
    public string log { get; set; }
    public string trace { get; set; }
    public LogType type { get; set; }
}

public class FileLogger : MonoBehaviour
{
    private StreamWriter _streamWriter;

    private readonly ConcurrentQueue<LogData> _concurrentQueue = new ConcurrentQueue<LogData>();

    private readonly ManualResetEvent _manualResetEvent = new ManualResetEvent(false);

    private bool _listening = false;

    private LogLevel _logLevel = LogLevel.Info;

    private string _nowTime => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    internal void Initialize(string logDirName, string logFileName, LogLevel level)
    {
        string logFilePath = Path.Combine(logDirName, logFileName);
        _streamWriter = new StreamWriter(logFilePath);
        // 监听Unity日志事件
        Application.logMessageReceivedThreaded += OnLogMessageReceived;
        // 激活监听线程
        _listening = true;
        Thread fileThread = new Thread(LogToFile);
        fileThread.Start();
    }

    /// <summary>
    /// 日志监听线程
    /// </summary>
    private void LogToFile()
    {
        while (_listening)
        {
            // 等待信号量，直到有日志数据可处理
            _manualResetEvent.WaitOne();
            if (_streamWriter == null)
                throw new Exception("StreamWriter is null. Ensure that FileLogger is initialized properly before logging.");
            while (_concurrentQueue.Count > 0 && _concurrentQueue.TryDequeue(out var data))
            {
                if (data.type == LogType.Log)
                {
                    if (_logLevel > LogLevel.Info)
                        continue;

                    _streamWriter.Write("Log >>> ");
                    _streamWriter.WriteLine(data.log);
                    _streamWriter.WriteLine(data.trace);

                }
                else if (data.type == LogType.Warning)
                {
                    if (_logLevel > LogLevel.Warning)
                        continue;

                    _streamWriter.Write("Warning >>> ");
                    _streamWriter.WriteLine(data.log);
                    _streamWriter.WriteLine(data.trace);
                }
                else if (data.type == LogType.Error)
                {
                    if (_logLevel > LogLevel.Error)
                        continue;

                    _streamWriter.Write("Error >>> ");
                    _streamWriter.WriteLine(data.log);
                    _streamWriter.WriteLine(data.trace);
                }
                _streamWriter.Write("\r\n");
            }
            _streamWriter.Flush();
            // 重置信号量，准备下一次等待
            _manualResetEvent.Reset();
            Thread.Sleep(1);
        }
    }

    private void OnApplicationQuit()
    {

        // 取消监听Unity日志事件
        Application.logMessageReceivedThreaded -= OnLogMessageReceived;
        // 回收监听线程
        _listening = false;
        _manualResetEvent.Set();
        if (_streamWriter != null)
        {
            _streamWriter.Flush();
            _streamWriter.Close();
            _streamWriter.Dispose();
            _streamWriter = null;
        }
    }

    private void OnLogMessageReceived(string condition, string stacktrace, LogType type)
    {
        _concurrentQueue.Enqueue(new LogData
        {
            log = _nowTime + " " + condition,
            trace = stacktrace,
            type = type
        });
        // 有日志，设置信号量
        _manualResetEvent.Set();
    }
}
