# 猜数字小游戏

import random

# 随机生成一个1到100之间的整数
secret_number = random.randint(1, 100)

print("🎲 欢迎进入猜数字游戏！")
print("我已经想好了一个 1 到 100 之间的数字，你能猜出来吗？")

attempts = 0

while True:
    try:
        guess = int(input("请输入你的猜测："))
        attempts += 1

        if guess < secret_number:
            print("太小了，再试试！")
        elif guess > secret_number:
            print("太大了，再来一次！")
        else:
            print(f"🎉 恭喜你，猜对了！答案就是 {secret_number}。")
            print(f"你一共猜了 {attempts} 次。")
            break
    except ValueError:
        print("请输入一个有效的整数。")
