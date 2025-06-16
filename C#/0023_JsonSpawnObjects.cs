// 示例 JSON 文件（放在 StreamingAssets/spawn_config.json）
[
  { "name": "Knight", "x": 0, "y": 0, "z": 0 },
  { "name": "Mage",   "x": 3, "y": 0, "z": 1 },
  { "name": "Archer", "x": -2, "y": 0, "z": 4 }
]

// JsonSpawnObjects.cs Unity 脚本：读取 JSON 并生成预制体对象
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class JsonSpawnObjects : MonoBehaviour
{
    [System.Serializable]
    public class SpawnData
    {
        public string name;
        public float x;
        public float y;
        public float z;
    }

    public GameObject knightPrefab;
    public GameObject magePrefab;
    public GameObject archerPrefab;

    private Dictionary<string, GameObject> prefabMap;

    void Start()
    {
        // 初始化映射表
        prefabMap = new Dictionary<string, GameObject>
        {
            { "Knight", knightPrefab },
            { "Mage", magePrefab },
            { "Archer", archerPrefab }
        };

        // 路径
        string path = Path.Combine(Application.streamingAssetsPath, "spawn_config.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            List<SpawnData> spawnList = JsonConvert.DeserializeObject<List<SpawnData>>(json);

            foreach (var entry in spawnList)
            {
                if (prefabMap.ContainsKey(entry.name))
                {
                    Vector3 pos = new Vector3(entry.x, entry.y, entry.z);
                    Instantiate(prefabMap[entry.name], pos, Quaternion.identity);
                }
                else
                {
                    Debug.LogWarning($"Prefab not found for name: {entry.name}");
                }
            }
        }
        else
        {
            Debug.LogError("spawn_config.json not found!");
        }
    }
}
