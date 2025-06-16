// JsonSaveLoad.cs 此脚本实现一个简易的存档系统，记录玩家的位置、生命值、金币，并在下次运行时自动加载。
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class JsonSaveLoad : MonoBehaviour
{
    [System.Serializable]
    public class SaveData
    {
        public float posX;
        public float posY;
        public float posZ;
        public int coins;
        public float health;
    }

    private string savePath;

    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.json");

        // 如果存在存档，加载
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonConvert.DeserializeObject<SaveData>(json);
            transform.position = new Vector3(data.posX, data.posY, data.posZ);
            Debug.Log($"[LOAD] Pos=({data.posX},{data.posY},{data.posZ}), Coins={data.coins}, HP={data.health}");
        }
        else
        {
            Debug.Log("[INFO] No save file found.");
        }
    }

    void Update()
    {
        // 按 S 保存
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData data = new SaveData
            {
                posX = transform.position.x,
                posY = transform.position.y,
                posZ = transform.position.z,
                coins = 100,
                health = 75.5f
            };

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(savePath, json);
            Debug.Log("[SAVE] Game state saved.");
        }
    }
}
