// InventoryManager.cs 每个道具条目的按钮响应 我们将：为每个道具创建一个 UI 按钮（或包含按钮的 Prefab）；点击后调用使用/删除函数；动态刷新背包显示
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class InventoryManager : MonoBehaviour
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

    public GameObject itemEntryPrefab;
    public Transform contentPanel;

    private InventoryData inventory = new InventoryData();
    private string savePath;

    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "inventory.json");

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            inventory = JsonConvert.DeserializeObject<InventoryData>(json);
        }

        RefreshUI();
    }

    public void RefreshUI()
    {
        // 清空旧内容
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // 重新创建
        foreach (var item in inventory.items)
        {
            GameObject entry = Instantiate(itemEntryPrefab, contentPanel);
            entry.GetComponentInChildren<Text>().text = $"{item.itemName} x{item.quantity} ({item.rarity})";

            // 设置按钮事件
            Button btn = entry.GetComponentInChildren<Button>();
            btn.onClick.AddListener(() => UseItem(item.itemName));
        }
    }

    public void UseItem(string itemName)
    {
        var target = inventory.items.Find(i => i.itemName == itemName);
        if (target != null)
        {
            target.quantity--;
            Debug.Log($"[USE] Used 1 {target.itemName}, remaining: {target.quantity}");

            if (target.quantity <= 0)
                inventory.items.Remove(target);

            SaveInventory();
            RefreshUI();
        }
    }

    private void SaveInventory()
    {
        string json = JsonConvert.SerializeObject(inventory, Formatting.Indented);
        File.WriteAllText(savePath, json);
    }
}
