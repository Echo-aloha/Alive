import random  # 导入模块，用于生成随机数

# 随机生成一个 1 到 100 的整数
secret_number = random.randint(1, 100)

print("我已经想好了一个 1 到 100 之间的数字。来猜猜看吧！")

# 无限循环，直到猜对为止
while True:
    guess = int(input("请输入你的猜测："))

    if guess < secret_number:
        print("太小了！")
    elif guess > secret_number:
        print("太大了！")
    else:
        print("恭喜你，猜对了！")
        break  # 退出循环
