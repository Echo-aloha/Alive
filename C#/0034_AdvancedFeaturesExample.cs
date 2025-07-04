// AdvancedFeaturesExample.cs 展示了委托与事件、Lambda 与 LINQ、协程和异步方法的综合示例
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// 演示 C# 高级特性：委托、事件、Lambda 表达式、LINQ 查询、协程与异步方法
/// 脚本名：AdvancedFeaturesExample.cs
/// </summary>
public class AdvancedFeaturesExample : MonoBehaviour
{
    // 1. 委托与事件
    public delegate void MessageDelegate(string message);
    public event MessageDelegate OnMessageReceived;

    // 2. 示例数据：玩家信息列表
    private List<Player> players = new List<Player>
    {
        new Player { Name = "Alice", Score = 85 },
        new Player { Name = "Bob", Score = 92 },
        new Player { Name = "Charlie", Score = 78 }
    };

    void Start()
    {
        // 订阅事件
        OnMessageReceived += HandleMessage;

        // 触发事件
        OnMessageReceived?.Invoke("Hello from event!");

        // 3. LINQ 查询：筛选高分玩家，并按分数降序获取姓名
        var highScorers = players
            .Where(p => p.Score > 80)
            .OrderByDescending(p => p.Score)
            .Select(p => p.Name);

        Debug.Log("High scorers: " + string.Join(", ", highScorers));

        // 4. 启动协程（Coroutine）
        StartCoroutine(CountdownCoroutine(3));

        // 5. 调用异步方法（async/await）
        ShowAsyncMessage();
    }

    // 事件处理方法
    void HandleMessage(string msg)
    {
        Debug.Log("[Event] " + msg);
    }

    // 协程示例：倒计时
    IEnumerator CountdownCoroutine(int seconds)
    {
        while (seconds > 0)
        {
            Debug.Log("Countdown: " + seconds);
            yield return new WaitForSeconds(1f);
            seconds--;
        }
        Debug.Log("Coroutine finished!");
    }

    // 异步方法示例：2 秒后输出
    async void ShowAsyncMessage()
    {
        await Task.Delay(2000);
        Debug.Log("Async method after 2 seconds.");
    }
}

[Serializable]
public class Player
{
    public string Name;
    public int Score;
}
