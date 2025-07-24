// 这个脚本将管理多个带有 SaveableObject 组件的对象，并集中保存它们的状态。
using System.Collections.Generic;
using UnityEngine;

public class MultiSaveTarget : MonoBehaviour
{
    public static MultiSaveTarget Instance;

    private void Awake()
    {
        Instance = this;
    }

    // 获取场景中所有需要保存的对象状态
    public List<SaveData> CreateSaveDataList()
    {
        List<SaveData> dataList = new List<SaveData>();
        SaveableObject[] objects = FindObjectsOfType<SaveableObject>();

        foreach (var obj in objects)
        {
            SaveData data = new SaveData();
            data.id = obj.id;
            data.position = obj.transform.position;
            data.rotation = obj.transform.rotation;
            data.prefabName = obj.prefabName;

            dataList.Add(data);
        }

        return dataList;
    }

    // 用保存的数据恢复所有对象状态（可扩展为支持对象重建）
    public void LoadFromDataList(List<SaveData> dataList)
    {
        SaveableObject[] existing = FindObjectsOfType<SaveableObject>();

        // 可选：先销毁当前对象，然后重新生成
        foreach (var obj in existing)
        {
            Destroy(obj.gameObject);
        }

        foreach (var data in dataList)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + data.prefabName);
            if (prefab != null)
            {
                GameObject go = Instantiate(prefab, data.position, data.rotation);
                SaveableObject obj = go.GetComponent<SaveableObject>();
                obj.id = data.id;
                obj.prefabName = data.prefabName;
            }
        }
    }
}
