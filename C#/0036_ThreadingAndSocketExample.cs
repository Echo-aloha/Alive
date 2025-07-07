// ThreadingAndSocketExample.cs 展示如何在 Unity 中使用后台线程启动 TCP 服务器，并通过协程模拟客户端进行发送和接收，示范多线程与套接字编程的结合。
using UnityEngine;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;

/// <summary>
/// 演示 C# 多线程与 TCP 套接字通信示例：
/// - 后台线程启动 TCP 服务器
/// - 协程模拟客户端连接并收发消息
/// 脚本名：ThreadingAndSocketExample.cs
/// </summary>
public class ThreadingAndSocketExample : MonoBehaviour
{
    private Thread listenerThread;
    private TcpListener tcpListener;
    private bool isListening = false;

    void Start()
    {
        // 启动服务器线程
        listenerThread = new Thread(new ThreadStart(StartServer));
        listenerThread.IsBackground = true;
        listenerThread.Start();

        // 启动客户端协程
        StartCoroutine(ClientCoroutine());
    }

    void OnApplicationQuit()
    {
        StopServer();
    }

    /// <summary>
    /// 在后台线程中启动 TCP 服务器
    /// </summary>
    void StartServer()
    {
        try
        {
            tcpListener = new TcpListener(IPAddress.Loopback, 5000);
            tcpListener.Start();
            isListening = true;
            Debug.Log("[Server] Listening on port 5000");

            while (isListening)
            {
                // 等待客户端连接
                TcpClient client = tcpListener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                // 读取客户端消息
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Debug.Log("[Server] Received: " + message);

                // 回显消息
                byte[] response = Encoding.UTF8.GetBytes("Echo: " + message);
                stream.Write(response, 0, response.Length);

                client.Close();
            }
        }
        catch (SocketException ex)
        {
            Debug.LogError("[Server] SocketException: " + ex.Message);
        }
    }

    /// <summary>
    /// 停止服务器并终止线程
    /// </summary>
    void StopServer()
    {
        isListening = false;
        tcpListener?.Stop();
        listenerThread?.Join();
    }

    /// <summary>
    /// 模拟客户端通过协程连接服务器并发送/接收消息
    /// </summary>
    IEnumerator ClientCoroutine()
    {
        // 等待服务器启动
        yield return new WaitForSeconds(1f);

        try
        {
            using (TcpClient client = new TcpClient("127.0.0.1", 5000))
            {
                NetworkStream stream = client.GetStream();
                string msg = "Hello Server!";
                byte[] data = Encoding.UTF8.GetBytes(msg);
                stream.Write(data, 0, data.Length);
                Debug.Log("[Client] Sent: " + msg);

                // 接收服务器回显
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Debug.Log("[Client] Received: " + response);
            }
        }
        catch (SocketException ex)
        {
            Debug.LogError("[Client] SocketException: " + ex.Message);
        }
    }
}
