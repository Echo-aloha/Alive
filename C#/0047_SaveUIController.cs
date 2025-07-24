// 该脚本会自动读取所有存档文件名，并在 UI 中创建按钮列表。每个按钮可以点击来加载或删除该存档。
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SaveUIController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject saveButtonPrefab;
    public Transform saveListContainer;
    public TMP_InputField newSaveNameInput;
    public Button saveButton;

    private void Start()
    {
        RefreshSaveList();
        saveButton.onClick.AddListener(OnSaveNewFile);
    }

    public void RefreshSaveList()
    {
        // 清空旧的 UI 项
        foreach (Transform child in saveListContainer)
        {
            Destroy(child.gameObject);
        }

        // 获取所有存档文件
        string[] files = SaveManager.GetAllSaveFiles();
        foreach (string file in files)
        {
            GameObject go = Instantiate(saveButtonPrefab, saveListContainer);
            SaveListItem item = go.GetComponent<SaveListItem>();
            item.Setup(file, this);
        }
    }

    public void OnSaveNewFile()
    {
        string saveName = newSaveNameInput.text.Trim();
        if (string.IsNullOrEmpty(saveName))
        {
            Debug.LogWarning("请输入存档名称");
            return;
        }

        SaveData data = SaveTarget.Instance.CreateSaveData(); // 假设存在一个保存目标类
        SaveManager.Save(data, saveName);
        RefreshSaveList();
    }

    public void OnLoadClicked(string fileName)
    {
        SaveData data = SaveManager.Load(fileName);
        if (data != null)
        {
            SaveTarget.Instance.LoadFromData(data); // 加载存档数据
        }
    }

    public void OnDeleteClicked(string fileName)
    {
        SaveManager.DeleteSave(fileName);
        RefreshSaveList();
    }
}
