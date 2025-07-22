// 该脚本用于表示单个存档位的信息展示与响应点击，用于在存档界面显示“存档1 / 存档2 / 空存档”等状态，并点击后切换或加载。
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSlot : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI slotNameText;
    public TextMeshProUGUI dateText;
    public Button slotButton;

    [Header("Data")]
    public string saveFileName;
    public bool isEmptySlot;

    public void SetData(string fileName, string displayName, string date, bool isEmpty)
    {
        saveFileName = fileName;
        slotNameText.text = displayName;
        dateText.text = date;
        isEmptySlot = isEmpty;

        if (isEmpty)
        {
            slotNameText.text = "空存档";
            dateText.text = "";
        }
    }

    private void Start()
    {
        slotButton.onClick.AddListener(OnSlotClicked);
    }

    public void OnSlotClicked()
    {
        if (isEmptySlot)
        {
            Debug.Log($"新建存档：{saveFileName}");
            SaveManager.Instance.CreateNewGame(saveFileName);
        }
        else
        {
            Debug.Log($"读取存档：{saveFileName}");
            SaveManager.Instance.LoadGame(saveFileName);
        }
    }
}
