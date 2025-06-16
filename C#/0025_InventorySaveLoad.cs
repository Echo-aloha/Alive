// InventorySaveLoad.cs 示例：玩家背包中有多个道具，每个道具有名称、数量、稀有度
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class InventorySaveLoad : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public string itemName;
        public int quantity;
        public string rarity;
    }

    [System.Serializable]
    public class InventoryData
    {
        public List<Item> items = new List<Item>();
    }

    private string savePath;

    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "inventory.json");

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            InventoryData loaded = JsonConvert.DeserializeObject<InventoryData>(json);

            Debug.Log("[LOAD] Inventory contents:");
            foreach (var item in loaded.items)
            {
                Debug.Log($"- {item.itemName} x{item.quantity} [{item.rarity}]");
            }
        }
        else
        {
            Debug.Log("[INFO] No inventory file found.");
        }
    }

    void Update()
    {
        // 按下 I 保存当前背包内容
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryData inventory = new InventoryData();
            inventory.items.Add(new Item { itemName = "Health Potion", quantity = 3, rarity = "Common" });
            inventory.items.Add(new Item { itemName = "Iron Sword", quantity = 1, rarity = "Rare" });
            inventory.items.Add(new Item { itemName = "Magic Scroll", quantity = 2, rarity = "Epic" });

            string json = JsonConvert.SerializeObject(inventory, Formatting.Indented);
            File.WriteAllText(savePath, json);
            Debug.Log("[SAVE] Inventory saved.");
        }
    }
}
