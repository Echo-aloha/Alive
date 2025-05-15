# 简易待办事项管理器

todo_list = []  # 用列表保存任务

def show_menu():
    print("\n===== 待办事项管理器 =====")
    print("1. 添加任务")
    print("2. 查看任务")
    print("3. 删除任务")
    print("4. 退出程序")

while True:
    show_menu()
    choice = input("请输入选项（1-4）：")

    if choice == "1":
        task = input("请输入新任务内容：")
        todo_list.append(task)
        print("任务已添加！")
    elif choice == "2":
        print("\n当前任务列表：")
        if not todo_list:
            print("（无任务）")
        else:
            for i, task in enumerate(todo_list, start=1):
                print(f"{i}. {task}")
    elif choice == "3":
        task_num = int(input("请输入要删除的任务编号："))
        if 1 <= task_num <= len(todo_list):
            removed = todo_list.pop(task_num - 1)
            print(f"任务“{removed}”已删除。")
        else:
            print("无效的任务编号。")
    elif choice == "4":
        print("退出程序，再见！")
        break
    else:
        print("请输入有效的选项（1-4）")
