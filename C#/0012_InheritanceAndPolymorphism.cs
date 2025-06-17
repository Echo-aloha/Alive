// InheritanceAndPolymorphism.cs 这个脚本展示如何创建一个基类 Animal，并由两个子类 Dog 和 Cat 继承并重写方法。
using System;

class Animal
{
    public string Name { get; set; }

    public Animal(string name)
    {
        Name = name;
    }

    // 可被子类重写的方法
    public virtual void Speak()
    {
        Console.WriteLine($"{Name} makes a sound.");
    }
}

class Dog : Animal
{
    public Dog(string name) : base(name) { }

    // 重写基类方法
    public override void Speak()
    {
        Console.WriteLine($"{Name} says: Woof!");
    }
}

class Cat : Animal
{
    public Cat(string name) : base(name) { }

    public override void Speak()
    {
        Console.WriteLine($"{Name} says: Meow!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Animal myDog = new Dog("Buddy");
        Animal myCat = new Cat("Kitty");

        myDog.Speak();  // Buddy says: Woof!
        myCat.Speak();  // Kitty says: Meow!
    }
}
