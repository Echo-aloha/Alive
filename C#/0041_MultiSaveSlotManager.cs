// 实现多存档位管理系统
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

public class MultiSaveSlotManager : MonoBehaviour
{
    public int maxSlots = 3;
    public QuestDatabase questDatabase;

    [System.Serializable]
    public class SaveSlotInfo
    {
        public string timestamp;
        public List<string> dialogueFlags = new();
        public List<SaveLoadSystem.QuestState> questStates = new();
    }

    private string GetSlotPath(int slot) =>
        Path.Combine(Application.persistentDataPath, $"save_slot_{slot}.json");

    // 保存到指定槽
    public void SaveToSlot(int slot, List<string> dialogueFlags)
    {
        SaveSlotInfo data = new()
        {
            timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            dialogueFlags = dialogueFlags,
            questStates = new()
        };

        foreach (var quest in questDatabase.quests)
        {
            data.questStates.Add(new SaveLoadSystem.QuestState
            {
                id = quest.id,
                isCompleted = quest.isCompleted
            });
        }

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(GetSlotPath(slot), json);
        Debug.Log($"[SAVE] Slot {slot} saved.");
    }

    // 读取指定槽
    public SaveSlotInfo LoadFromSlot(int slot)
    {
        string path = GetSlotPath(slot);
        if (!File.Exists(path))
        {
            Debug.LogWarning($"[LOAD] Slot {slot} is empty.");
            return null;
        }

        string json = File.ReadAllText(path);
        SaveSlotInfo data = JsonConvert.DeserializeObject<SaveSlotInfo>(json);

        foreach (var qs in data.questStates)
        {
            var quest = questDatabase.quests.Find(q => q.id == qs.id);
            if (quest != null)
                quest.isCompleted = qs.isCompleted;
        }

        Debug.Log($"[LOAD] Slot {slot} loaded.");
        return data;
    }

    // 删除指定槽
    public void DeleteSlot(int slot)
    {
        string path = GetSlotPath(slot);
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"[DELETE] Slot {slot} deleted.");
        }
    }

    // 获取所有槽位的状态
    public List<SlotPreview> GetAllSlotPreviews()
    {
        List<SlotPreview> previews = new();
        for (int i = 0; i < maxSlots; i++)
        {
            string path = GetSlotPath(i);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                SaveSlotInfo data = JsonConvert.DeserializeObject<SaveSlotInfo>(json);
                previews.Add(new SlotPreview { slot = i, timestamp = data.timestamp, hasData = true });
            }
            else
            {
                previews.Add(new SlotPreview { slot = i, timestamp = "Empty", hasData = false });
            }
        }
        return previews;
    }

    [System.Serializable]
    public class SlotPreview
    {
        public int slot;
        public string timestamp;
        public bool hasData;
    }
}
