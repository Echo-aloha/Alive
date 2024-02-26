import time

# 设置倒计时的秒数
countdown_seconds = 270

while countdown_seconds > 0:
    minutes, seconds = divmod(countdown_seconds, 60)
    print(f"倒计时: {minutes:02d}:{seconds:02d}", end="\r")
    time.sleep(1)
    countdown_seconds -= 1

print("倒计时结束！")
