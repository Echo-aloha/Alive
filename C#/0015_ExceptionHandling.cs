// ExceptionHandling.cs 这个脚本模拟用户输入错误数据的情况，并通过 try-catch 安全处理异常。
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter an integer: ");
        string input = Console.ReadLine();

        try
        {
            int number = int.Parse(input);  // 可能抛出异常
            Console.WriteLine($"You entered: {number}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Input was not a valid integer.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("The number is too large or too small.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Program finished (with or without error).");
        }
    }
}
