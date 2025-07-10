// MainMenuAndSceneLoader.cs 包含：主菜单按钮逻辑（新游戏 / 继续游戏）; 存档文件检测与处理; 场景加载（使用 SceneManager）; 全局 GameManager 单例类用于管理保存/读取行为，确保跨场景生效
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// 游戏主菜单脚本 + 场景加载与存档管理逻辑整合
/// 脚本名：MainMenuAndSceneLoader.cs
/// </summary>
public class MainMenuAndSceneLoader : MonoBehaviour
{
    public Button newGameButton;
    public Button continueButton;
    public string firstSceneName = "IntroScene";
    public string saveFileName = "savegame.json";

    private void Start()
    {
        newGameButton.onClick.AddListener(OnNewGame);
        continueButton.onClick.AddListener(OnContinue);

        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        continueButton.interactable = File.Exists(path);
    }

    void OnNewGame()
    {
        // 删除旧存档
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        if (File.Exists(path)) File.Delete(path);

        // 跳转到首个场景
        SceneManager.LoadScene(firstSceneName);
    }

    void OnContinue()
    {
        SceneManager.LoadScene(firstSceneName);
    }
}


// GameManager.cs
using UnityEngine;

/// <summary>
/// 游戏生命周期控制器：跨场景保存状态（单例）
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public SaveLoadSystem saveSystem;
    public DialogueQuestTrigger dialogue;
    public QuestManager questManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        saveSystem.SaveGame(dialogue.GetActiveFlags());
    }

    public void Load()
    {
        var state = saveSystem.LoadGame();
        if (state != null)
        {
            dialogue.SetActiveFlags(state.dialogueFlags);
        }
    }
}
