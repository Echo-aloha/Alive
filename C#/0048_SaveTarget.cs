// 这个脚本是你希望保存和加载状态的对象（如玩家、车辆）上挂载的组件。
using UnityEngine;

public class SaveTarget : MonoBehaviour
{
    public static SaveTarget Instance;

    public Transform objectToSave; // 通常是玩家或车辆等主对象

    private void Awake()
    {
        Instance = this;
    }

    // 创建当前状态的存档数据
    public SaveData CreateSaveData()
    {
        SaveData data = new SaveData();

        // 保存位置和旋转
        data.position = objectToSave.position;
        data.rotation = objectToSave.rotation;

        // 如果需要保存更多内容，可扩展这里
        // data.speed = ...
        // data.health = ...

        return data;
    }

    // 加载并应用存档数据
    public void LoadFromData(SaveData data)
    {
        objectToSave.position = data.position;
        objectToSave.rotation = data.rotation;

        // 同样可以恢复更多内容
        // objectToSave.GetComponent<Health>().Set(data.health);
    }
}
