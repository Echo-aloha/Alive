using UnityEngine;
using UnityEditor;
using System.Linq;

public class DialogueEditorWindow : EditorWindow
{
    private DialogueDatabase db;
    private Vector2 scrollPosition;

    [MenuItem("Tools/Dialogue Editor")]
    public static void ShowWindow()
    {
        GetWindow<DialogueEditorWindow>("Dialogue Editor");
    }

    private void OnEnable()
    {
        db = Resources.Load<DialogueDatabase>("DialogueDatabase");
        if (db == null)
            Debug.LogError("请先在 Resources 下创建 DialogueDatabase.asset");
    }

    private void OnGUI()
    {
        if (db == null)
            return;

        // 顶部按钮
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Entry"))
        {
            int newId = db.entries.Any() ? db.entries.Max(e => e.id) + 1 : 1;
            db.entries.Add(new DialogueEntry {
                id = newId,
                npc = "",
                text = "",
                options = new System.Collections.Generic.List<DialogueOption>()
            });
            EditorUtility.SetDirty(db);
        }
        if (GUILayout.Button("Save All"))
        {
            EditorUtility.SetDirty(db);
            AssetDatabase.SaveAssets();
        }
        EditorGUILayout.EndHorizontal();

        // 列表滚动区域
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        for (int i = 0; i < db.entries.Count; i++)
        {
            var entry = db.entries[i];
            EditorGUILayout.BeginVertical("box");

            // ID 与删除
            EditorGUILayout.BeginHorizontal();
            entry.id = EditorGUILayout.IntField("ID", entry.id);
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                db.entries.RemoveAt(i);
                EditorUtility.SetDirty(db);
                break;
            }
            EditorGUILayout.EndHorizontal();

            // NPC 与文本
            entry.npc = EditorGUILayout.TextField("NPC", entry.npc);
            EditorGUILayout.LabelField("Text");
            entry.text = EditorGUILayout.TextArea(entry.text, GUILayout.Height(60));

            // 选项列表
            EditorGUILayout.LabelField("Options");
            int removeIndex = -1;
            for (int j = 0; j < entry.options.Count; j++)
            {
                var opt = entry.options[j];
                EditorGUILayout.BeginHorizontal();
                opt.text = EditorGUILayout.TextField(opt.text);
                opt.nextId = EditorGUILayout.IntField(opt.nextId, GUILayout.Width(50));
                opt.flag = EditorGUILayout.TextField(opt.flag, GUILayout.Width(100));
                if (GUILayout.Button("–", GUILayout.Width(20)))
                    removeIndex = j;
                EditorGUILayout.EndHorizontal();
            }
            if (removeIndex >= 0)
            {
                entry.options.RemoveAt(removeIndex);
                EditorUtility.SetDirty(db);
            }
            if (GUILayout.Button("Add Option"))
            {
                entry.options.Add(new DialogueOption { text = "", nextId = 0, flag = "" });
                EditorUtility.SetDirty(db);
            }

            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();
    }
}
