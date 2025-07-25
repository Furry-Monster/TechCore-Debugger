# TechCore-Debugger

## Overview

TechCore-Debugger is a lightweight and flexible debugger wrapper designed for Unity projects. It simplifies the process of logging, monitoring, and debugging within Unity, providing developers with an easy-to-use interface to track runtime information, errors, and custom events in their applications.

## Features

- **Customizable Logging**: Easily log messages, warnings, and errors with configurable verbosity levels.
- **Unity Integration**: Seamlessly integrates with Unity's ecosystem, supporting both Editor and runtime environments.
- **Real-time Monitoring**: Monitor key variables and system states during gameplay or testing.
- **Extensible**: Add custom debug commands and extend functionality to suit your project's needs.
- **Lightweight**: Minimal performance overhead, optimized for use in both development and production builds.

## Installation

1. **Clone or Download**:
   Clone the repository or download the source code from [GitHub](https://github.com/Furry-Monster/TechCore-Debugger).
   ```bash
   git clone https://github.com/Furry-Monster/TechCore-Debugger.git
   ```

2. **Add to Unity**:
   - Copy the `TechCore-Debugger` folder into your Unity project's `Assets` directory.
   - Alternatively, add it as a package via Unity's Package Manager using the Git URL.

3. **Setup**:
   - Attach the `TechCoreDebugger` component to a GameObject in your scene.
   - Configure settings in the Unity Inspector or via script.

## Usage

### Basic Example
```csharp
using MonsterLogger.Runtime;
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
```

### Advanced Configuration
You can customize the debugger's behavior in Unity Editor.
Just Open Config Panel clicking Tools/MonsterLogger/Config!

## Contributing

Contributions are welcome! To contribute:
1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Submit a pull request with a clear description of your changes.

Please ensure your code follows the project's coding standards and includes relevant tests.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For questions, suggestions, or issues, please open an issue on the [GitHub repository](https://github.com/Furry-Monster/TechCore-Debugger) or contact the maintainer at [4urrym0nster@gmail.com].

---

*Happy debugging with TechCore-Debugger!*
