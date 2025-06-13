// InterfaceExample.cs 这个脚本展示了如何定义一个接口 IMovable，并由 Car 和 Bicycle 类实现该接口。
using System;

// 定义接口：只包含方法签名，不包含实现
interface IMovable
{
    void Move();
}

// 类实现接口，必须实现接口中所有方法
class Car : IMovable
{
    public void Move()
    {
        Console.WriteLine("Car is driving on the road.");
    }
}

class Bicycle : IMovable
{
    public void Move()
    {
        Console.WriteLine("Bicycle is pedaling on the track.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        IMovable vehicle1 = new Car();
        IMovable vehicle2 = new Bicycle();

        vehicle1.Move();  // 输出：Car is driving on the road.
        vehicle2.Move();  // 输出：Bicycle is pedaling on the track.
    }
}
