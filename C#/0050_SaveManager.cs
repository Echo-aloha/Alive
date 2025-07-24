// 多对象版本。此版本会保存整个对象列表到 JSON 文件中，并从中读取恢复。
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    private static string savePath => Path.Combine(Application.persistentDataPath, "save.json");

    // 保存多个对象的状态到JSON文件
    public static void Save()
    {
        List<SaveData> dataList = MultiSaveTarget.Instance.CreateSaveDataList();
        SaveDataListWrapper wrapper = new SaveDataListWrapper { dataList = dataList };

        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(savePath, json);

        Debug.Log("保存成功：" + savePath);
    }

    // 从JSON文件中加载对象状态
    public static void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveDataListWrapper wrapper = JsonUtility.FromJson<SaveDataListWrapper>(json);

            MultiSaveTarget.Instance.LoadFromDataList(wrapper.dataList);

            Debug.Log("加载成功：" + savePath);
        }
        else
        {
            Debug.LogWarning("找不到保存文件！");
        }
    }

    // 删除保存文件
    public static void Delete()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("已删除保存文件");
        }
    }
}
