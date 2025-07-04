using UnityEngine;
using UnityEditor;
using System.Linq;

public class QuestEditorWindow : EditorWindow
{
    private QuestDatabase db;
    private Vector2 scrollPosition;

    [MenuItem("Tools/Quest Editor")]
    public static void ShowWindow()
    {
        GetWindow<QuestEditorWindow>("Quest Editor");
    }

    private void OnEnable()
    {
        db = Resources.Load<QuestDatabase>("QuestDatabase");
        if (db == null)
            Debug.LogError("请先在 Resources 下创建 QuestDatabase.asset");
    }

    private void OnGUI()
    {
        if (db == null)
            return;

        // 顶部按钮
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Quest"))
        {
            int newId = db.quests.Any() ? db.quests.Max(q => q.id) + 1 : 1;
            db.quests.Add(new Quest {
                id = newId,
                title = "",
                description = "",
                isCompleted = false
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
        for (int i = 0; i < db.quests.Count; i++)
        {
            var quest = db.quests[i];
            EditorGUILayout.BeginVertical("box");

            // ID 与删除
            EditorGUILayout.BeginHorizontal();
            quest.id = EditorGUILayout.IntField("ID", quest.id);
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                db.quests.RemoveAt(i);
                EditorUtility.SetDirty(db);
                break;
            }
            EditorGUILayout.EndHorizontal();

            // 标题、完成状态、描述
            quest.title = EditorGUILayout.TextField("Title", quest.title);
            quest.isCompleted = EditorGUILayout.Toggle("Completed", quest.isCompleted);
            EditorGUILayout.LabelField("Description");
            quest.description = EditorGUILayout.TextArea(quest.description, GUILayout.Height(60));

            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();
    }
}
