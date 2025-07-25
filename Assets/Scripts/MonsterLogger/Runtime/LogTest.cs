using MonsterLogger.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logger = MonsterLogger.Runtime.Logger;

public class LogTest : MonoBehaviour
{
    private void Start()
    {
        // Initialize the logger with default configuration
        Logger.Initialize(new LogConfig { EnableLog = true });

        // Example usage of the logger
        Logger.Log("Game started successfully!", LogColor.Gray);
        Logger.LogWarning("This is a warning message.", LogColor.Blue);
        Logger.LogError("This is an error message.", LogColor.Green);
    }
}
