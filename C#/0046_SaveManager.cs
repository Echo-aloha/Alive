// 该脚本用于将游戏数据保存为 JSON 文件，以及从 JSON 文件中读取还原游戏状态。
using UnityEngine;
using System.IO;

public static class SaveManager
{
    private static string saveDirectory = Application.persistentDataPath + "/Saves/";

    // 保存数据到指定文件名
    public static void Save(SaveData data, string fileName)
    {
        // 确保保存目录存在
        if (!Directory.Exists(saveDirectory))
            Directory.CreateDirectory(saveDirectory);

        string json = JsonUtility.ToJson(data, true);
        string path = saveDirectory + fileName + ".json";
        File.WriteAllText(path, json);
        Debug.Log("数据已保存至：" + path);
    }

    // 从指定文件名加载数据
    public static SaveData Load(string fileName)
    {
        string path = saveDirectory + fileName + ".json";

        if (!File.Exists(path))
        {
            Debug.LogWarning("未找到存档文件：" + path);
            return null;
        }

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        Debug.Log("数据已加载：" + path);
        return data;
    }

    // 检查存档是否存在
    public static bool SaveExists(string fileName)
    {
        return File.Exists(saveDirectory + fileName + ".json");
    }

    // 删除指定存档
    public static void DeleteSave(string fileName)
    {
        string path = saveDirectory + fileName + ".json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("已删除存档：" + path);
        }
    }

    // 获取所有存档文件名
    public static string[] GetAllSaveFiles()
    {
        if (!Directory.Exists(saveDirectory))
            return new string[0];

        string[] files = Directory.GetFiles(saveDirectory, "*.json");
        for (int i = 0; i < files.Length; i++)
        {
            files[i] = Path.GetFileNameWithoutExtension(files[i]);
        }
        return files;
    }
}
