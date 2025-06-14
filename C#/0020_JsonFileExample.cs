// JsonFileExample.cs 这个脚本使用 C# 的 System.Text.Json 库来将对象写入 JSON 文件，再从文件中读取回来。
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        string jsonPath = "people.json";

        // 1. 创建对象列表
        List<Person> people = new List<Person>
        {
            new Person { Name = "Alice", Age = 25, Gender = "Female" },
            new Person { Name = "Bob", Age = 30, Gender = "Male" },
            new Person { Name = "Charlie", Age = 22, Gender = "Male" }
        };

        // 2. 写入 JSON 文件
        try
        {
            string json = JsonSerializer.Serialize(people, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonPath, json);
            Console.WriteLine("JSON written successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Write error: " + ex.Message);
        }

        // 3. 读取 JSON 文件
        try
        {
            string jsonFromFile = File.ReadAllText(jsonPath);
            List<Person> loadedPeople = JsonSerializer.Deserialize<List<Person>>(jsonFromFile);

            Console.WriteLine("Loaded people from JSON:");
            foreach (var p in loadedPeople)
            {
                Console.WriteLine($"{p.Name}, {p.Age}, {p.Gender}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Read error: " + ex.Message);
        }
    }
}
