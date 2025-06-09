// ClassAndObject.cs 这个脚本展示如何创建一个 Person 类，并在主程序中使用它创建对象和访问其属性和方法。
using System;

class Person
{
    // 属性
    public string Name;
    public int Age;

    // 方法
    public void Introduce()
    {
        Console.WriteLine($"Hi, my name is {Name}, and I am {Age} years old.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 创建对象
        Person person1 = new Person();
        person1.Name = "Alice";
        person1.Age = 30;

        person1.Introduce();

        // 再创建一个对象
        Person person2 = new Person();
        person2.Name = "Bob";
        person2.Age = 25;

        person2.Introduce();
    }
}
