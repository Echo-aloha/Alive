// InventoryUI.cs 本例使用 UnityEngine.UI 系统，请确保你的场景中已有：一个 Canvas 画布；一个 Vertical Layout Group 控制的容器（如 InventoryPanel）；一个 UI 预制体（如 ItemEntryPrefab），里面有 Text 或 TMP_Text 显示内容。
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class InventoryUI : MonoBehaviour
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

    public GameObject itemEntryPrefab;         // UI 预制体：有 Text 组件
    public Transform inventoryContentPanel;    // UI 列表容器（带 VerticalLayoutGroup）

    private string savePath;

    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "inventory.json");

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            InventoryData inventory = JsonConvert.DeserializeObject<InventoryData>(json);

            foreach (var item in inventory.items)
            {
                GameObject entry = Instantiate(itemEntryPrefab, inventoryContentPanel);
                Text label = entry.GetComponentInChildren<Text>(); // 或 TMP_Text
                label.text = $"{item.itemName} x{item.quantity} ({item.rarity})";
            }
        }
        else
        {
            Debug.LogWarning("No inventory file found.");
        }
    }
}
