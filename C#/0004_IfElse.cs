// IfElse.cs 这个脚本展示了如何使用 if、else if 和 else 来根据用户输入进行不同的逻辑判断。
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your score (0-100): ");
        string input = Console.ReadLine();
        int score = int.Parse(input);

        if (score >= 90)
        {
            Console.WriteLine("Grade: A");
        }
        else if (score >= 80)
        {
            Console.WriteLine("Grade: B");
        }
        else if (score >= 70)
        {
            Console.WriteLine("Grade: C");
        }
        else if (score >= 60)
        {
            Console.WriteLine("Grade: D");
        }
        else
        {
            Console.WriteLine("Grade: F");
        }
    }
}
