# 学生成绩管理系统（基础版）

grades = {}  # 使用字典存储：键是学生姓名，值是成绩

def show_menu():
    print("\n=== 学生成绩管理系统 ===")
    print("1. 添加学生成绩")
    print("2. 查看所有成绩")
    print("3. 计算平均成绩")
    print("4. 退出程序")

while True:
    show_menu()
    choice = input("请输入选项（1-4）：")

    if choice == "1":
        name = input("请输入学生姓名：")
        try:
            score = float(input("请输入成绩（0-100）："))
            grades[name] = score
            print(f"已添加：{name} 的成绩是 {score}")
        except ValueError:
            print("请输入有效的数字成绩。")
    elif choice == "2":
        if not grades:
            print("暂无成绩记录。")
        else:
            print("\n学生成绩列表：")
            for name, score in grades.items():
                print(f"{name}：{score}")
    elif choice == "3":
        if not grades:
            print("暂无成绩，无法计算平均值。")
        else:
            avg = sum(grades.values()) / len(grades)
            print(f"全班平均成绩为：{avg:.2f}")
    elif choice == "4":
        print("程序已退出，再见！")
        break
    else:
        print("请输入有效选项（1-4）")
