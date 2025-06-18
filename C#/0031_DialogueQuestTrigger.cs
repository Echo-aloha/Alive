// 修改过的对话 JSON 示例（dialogue.json）
[
  {
    "id": 1,
    "npc": "Elder",
    "text": "Are you ready to prove yourself?",
    "options": [
      { "text": "Yes, give me a task.", "nextId": 2, "flag": "start_goblin_quest" },
      { "text": "Not now.", "nextId": 3 }
    ]
  },
  {
    "id": 2,
    "npc": "Elder",
    "text": "Then defeat the goblins in the forest.",
    "options": []
  },
  {
    "id": 3,
    "npc": "Elder",
    "text": "Come back when you're ready.",
    "options": []
  }
]

// 对话系统脚本扩展：调用任务系统
// DialogueQuestTrigger.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class DialogueQuestTrigger : MonoBehaviour
{
    [System.Serializable]
    public class DialogueOption
    {
        public string text;
        public int nextId;
        public string flag;
    }

    [System.Serializable]
    public class DialogueEntry
    {
        public int id;
        public string npc;
        public string text;
        public List<DialogueOption> options;
    }

    public Text npcNameText;
    public Text dialogueText;
    public GameObject optionButtonPrefab;
    public Transform optionsPanel;
    public QuestManager questManager; // 引用任务系统

    private List<DialogueEntry> dialogues;
    private HashSet<string> triggeredFlags = new HashSet<string>();
    private string dialoguePath;

    void Start()
    {
        dialoguePath = Path.Combine(Application.streamingAssetsPath, "dialogue.json");

        string json = File.ReadAllText(dialoguePath);
        dialogues = JsonConvert.DeserializeObject<List<DialogueEntry>>(json);

        ShowDialogue(1);
    }

    void ShowDialogue(int id)
    {
        DialogueEntry entry = dialogues.Find(d => d.id == id);
        if (entry == null) return;

        npcNameText.text = entry.npc;
        dialogueText.text = entry.text;

        foreach (Transform child in optionsPanel)
            Destroy(child.gameObject);

        foreach (var opt in entry.options)
        {
            GameObject btnObj = Instantiate(optionButtonPrefab, optionsPanel);
            btnObj.GetComponentInChildren<Text>().text = opt.text;

            Button btn = btnObj.GetComponent<Button>();
            string flag = opt.flag;
            int next = opt.nextId;

            btn.onClick.AddListener(() =>
            {
                if (!string.IsNullOrEmpty(flag) && !triggeredFlags.Contains(flag))
                {
                    triggeredFlags.Add(flag);
                    Debug.Log("[TRIGGER] Flag: " + flag);

                    // 联动任务系统
                    if (flag == "start_goblin_quest")
                    {
                        questManager.AddQuest("Goblin Hunt", "Defeat 5 goblins in the forest.");
                    }
                }

                ShowDialogue(next);
            });
        }
    }
}

// 简单的任务系统脚本：QuestManager.cs
// QuestManager.cs
using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public class Quest
    {
        public string title;
        public string description;
        public bool isCompleted;
    }

    private List<Quest> quests = new List<Quest>();

    public void AddQuest(string title, string description)
    {
        quests.Add(new Quest { title = title, description = description, isCompleted = false });
        Debug.Log($"[QUEST ADDED] {title}: {description}");
    }

    public void CompleteQuest(string title)
    {
        Quest q = quests.Find(q => q.title == title);
        if (q != null)
        {
            q.isCompleted = true;
            Debug.Log($"[QUEST COMPLETE] {q.title}");
        }
    }

    public void ListQuests()
    {
        foreach (var q in quests)
        {
            Debug.Log($"- {q.title} [{(q.isCompleted ? "Done" : "In Progress")}]");
        }
    }
}
