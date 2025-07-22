// 该脚本用于定义 存档数据结构，用于保存和加载游戏状态时的数据序列化。
using System;

[System.Serializable]
public class SaveData
{
    public string playerName;
    public int level;
    public float health;
    public float[] position;     // 位置：x,y,z
    public string saveTime;      // 存档时间字符串

    public SaveData()
    {
        // 可选：初始化默认值
        playerName = "默认玩家";
        level = 1;
        health = 100f;
        position = new float[] { 0f, 0f, 0f };
        saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
