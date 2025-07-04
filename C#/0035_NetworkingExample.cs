// NetworkingExample.cs 示范了 C# HttpClient 异步请求及 UnityWebRequest 协程的网络编程用法
using System;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 演示 C# 和 Unity 网络编程：HttpClient 异步请求 与 UnityWebRequest 协程
/// 脚本名：NetworkingExample.cs
/// </summary>
public class NetworkingExample : MonoBehaviour
{
    // 1. .NET HttpClient (异步 GET 请求)
    private static readonly HttpClient httpClient = new HttpClient();

    void Start()
    {
        // HttpClient 异步 GET
        _ = FetchDataAsync("https://api.example.com/data");

        // UnityWebRequest GET
        StartCoroutine(FetchDataCoroutine("https://api.example.com/data"));

        // UnityWebRequest POST
        string jsonPayload = "{\"name\":\"Alice\",\"score\":95}";
        StartCoroutine(PostDataCoroutine("https://api.example.com/submit", jsonPayload));
    }

    /// <summary>
    /// 异步 GET 示例
    /// </summary>
    async Task FetchDataAsync(string url)
    {
        try
        {
            HttpResponseMessage response = await httpClient.GetAsync(url, CancellationToken.None);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            Debug.Log("[HttpClient GET] " + content);
        }
        catch (Exception ex)
        {
            Debug.LogError("[HttpClient Error] " + ex.Message);
        }
    }

    /// <summary>
    /// UnityWebRequest GET 协程示例
    /// </summary>
    IEnumerator FetchDataCoroutine(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
                Debug.Log("[UnityWebRequest GET] " + request.downloadHandler.text);
            else
                Debug.LogError("[UnityWebRequest Error] " + request.error);
        }
    }

    /// <summary>
    /// UnityWebRequest POST 协程示例
    /// </summary>
    IEnumerator PostDataCoroutine(string url, string jsonPayload)
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
                Debug.Log("[UnityWebRequest POST] " + request.downloadHandler.text);
            else
                Debug.LogError("[UnityWebRequest POST Error] " + request.error);
        }
    }
}
