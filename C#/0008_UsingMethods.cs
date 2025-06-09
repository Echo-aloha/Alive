// UsingMethods.cs 这个脚本展示如何定义一个计算两个数和的函数，并从主程序中调用它。
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter first number: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Enter second number: ");
        int b = int.Parse(Console.ReadLine());

        int result = AddNumbers(a, b);
        Console.WriteLine($"The sum of {a} and {b} is {result}");
    }

    // 自定义方法：接收两个整数，返回它们的和
    static int AddNumbers(int x, int y)
    {
        return x + y;
    }
}
