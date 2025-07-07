// BattleSystemIntegration.cs 包括从对话状态 JSON 检测触发标记、批量生成敌人、简易战斗流程的完整代码示例。请将其挂在一个管理对象上，并在 Inspector 中配置 enemyPrefab 与 spawnPoints 列表。
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// 整合对话分支与战斗系统：检测对话标记触发敌人生成与战斗流程
/// 脚本名：BattleSystemIntegration.cs
/// </summary>
public class BattleSystemIntegration : MonoBehaviour
{
    [System.Serializable]
    public class DialogueState
    {
        public List<string> selectedFlags;
    }

    public string startFlag = "start_goblin_quest";
    public string stateFileName = "dialogue_state.json";
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    private bool battleStarted = false;
    private List<GameObject> enemies = new List<GameObject>();

    void Update()
    {
        if (battleStarted) return;

        string path = Path.Combine(Application.persistentDataPath, stateFileName);
        if (!File.Exists(path)) return;

        try
        {
            var state = JsonConvert.DeserializeObject<DialogueState>(File.ReadAllText(path));
            if (state.selectedFlags != null && state.selectedFlags.Contains(startFlag))
            {
                battleStarted = true;
                StartCoroutine(StartBattle());
            }
        }
        catch { /* 处理反序列化错误 */ }
    }

    IEnumerator StartBattle()
    {
        // 生成敌人
        foreach (var pt in spawnPoints)
        {
            var go = Instantiate(enemyPrefab, pt.position, Quaternion.identity);
            var se = go.AddComponent<SimpleEnemy>();
            se.Initialize(50);
            enemies.Add(go);
        }

        // 等待所有敌人被消灭
        while (enemies.Exists(e => e != null))
            yield return null;

        Debug.Log("[Battle] All enemies defeated!");
        // 可在此触发后续逻辑，如下一任务或对话
    }
}

/// <summary>
/// 简易敌人类：接收伤害并销毁
/// </summary>
public class SimpleEnemy : MonoBehaviour
{
    private int health;

    public void Initialize(int hp)
    {
        health = hp;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("PlayerProjectile"))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
            Destroy(gameObject);
    }
}
