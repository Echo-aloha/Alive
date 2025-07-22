// 此脚本负责从主菜单异步加载主游戏场景，并在加载过程中显示加载进度条。
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoaderWithProgress : MonoBehaviour
{
    [Header("UI")]
    public GameObject loadingScreen;
    public Slider progressBar;
    public Text progressText;

    // 外部调用此方法开始加载新场景
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            progressText.text = (progress * 100f).ToString("F0") + "%";

            // 场景加载到 90% 就绪，等待用户确认或自动激活
            if (operation.progress >= 0.9f)
            {
                progressText.text = "加载完成!";
                yield return new WaitForSeconds(0.5f); // 可选等待
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
