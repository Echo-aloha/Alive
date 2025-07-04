// DialogueDatabaseSO.cs 以下示例展示如何用 ScriptableObject 取代 JSON，实现对话和任务数据在编辑器中可视化管理，并在运行时加载。
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueDatabase", menuName = "Game/Dialogue Database")]
public class DialogueDatabase : ScriptableObject
{
    public List<DialogueEntry> entries;
}

[System.Serializable]
public class DialogueEntry
{
    public int id;
    public string npc;
    [TextArea(3, 10)]
    public string text;
    public List<DialogueOption> options;
}

[System.Serializable]
public class DialogueOption
{
    public string text;
    public int nextId;
    public string flag;
}
