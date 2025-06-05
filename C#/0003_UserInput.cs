// UserInput.cs
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
