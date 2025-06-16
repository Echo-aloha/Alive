// 示例 JSON 文件（放在 StreamingAssets/player_list.json）
[
  { "name": "Knight", "level": 5, "health": 80.0 },
  { "name": "Mage",   "level": 3, "health": 60.5 },
  { "name": "Archer", "level": 4, "health": 70.0 }
]

// UnityJsonListExample.cs Unity 脚本：读取 JSON 数组并生成对象列表
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class UnityJsonListExample : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public int level;
        public float health;
    }

    void Start()
    {
        // 路径：StreamingAssets 中的文件在打包后也可读取
        string path = Path.Combine(Application.streamingAssetsPath, "player_list.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            List<PlayerData> players = JsonConvert.DeserializeObject<List<PlayerData>>(json);

            foreach (var p in players)
            {
                Debug.Log($"Name: {p.name}, Level: {p.level}, Health: {p.health}");
            }
        }
        else
        {
            Debug.LogWarning("player_list.json not found!");
        }
    }
}
