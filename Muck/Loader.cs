using UnityEngine;

namespace bruh {
    public class Loader {
        public static void Init() {
            _gameObject = new GameObject();
            _gameObject.AddComponent<Main>();
            _gameObject.AddComponent<MenuGUI>();
            GameObject.DontDestroyOnLoad(_gameObject);
        }
        public static void Unload() {
            _Unload();
        }
        private static void _Unload() {
            GameObject.Destroy(_gameObject);
        }
        private static GameObject _gameObject;
    }
}