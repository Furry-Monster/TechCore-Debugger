using MonsterLogger.Runtime;
using UnityEditor;
using UnityEngine;

namespace MonsterLogger.Editor
{

    public class MonsterLoggerConfigWindow : EditorWindow
    {
        private bool isLogEnabled;
        private LogLevel logLevel = LogLevel.Info;
        private string logPath = "Logs";
        private bool writeToFile;

        private Vector2 scrollPosition;
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
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Monster Logger Configuration", EditorStyles.boldLabel);
            EditorGUILayout.Space(10);

            // 基本设置
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Basic Settings", EditorStyles.boldLabel);
            isLogEnabled = EditorGUILayout.Toggle("Enable Logging", isLogEnabled);
            logLevel = (LogLevel)EditorGUILayout.EnumPopup("Log Level", logLevel);
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(10);

            // 输出设置
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Output Settings", EditorStyles.boldLabel);
            writeToFile = EditorGUILayout.Toggle("Write To File", writeToFile);

            using (new EditorGUI.DisabledScope(!writeToFile))
            {
                logPath = EditorGUILayout.TextField("Log Path", logPath);
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
            isLogEnabled = EditorPrefs.GetBool(PrefsPrefix + "IsEnabled", true);
            logLevel = (LogLevel)EditorPrefs.GetInt(PrefsPrefix + "LogLevel", (int)LogLevel.Info);
            logPath = EditorPrefs.GetString(PrefsPrefix + "LogPath", "Logs");
            writeToFile = EditorPrefs.GetBool(PrefsPrefix + "WriteToFile", false);
        }

        private void SavePrefs()
        {
            EditorPrefs.SetBool(PrefsPrefix + "IsEnabled", isLogEnabled);
            EditorPrefs.SetInt(PrefsPrefix + "LogLevel", (int)logLevel);
            EditorPrefs.SetString(PrefsPrefix + "LogPath", logPath);
            EditorPrefs.SetBool(PrefsPrefix + "WriteToFile", writeToFile);
        }

        private void ResetPrefs()
        {
            isLogEnabled = true;
            logLevel = LogLevel.Info;
            logPath = "Logs";
            writeToFile = false;
            SavePrefs();
        }
    }
}