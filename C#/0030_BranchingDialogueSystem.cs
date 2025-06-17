// 扩展版 JSON 对话结构（dialogue.json）：
[
  {
    "id": 1,
    "npc": "Elder",
    "text": "Do you want to join the resistance?",
    "options": [
      { "text": "Yes, I will fight!", "nextId": 2, "flag": "join_resistance" },
      { "text": "No, I must stay neutral.", "nextId": 3, "flag": "stay_neutral" }
    ]
  },
  {
    "id": 2,
    "npc": "Elder",
    "text": "Welcome, soldier! Your mission begins now.",
    "options": []
  },
  {
    "id": 3,
    "npc": "Elder",
    "text": "Then may fate guide your path.",
    "options": []
  }
]

// 分支记录脚本：BranchingDialogueSystem.cs
// BranchingDialogueSystem.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class BranchingDialogueSystem : MonoBehaviour
{
    [System.Serializable]
    public class DialogueOption
    {
        public string text;
        public int nextId;
        public string flag; // 记录分支标记
    }

    [System.Serializable]
    public class DialogueEntry
    {
        public int id;
        public string npc;
        public string text;
        public List<DialogueOption> options;
    }

    [System.Serializable]
    public class DialogueState
    {
        public List<string> selectedFlags = new List<string>();
    }

    public Text npcNameText;
    public Text dialogueText;
    public GameObject optionButtonPrefab;
    public Transform optionsPanel;

    private List<DialogueEntry> dialogues;
    private DialogueState state;
    private string dialoguePath;
    private string statePath;

    void Start()
    {
        dialoguePath = Path.Combine(Application.streamingAssetsPath, "dialogue.json");
        statePath = Path.Combine(Application.persistentDataPath, "dialogue_state.json");

        // 加载对话
        string json = File.ReadAllText(dialoguePath);
        dialogues = JsonConvert.DeserializeObject<List<DialogueEntry>>(json);

        // 加载状态
        if (File.Exists(statePath))
        {
            string stateJson = File.ReadAllText(statePath);
            state = JsonConvert.DeserializeObject<DialogueState>(stateJson);
        }
        else
        {
            state = new DialogueState();
        }

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
                if (!string.IsNullOrEmpty(flag) && !state.selectedFlags.Contains(flag))
                    state.selectedFlags.Add(flag);

                SaveState();
                ShowDialogue(next);
            });
        }
    }

    void SaveState()
    {
        string json = JsonConvert.SerializeObject(state, Formatting.Indented);
        File.WriteAllText(statePath, json);
    }
}
