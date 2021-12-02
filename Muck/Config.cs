using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace bruh {
    class Toggle { 
        public string text;
        public bool isEnabled;
    }
    static class Config {
        public static bool patchEnabled = true;
        public static bool showMenu = true;
        public const int DamageNormal = 30;
        public const int Damage = 20000;
        public const int DamageMutiplier = 200;

        public static Dictionary<string, Toggle> config = new Dictionary<string, Toggle> {
            {"GodMode", new Toggle { text = "無敵", isEnabled = false }},
            {"SpeedHack", new Toggle { text = "加速", isEnabled = false }},
            {"JumpHack", new Toggle { text = "飛天", isEnabled = false }},
            {"NoHunger", new Toggle { text = "無限飢餓度", isEnabled = false }},
            {"InfiniteStamina", new Toggle { text = "無限體力", isEnabled = false }},
            {"IgnoreToolLevel", new Toggle { text = "無視工具等級", isEnabled = false }},
            {"OneHitResource", new Toggle { text = "一擊採集資源", isEnabled = false }},
            {"OneHitPlayers", new Toggle { text = "一擊殺死玩家", isEnabled = false }},
            {"OneHitMobs", new Toggle { text = "一擊殺死生物", isEnabled = false }},
            {"ForceUnlockBoxes", new Toggle { text = "免費解鎖箱子", isEnabled = false }},
            {"ForceRepairBoatComponents", new Toggle { text = "強制修復小船部件", isEnabled = false }},
            {"BoatAlreadyRepaired", new Toggle { text = "小船修復完成", isEnabled = false }}
        };

        private static void loadConfig() {
            string configDir = @"";
            List<Item> config;
            using (StreamReader sr = new StreamReader(configDir + "config.json")) { 
                string json = sr.ReadToEnd();
                config = JsonConvert.DeserializeObject<List<Item>>(json);
            }
        }
    }
}
