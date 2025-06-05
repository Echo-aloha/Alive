// LoopingWithWhile.cs  这个脚本演示如何用 while 循环重复执行一段代码，直到用户输入特定内容。
using System;

class Program
{
    static void Main(string[] args)
    {
        string input = "";

        while (input != "exit")
        {
            Console.Write("Type something (or type 'exit' to quit): ");
            input = Console.ReadLine();
            Console.WriteLine("You typed: " + input);
        }

        Console.WriteLine("Program ended.");
    }
}
