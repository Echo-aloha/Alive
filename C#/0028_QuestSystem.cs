// 示例 JSON 文件（保存任务列表与状态）：
[
  { "id": 1, "title": "Collect 5 Mushrooms", "isCompleted": false },
  { "id": 2, "title": "Talk to the Elder", "isCompleted": true },
  { "id": 3, "title": "Defeat the Goblin King", "isCompleted": false }
]

// 任务系统控制器
// QuestSystem.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class QuestSystem : MonoBehaviour
{
    [System.Serializable]
    public class Quest
    {
        public int id;
        public string title;
        public bool isCompleted;
    }

    public GameObject questItemPrefab;
    public Transform questPanel;

    private List<Quest> quests = new List<Quest>();
    private string savePath;

    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "quests.json");

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            quests = JsonConvert.DeserializeObject<List<Quest>>(json);
        }
        else
        {
            // 如果没存档，创建默认任务
            quests = new List<Quest>
            {
                new Quest { id = 1, title = "Collect 5 Mushrooms", isCompleted = false },
                new Quest { id = 2, title = "Talk to the Elder", isCompleted = false },
                new Quest { id = 3, title = "Defeat the Goblin King", isCompleted = false }
            };
            SaveQuests();
        }

        DisplayQuests();
    }

    void DisplayQuests()
    {
        foreach (Transform child in questPanel)
            Destroy(child.gameObject);

        foreach (var quest in quests)
        {
            GameObject item = Instantiate(questItemPrefab, questPanel);
            Text text = item.GetComponentInChildren<Text>();
            Button btn = item.GetComponentInChildren<Button>();

            string status = quest.isCompleted ? "[✔]" : "[ ]";
            text.text = $"{status} {quest.title}";

            // 按下切换状态
            btn.onClick.AddListener(() => ToggleQuest(quest.id));
        }
    }

    void ToggleQuest(int questId)
    {
        Quest q = quests.Find(q => q.id == questId);
        if (q != null)
        {
            q.isCompleted = !q.isCompleted;
            Debug.Log($"Toggled quest: {q.title} → {(q.isCompleted ? "Completed" : "Incomplete")}");
            SaveQuests();
            DisplayQuests();
        }
    }

    void SaveQuests()
    {
        string json = JsonConvert.SerializeObject(quests, Formatting.Indented);
        File.WriteAllText(savePath, json);
    }
}
