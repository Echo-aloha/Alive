// PropertiesExample.cs  这个脚本展示如何使用属性（get 和 set）来控制对类内部数据的访问与修改。
using System;

class Person
{
    // 私有字段
    private string name;
    private int age;

    // 公共属性：控制字段访问
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Age
    {
        get { return age; }
        set
        {
            if (value >= 0)
                age = value;
            else
                Console.WriteLine("Age cannot be negative!");
        }
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
        Person p = new Person();
        p.Name = "Charlie";
        p.Age = 28;

        p.Introduce();

        p.Age = -5; // 会触发属性中的验证逻辑
    }
}
