import time

def focus_timer(minutes):
    seconds = minutes * 60
    for remaining_time in range(seconds, 0, -1):
        mins, secs = divmod(remaining_time, 60)
        timeformat = '{:02d}:{:02d}'.format(mins, secs)
        print(f"倒计时剩余时间: {timeformat}", end='\r')
        time.sleep(1)
    print("\n专注时间到！")

if __name__ == "__main__":
    try:
        minutes = int(input("请输入专注时间（分钟）："))
        focus_timer(minutes)
    except ValueError:
        print("请输入一个有效的数字。")
