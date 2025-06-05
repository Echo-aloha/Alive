// UserInput.cs  这个脚本演示如何从控制台获取用户输入，并将输入转换为适当的数据类型进行处理。
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();

        Console.Write("Enter your age: ");
        string ageInput = Console.ReadLine();
        int age = int.Parse(ageInput); // 将字符串转换为整数

        Console.WriteLine("Hello, " + name + "! You are " + age + " years old.");
    }
}
