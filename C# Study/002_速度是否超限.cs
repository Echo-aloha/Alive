using System;

class Program
{
    static void Main()
    {
        double speed = 92.5;
        double limit = 80.0;

        if (speed > limit)
        {
            Console.WriteLine("超速");
        }
        else
        {
            Console.WriteLine("未超速");
        }
    }
}
