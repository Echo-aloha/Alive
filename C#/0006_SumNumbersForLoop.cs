// SumNumbersForLoop.cs 这个脚本示范如何使用 for 循环把 1 到某个正整数 N 的所有数相加，并输出结果。
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter a positive integer N: ");
        string input = Console.ReadLine();
        int n = int.Parse(input);

        int sum = 0;
        for (int i = 1; i <= n; i++)
        {
            sum += i; // 等价于 sum = sum + i
        }

        Console.WriteLine($"The sum of numbers from 1 to {n} is {sum}.");
    }
}
