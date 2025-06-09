// ConstructorExample.cs 这个脚本演示如何使用构造函数在创建对象时直接赋值，避免重复写代码。
using System;

class Person
{
    public string Name;
    public int Age;

    // 构造函数：在创建对象时自动调用
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void Introduce()
    {
        Console.WriteLine($"Hi, I'm {Name}, and I'm {Age} years old.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 使用构造函数创建对象并初始化
        Person person1 = new Person("Alice", 30);
        Person person2 = new Person("Bob", 25);

        person1.Introduce();
        person2.Introduce();
    }
}
