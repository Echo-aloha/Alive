// 实现了一个基础的文本冒险游戏框架，包含类、方法、分支、循环、用户交互等核心知识点
using System;

class AdventureGame
{
    static void Main(string[] args)
    {
        StartGame();
    }

    static void StartGame()
    {
        Console.WriteLine("欢迎来到冒险游戏！");
        Console.WriteLine("请输入你的角色名称：");
        string playerName = Console.ReadLine();

        Console.WriteLine($"你好，{playerName}，你站在一座神秘森林的入口处。");
        Console.WriteLine("你想进入森林吗？(yes/no)");

        string choice = Console.ReadLine().ToLower();

        if (choice == "yes")
        {
            EnterForest(playerName);
        }
        else
        {
            Console.WriteLine("你选择了放弃冒险。游戏结束！");
        }
    }

    static void EnterForest(string name)
    {
        Console.WriteLine("你踏入了阴森的森林，鸟语声逐渐消失，只剩下风声。");
        Console.WriteLine("你遇到了一只狼！你要怎么办？(fight/run)");

        string action = Console.ReadLine().ToLower();

        if (action == "fight")
        {
            FightWolf(name);
        }
        else if (action == "run")
        {
            Console.WriteLine("你选择了逃跑，侥幸脱身了。");
            EndGame(name, false);
        }
        else
        {
            Console.WriteLine("你犹豫不决，被狼扑倒了……");
            EndGame(name, true);
        }
    }

    static void FightWolf(string name)
    {
        Random rand = new Random();
        int playerAttack = rand.Next(5, 15);
        int wolfHealth = rand.Next(10, 20);

        Console.WriteLine($"{name} 攻击了狼，造成了 {playerAttack} 点伤害！");
        Console.WriteLine($"狼的生命值为 {wolfHealth}");

        if (playerAttack >= wolfHealth)
        {
            Console.WriteLine("你打败了狼！");
            Console.WriteLine("你在狼的洞穴中找到了一把宝剑！");
            EndGame(name, false, "宝剑");
        }
        else
        {
            Console.WriteLine("你没有打败狼，被它反击了！");
            EndGame(name, true);
        }
    }

    static void EndGame(string name, bool died, string treasure = null)
    {
        if (died)
        {
            Console.WriteLine($"很遗憾，{name} 在冒险中牺牲了。");
        }
        else
        {
            Console.WriteLine($"{name} 成功完成了冒险！");
            if (treasure != null)
            {
                Console.WriteLine($"你带着 {treasure} 回到了村庄，成为了英雄！");
            }
        }

        Console.WriteLine("感谢游玩本游戏！");
    }
}
