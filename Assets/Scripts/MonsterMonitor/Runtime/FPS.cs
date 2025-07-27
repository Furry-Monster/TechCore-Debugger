using UnityEngine;

namespace MonsterMonitor.Runtime
{
    public class FPS : MonoBehaviour
    {
        private float _deltaTime = 0.0f;

        private GUIStyle _mStyle;

        private void Awake()
        {
            _mStyle = new GUIStyle
            {
                alignment = TextAnchor.UpperLeft,
                normal =
                {
                    background = null,
                    textColor = Color.red
                },
                fontSize = 35
            };
        }

        private void Update()
        {
            _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
        }

        private void OnGUI()
        {
            var rect = new Rect(0, 0, 500, 300);
            var fps = 1.0f / _deltaTime;
            var text = $" FPS:{fps:N0} ";
            GUI.Label(rect, text, _mStyle);
        }
    }
}