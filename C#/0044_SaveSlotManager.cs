// 该脚本用于：在存档选择界面中 加载所有已有存档；实例化多个 SaveSlot UI；向每个槽位传入正确的数据（存档文件名、时间等）；支持空槽显示（例如总共 3 个存档位，只有 1 个有内容）。
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SaveSlotManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject slotPrefab;               // 预制体，每个存档按钮
    public Transform slotContainer;             // 存放按钮的父对象
    public int maxSlots = 3;                    // 最多几个存档位

    private void Start()
    {
        LoadAllSlots();
    }

    void LoadAllSlots()
    {
        // 清空旧的槽位
        foreach (Transform child in slotContainer)
        {
            Destroy(child.gameObject);
        }

        // 获取存档路径
        string folderPath = Application.persistentDataPath + "/Saves";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // 获取所有存档文件
        string[] files = Directory.GetFiles(folderPath, "*.json");

        // 已加载的文件数
        int count = 0;

        // 加载已有存档
        foreach (string file in files)
        {
            if (count >= maxSlots) break;

            string fileName = Path.GetFileNameWithoutExtension(file);
            string json = File.ReadAllText(file);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            GameObject slotObj = Instantiate(slotPrefab, slotContainer);
            SaveSlot slot = slotObj.GetComponent<SaveSlot>();
            slot.SetData(fileName, "存档" + (count + 1), data.saveTime, false);

            count++;
        }

        // 创建空存档位
        for (int i = count; i < maxSlots; i++)
        {
            string newFileName = "Save_" + (i + 1);
            GameObject slotObj = Instantiate(slotPrefab, slotContainer);
            SaveSlot slot = slotObj.GetComponent<SaveSlot>();
            slot.SetData(newFileName, "空存档位 " + (i + 1), "", true);
        }
    }
}
