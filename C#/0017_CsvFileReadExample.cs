// CsvFileReadExample.cs 这个脚本展示如何读取 .csv 文件中的每一行，并将其拆分为字段。
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string csvPath = "data.csv";

        try
        {
            // 逐行读取 CSV
            string[] lines = File.ReadAllLines(csvPath);

            Console.WriteLine("CSV content:");
            foreach (string line in lines)
            {
                // 用逗号分隔每行
                string[] fields = line.Split(',');

                // 输出每个字段
                Console.WriteLine("Row:");
                for (int i = 0; i < fields.Length; i++)
                {
                    Console.WriteLine($"  Column {i + 1}: {fields[i]}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to read CSV: " + ex.Message);
        }
    }
}
