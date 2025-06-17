// 示例对话 JSON 文件（放在 StreamingAssets/dialogue.json）
[
  {
    "id": 1,
    "npc": "Elder",
    "text": "Welcome, traveler. What brings you here?",
    "options": [
      { "text": "I'm looking for help.", "nextId": 2 },
      { "text": "Just passing by.", "nextId": 3 }
    ]
  },
  {
    "id": 2,
    "npc": "Elder",
    "text": "Then you must find the ancient scroll.",
    "options": []
  },
  {
    "id": 3,
    "npc": "Elder",
    "text": "Safe travels, stranger.",
    "options": []
  }
]

// Unity 脚本：读取对话 JSON 并显示 + 响应选项点击
// DialogueSystem.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class DialogueSystem : MonoBehaviour
{
    [System.Serializable]
    public class DialogueOption
    {
        public string text;
        public int nextId;
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

    private List<DialogueEntry> dialogues = new List<DialogueEntry>();

    void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "dialogue.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            dialogues = JsonConvert.DeserializeObject<List<DialogueEntry>>(json);
            ShowDialogue(1); // 从第1条开始
        }
    }

    void ShowDialogue(int id)
    {
        DialogueEntry entry = dialogues.Find(d => d.id == id);
        if (entry == null)
        {
            Debug.LogWarning("Dialogue not found: " + id);
            return;
        }

        npcNameText.text = entry.npc;
        dialogueText.text = entry.text;

        foreach (Transform child in optionsPanel)
            Destroy(child.gameObject);

        foreach (var opt in entry.options)
        {
            GameObject btnObj = Instantiate(optionButtonPrefab, optionsPanel);
            btnObj.GetComponentInChildren<Text>().text = opt.text;

            Button btn = btnObj.GetComponent<Button>();
            int nextId = opt.nextId;
            btn.onClick.AddListener(() => ShowDialogue(nextId));
        }
    }
}
