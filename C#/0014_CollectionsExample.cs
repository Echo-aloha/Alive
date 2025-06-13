// CollectionsExample.cs 这个脚本展示了如何使用列表来存储多个值，以及使用字典来建立键值对映射。
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // List 示例：存储多个名字
        List<string> names = new List<string>();
        names.Add("Alice");
        names.Add("Bob");
        names.Add("Charlie");

        Console.WriteLine("Names in the list:");
        foreach (string name in names)
        {
            Console.WriteLine("- " + name);
        }

        Console.WriteLine();

        // Dictionary 示例：存储学生姓名与成绩
        Dictionary<string, int> scores = new Dictionary<string, int>();
        scores["Alice"] = 95;
        scores["Bob"] = 88;
        scores["Charlie"] = 72;

        Console.WriteLine("Student scores:");
        foreach (KeyValuePair<string, int> entry in scores)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }

        // 判断是否存在键
        if (scores.ContainsKey("David"))
        {
            Console.WriteLine($"David's score is {scores["David"]}");
        }
        else
        {
            Console.WriteLine("David is not in the score list.");
        }
    }
}
