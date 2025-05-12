import random

number = random.randint(1, 100)
guess = None

while guess != number:
    guess = int(input("猜一个 1 到 100 之间的数字: "))
    if guess < number:
        print("太小了！")
    elif guess > number:
        print("太大了！")
    else:
        print("恭喜你，猜对了！")
