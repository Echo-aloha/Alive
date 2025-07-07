// GameFlowManager.cs 包括对话触发、任务管理、战斗系统和流程管理器四大模块的整体代码，用于串联对话→任务→战斗→后续对话的完整游戏流程。请将此脚本放入你的 Unity 项目中，并在 Inspector 中关联相应组件。
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// 演示完整流程：对话触发任务 → 战斗进行 → 任务完成 → 后续对话
/// 文件名：GameFlowManager.cs
/// </summary>

// 1. 对话触发任务系统，带事件通知
public class DialogueQuestTrigger : MonoBehaviour
{
    [Serializable]
    public class DialogueOption { public string text; public int nextId; public string flag; }
    [Serializable]
    public class DialogueEntry { public int id; public string npc; public string text; public List<DialogueOption> options; }

    public Text npcNameText;
    public Text dialogueText;
    public GameObject optionButtonPrefab;
    public Transform optionsPanel;
    private List<DialogueEntry> dialogues;

    public event Action<string> OnFlagTriggered;

    void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "dialogue.json");
        dialogues = JsonConvert.DeserializeObject<List<DialogueEntry>>(File.ReadAllText(path));
        ShowDialogue(1);
    }

    void ShowDialogue(int id)
    {
        var entry = dialogues.Find(d => d.id == id);
        if (entry == null) return;
        npcNameText.text = entry.npc;
        dialogueText.text = entry.text;
        foreach (Transform t in optionsPanel) Destroy(t.gameObject);

        foreach (var opt in entry.options)
        {
            var btnObj = Instantiate(optionButtonPrefab, optionsPanel);
            btnObj.GetComponentInChildren<Text>().text = opt.text;
            btnObj.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                if (!string.IsNullOrEmpty(opt.flag))
                    OnFlagTriggered?.Invoke(opt.flag);
                ShowDialogue(opt.nextId);
            });
        }
    }
}

// 2. 简易任务管理器：添加/完成任务
public class QuestManager : MonoBehaviour
{
    public class Quest { public string title; public string description; public bool isCompleted; }
    private List<Quest> quests = new List<Quest>();

    public void AddQuest(string title, string description)
    {
        quests.Add(new Quest { title = title, description = description, isCompleted = false });
        Debug.Log($"[QUEST ADDED] {title}: {description}");
    }

    public void CompleteQuest(string title)
    {
        var q = quests.Find(qt => qt.title == title);
        if (q != null && !q.isCompleted)
        {
            q.isCompleted = true;
            Debug.Log($"[QUEST COMPLETE] {title}");
        }
    }
}

// 3. 战斗系统集成：触发标记后生成敌人并在完成后发送事件
public class BattleSystemIntegration : MonoBehaviour
{
    [Serializable]
    public class DialogueState { public List<string> selectedFlags; }

    public string triggerFlag = "start_goblin_quest";
    public string stateFileName = "dialogue_state.json";
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public event Action OnBattleComplete;
    private bool battleStarted = false;
    private List<GameObject> enemies = new List<GameObject>();

    void Update()
    {
        if (battleStarted) return;
        string path = Path.Combine(Application.persistentDataPath, stateFileName);
        if (!File.Exists(path)) return;

        var state = JsonConvert.DeserializeObject<DialogueState>(File.ReadAllText(path));
        if (state.selectedFlags?.Contains(triggerFlag) == true)
        {
            battleStarted = true;
            StartCoroutine(StartBattle());
        }
    }

    IEnumerator StartBattle()
    {
        foreach (var pt in spawnPoints)
        {
            var go = Instantiate(enemyPrefab, pt.position, Quaternion.identity);
            enemies.Add(go);
        }
        while (enemies.Exists(e => e != null)) yield return null;
        Debug.Log("[BATTLE] All enemies defeated!");
        OnBattleComplete?.Invoke();
    }
}

// 4. 游戏流程管理器：订阅事件并串联对话、任务与战斗
public class GameFlowManager : MonoBehaviour
{
    public DialogueQuestTrigger dialogueTrigger;
    public QuestManager questManager;
    public BattleSystemIntegration battleSystem;
    public DialogueQuestTrigger postBattleDialogue; // 用于战后对话

    void Awake()
    {
        dialogueTrigger.OnFlagTriggered += HandleFlag;
        battleSystem.OnBattleComplete += HandleBattleFinish;
    }

    void HandleFlag(string flag)
    {
        if (flag == "start_goblin_quest")
        {
            questManager.AddQuest("Goblin Hunt", "Defeat goblins in the forest");
        }
    }

    void HandleBattleFinish()
    {
        questManager.CompleteQuest("Goblin Hunt");
        // 启动战后对话，从第4条开始（示例）
        postBattleDialogue.ShowDialogue(4);
    }
}
