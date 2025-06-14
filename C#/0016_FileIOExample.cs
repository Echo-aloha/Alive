// FileIOExample.cs 这个脚本演示如何使用 System.IO 读取和写入 .txt 文件。
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "data.txt";

        // 写入文本
        try
        {
            File.WriteAllText(filePath, "Hello, this is a test.\nWelcome to C# File I/O!");
            Console.WriteLine("Write successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Write failed: " + ex.Message);
        }

        // 读取文本
        try
        {
            string content = File.ReadAllText(filePath);
            Console.WriteLine("File content:");
            Console.WriteLine(content);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Read failed: " + ex.Message);
        }
    }
}
