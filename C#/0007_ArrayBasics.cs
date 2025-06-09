// ArrayBasics.cs 这个脚本展示如何定义一个数组，存储多个数字，然后计算它们的平均值。
using System;

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = new int[5]; // 创建一个长度为5的整数数组

        Console.WriteLine("Enter 5 integers:");

        for (int i = 0; i < numbers.Length; i++)
        {
            Console.Write($"Number {i + 1}: ");
            numbers[i] = int.Parse(Console.ReadLine());
        }

        int sum = 0;
        foreach (int num in numbers)
        {
            sum += num;
        }

        double average = (double)sum / numbers.Length;
        Console.WriteLine($"The average is: {average}");
    }
}
