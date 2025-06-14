// CsvToObjectExample.cs 这个脚本展示如何定义 Person 类，读取带表头的 .csv 文件，并将每行数据转为 Person 对象存储到列表中。
using System;
using System.IO;
using System.Collections.Generic;

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }

    public void Introduce()
    {
        Console.WriteLine($"Hi, I'm {Name}, {Age} years old, {Gender}.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        string filePath = "people.csv";
        List<Person> people = new List<Person>();

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++) // 跳过表头
            {
                string[] fields = lines[i].Split(',');

                Person p = new Person
                {
                    Name = fields[0],
                    Age = int.Parse(fields[1]),
                    Gender = fields[2]
                };

                people.Add(p);
            }

            Console.WriteLine("Loaded people from CSV:");
            foreach (Person p in people)
            {
                p.Introduce();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading CSV: " + ex.Message);
        }
    }
}
