# 简单倒计时器

import time

try:
    seconds = int(input("请输入倒计时时间（秒）："))

    print(f"开始倒计时：{seconds} 秒")
    while seconds > 0:
        print(f"剩余时间：{seconds} 秒")
        time.sleep(1)  # 暂停1秒
        seconds -= 1

    print("⏰ 时间到！")
except ValueError:
    print("请输入有效的整数秒数。")
