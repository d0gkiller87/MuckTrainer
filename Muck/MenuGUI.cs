using System.Linq;
using UnityEngine;

namespace bruh {
    class MenuGUI : MonoBehaviour {
        private const float windowWidth = 250f;
        private const float controlWidth = (float) (windowWidth * 0.75);
        private Rect windowRect = new Rect(
            Screen.width - windowWidth - 30, 10,
            windowWidth, 300f
        );
        private int selectedControlIndex = 0;

        public void Update() {
            if (Config.showMenu) {
                if (Input.GetKeyDown(KeyCode.Keypad8) && selectedControlIndex > 0) {
                    selectedControlIndex--;
                }
                if (Input.GetKeyDown(KeyCode.Keypad2) && selectedControlIndex < Config.config.Count - 1) {
                    selectedControlIndex++;
                }
                if (Input.GetKeyDown(KeyCode.Keypad5)) { 
                     Config.config.ElementAt(selectedControlIndex).Value.isEnabled ^= true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Keypad0)) { 
                Config.showMenu ^= true;
            }
            if (Input.GetKeyDown(KeyCode.F5)) {
                Config.patchEnabled ^= true;
                if (Config.patchEnabled) {
                    Patcher.harmony.PatchAll();
                } else {
                    Patcher.harmony.UnpatchAll();
                }
            }
        }

        public void OnGUI() {
            if (Config.showMenu) {
                windowRect = GUILayout.Window(
                    0,
                    windowRect,
                    WindowFunction,
                    "Muck Trainer 0.1 @d0gkiller87"
                );
                GUI.skin.label.fontSize = 18;
            }
        }

        void DrawOption(Toggle feature, bool isSelected, float width) { 
            GUILayout.BeginHorizontal();
            if (isSelected) {
                GUI.color = Color.blue;
            }
            GUILayout.Label(" " + feature.text, GUILayout.Width(width));
            if (feature.isEnabled) {
                GUI.color = Color.green;
                GUILayout.Label("啟用");
            } else {
                GUI.color = Color.red;
                GUILayout.Label("停用");
            }
            GUI.color = Color.white;
            GUILayout.EndHorizontal();
        }

        void WindowFunction(int windowID) {
            for (var i = 0; i < Config.config.Count; ++i) {
                DrawOption(
                    Config.config.ElementAt(i).Value,
                    i == selectedControlIndex,
                    controlWidth
                );
            }
        }
    }
}
