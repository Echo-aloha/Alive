// 用于 统一保存与读取玩家进度，支持以下内容：保存-任务完成状态（来自 QuestDataBase.asset）、对话分支标记；加载-还原任务完成状态、还原对话分支记录
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class SaveLoadSystem : MonoBehaviour
{
    [System.Serializable]
    public class GameState
    {
        public List<string> dialogueFlags = new List<string>();
        public List<QuestState> questStates = new List<QuestState>();
    }

    [System.Serializable]
    public class QuestState
    {
        public int id;
        public bool isCompleted;
    }

    public QuestDatabase questDatabase;
    public string saveFileName = "savegame.json";

    private string SavePath => Path.Combine(Application.persistentDataPath, saveFileName);

    public void SaveGame(List<string> dialogueFlags)
    {
        GameState data = new GameState();
        data.dialogueFlags = dialogueFlags;

        foreach (var quest in questDatabase.quests)
        {
            data.questStates.Add(new QuestState { id = quest.id, isCompleted = quest.isCompleted });
        }

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(SavePath, json);
        Debug.Log($"[SAVE] Game saved to {SavePath}");
    }

    public GameState LoadGame()
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("No save file found.");
            return null;
        }

        string json = File.ReadAllText(SavePath);
        GameState data = JsonConvert.DeserializeObject<GameState>(json);

        // 还原任务状态
        foreach (var qs in data.questStates)
        {
            var quest = questDatabase.quests.Find(q => q.id == qs.id);
            if (quest != null)
                quest.isCompleted = qs.isCompleted;
        }

        Debug.Log("[LOAD] Game loaded.");
        return data;
    }
}
