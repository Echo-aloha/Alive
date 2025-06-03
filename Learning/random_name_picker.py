# 随机点名 / 抽奖工具

import random

def pick_name(name_list):
    if not name_list:
        return "名单为空，无法抽取。"
    return random.choice(name_list)

# 用户输入名字，英文逗号分隔
raw_input = input("请输入所有名字，用英文逗号分隔（如：Alice,Bob,Charlie）：\n")
name_list = [name.strip() for name in raw_input.split(",") if name.strip()]

# 抽取一个名字
winner = pick_name(name_list)
print(f"🎉 抽中的人是：{winner}")
