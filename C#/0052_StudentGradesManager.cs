// 用于模拟一个简单的学生成绩管理系统
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentGradesManager
{
    class Student
    {
        public string Name { get; set; }
        public Dictionary<string, double> Grades { get; set; }

        public Student(string name)
        {
            Name = name;
            Grades = new Dictionary<string, double>();
        }

        public void AddGrade(string subject, double grade)
        {
            if (!Grades.ContainsKey(subject))
            {
                Grades[subject] = grade;
                Console.WriteLine($"成绩已添加：{subject} - {grade}");
            }
            else
            {
                Console.WriteLine($"{subject} 成绩已存在，可使用 UpdateGrade 修改。");
            }
        }

        public void UpdateGrade(string subject, double grade)
        {
            if (Grades.ContainsKey(subject))
            {
                Grades[subject] = grade;
                Console.WriteLine($"已更新成绩：{subject} - {grade}");
            }
            else
            {
                Console.WriteLine($"{subject} 不存在，请先添加成绩。");
            }
        }

        public double CalculateAverage()
        {
            return Grades.Count > 0 ? Grades.Values.Average() : 0.0;
        }

        public void DisplayGrades()
        {
            Console.WriteLine($"\n学生：{Name}");
            foreach (var grade in Grades)
            {
                Console.WriteLine($"{grade.Key}: {grade.Value}");
            }
            Console.WriteLine($"平均分：{CalculateAverage():F2}");
        }
    }

    class Program
    {
        static void Main()
        {
            List<Student> students = new List<Student>();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== 学生成绩管理系统 =====");
                Console.WriteLine("1. 添加学生");
                Console.WriteLine("2. 添加成绩");
                Console.WriteLine("3. 修改成绩");
                Console.WriteLine("4. 显示所有学生成绩");
                Console.WriteLine("5. 退出");
                Console.Write("请选择操作（1-5）：");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("请输入学生姓名：");
                        string name = Console.ReadLine();
                        students.Add(new Student(name));
                        Console.WriteLine("学生已添加。");
                        break;

                    case "2":
                        Student stuToAdd = SelectStudent(students);
                        if (stuToAdd != null)
                        {
                            Console.Write("输入科目名称：");
                            string subject = Console.ReadLine();
                            Console.Write("输入成绩：");
                            if (double.TryParse(Console.ReadLine(), out double score))
                            {
                                stuToAdd.AddGrade(subject, score);
                            }
                            else
                            {
                                Console.WriteLine("无效的成绩输入。");
                            }
                        }
                        break;

                    case "3":
                        Student stuToUpdate = SelectStudent(students);
                        if (stuToUpdate != null)
                        {
                            Console.Write("输入科目名称：");
                            string subject = Console.ReadLine();
                            Console.Write("输入新的成绩：");
                            if (double.TryParse(Console.ReadLine(), out double score))
                            {
                                stuToUpdate.UpdateGrade(subject, score);
                            }
                            else
                            {
                                Console.WriteLine("无效的成绩输入。");
                            }
                        }
                        break;

                    case "4":
                        foreach (var student in students)
                        {
                            student.DisplayGrades();
                        }
                        break;

                    case "5":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("无效选项，请输入 1-5。");
                        break;
                }
            }
        }

        static Student SelectStudent(List<Student> students)
        {
            if (students.Count == 0)
            {
                Console.WriteLine("当前没有学生信息，请先添加学生。");
                return null;
            }

            Console.WriteLine("请选择学生编号：");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {students[i].Name}");
            }

            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= students.Count)
            {
                return students[index - 1];
            }

            Console.WriteLine("输入错误！");
            return null;
        }
    }
}
