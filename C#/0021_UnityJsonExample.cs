// UnityJsonExample.cs Unity 推荐使用 Newtonsoft.Json，可通过 Unity Package Manager 安装或导入 DLL。
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class UnityJsonExample : MonoBehaviour
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
        // 创建数据
        PlayerData player = new PlayerData
        {
            name = "Knight",
            level = 5,
            health = 87.5f
        };

        // 路径（Unity 的 persistentDataPath 更适用于移动端和存档）
        string path = Application.persistentDataPath + "/player.json";

        // 序列化为 JSON 并保存
        string json = JsonConvert.SerializeObject(player, Formatting.Indented);
        File.WriteAllText(path, json);
        Debug.Log("Saved to: " + path);

        // 读取 JSON 并反序列化
        string readJson = File.ReadAllText(path);
        PlayerData loadedPlayer = JsonConvert.DeserializeObject<PlayerData>(readJson);

        Debug.Log($"Loaded Player: {loadedPlayer.name}, Level: {loadedPlayer.level}, Health: {loadedPlayer.health}");
    }
}
