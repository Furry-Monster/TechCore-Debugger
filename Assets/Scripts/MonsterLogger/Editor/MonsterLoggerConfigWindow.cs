using MonsterLogger.Runtime;
using UnityEditor;
using UnityEngine;

namespace MonsterLogger.Editor
{
    public class MonsterLoggerConfigWindow : EditorWindow
    {
        private bool _isLogEnabled;
        private LogLevel _logLevel = LogLevel.Info;
        private string _logPath = "Logs";
        private bool _writeToFile;

        private Vector2 _scrollPosition;
        private const string PrefsPrefix = "MonsterLogger_";

        [MenuItem("Tools/Monster Logger/Settings")]
        private static void ShowWindow()
        {
            var window = GetWindow<MonsterLoggerConfigWindow>();
            window.titleContent = new GUIContent("Logger Settings");
            window.minSize = new Vector2(400, 300);
            window.LoadPrefs();
        }

        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Monster Logger Configuration", EditorStyles.boldLabel);
            EditorGUILayout.Space(10);

            // 基本设置
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Basic Settings", EditorStyles.boldLabel);
            _isLogEnabled = EditorGUILayout.Toggle("Enable Logging", _isLogEnabled);
            _logLevel = (LogLevel)EditorGUILayout.EnumPopup("Log Level", _logLevel);
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(10);

            // 输出设置
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Output Settings", EditorStyles.boldLabel);
            _writeToFile = EditorGUILayout.Toggle("Write To File", _writeToFile);

            using (new EditorGUI.DisabledScope(!_writeToFile))
            {
                _logPath = EditorGUILayout.TextField("Log Path", _logPath);
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(20);

            // 按钮区域
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Save Settings", GUILayout.Height(30)))
            {
                SavePrefs();
                ShowNotification(new GUIContent("Settings saved!"));
            }

            if (GUILayout.Button("Reset", GUILayout.Height(30)))
            {
                ResetPrefs();
                ShowNotification(new GUIContent("Settings reset to default!"));
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();
        }

        private void LoadPrefs()
        {
            _isLogEnabled = EditorPrefs.GetBool(PrefsPrefix + "IsEnabled", true);
            _logLevel = (LogLevel)EditorPrefs.GetInt(PrefsPrefix + "LogLevel", (int)LogLevel.Info);
            _logPath = EditorPrefs.GetString(PrefsPrefix + "LogPath", "Logs");
            _writeToFile = EditorPrefs.GetBool(PrefsPrefix + "WriteToFile", false);
        }

        private void SavePrefs()
        {
            EditorPrefs.SetBool(PrefsPrefix + "IsEnabled", _isLogEnabled);
            EditorPrefs.SetInt(PrefsPrefix + "LogLevel", (int)_logLevel);
            EditorPrefs.SetString(PrefsPrefix + "LogPath", _logPath);
            EditorPrefs.SetBool(PrefsPrefix + "WriteToFile", _writeToFile);
        }

        private void ResetPrefs()
        {
            _isLogEnabled = true;
            _logLevel = LogLevel.Info;
            _logPath = "Logs";
            _writeToFile = false;
            SavePrefs();
        }
    }
}