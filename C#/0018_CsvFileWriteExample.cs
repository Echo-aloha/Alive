// CsvFileWriteExample.cs 这个脚本展示如何将一组数据写入一个 .csv 文件，包含表头和多行内容。
using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "output.csv";

        // 模拟要写入的数据
        List<string[]> data = new List<string[]>
        {
            new string[] { "Name", "Age", "Gender" },    // 表头
            new string[] { "Alice", "25", "Female" },
            new string[] { "Bob", "30", "Male" },
            new string[] { "Charlie", "22", "Male" }
        };

        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (string[] row in data)
                {
                    string line = string.Join(",", row); // 将字段连接成一行
                    writer.WriteLine(line);
                }
            }

            Console.WriteLine("CSV file written successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to write CSV: " + ex.Message);
        }
    }
}
