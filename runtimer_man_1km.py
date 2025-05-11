import time

def calculate_bmi(height_cm, weight_kg):
    height_m = height_cm / 100
    return weight_kg / (height_m ** 2)

def recommend_1km_time(bmi):
    if bmi < 18.5:
        return 360  # 6分钟
    elif 18.5 <= bmi < 24.9:
        return 300  # 5分钟
    elif 25 <= bmi < 29.9:
        return 360  # 6分钟
    else:
        return 420  # 7分钟

def countdown(seconds):
    print(f"\n倒计时开始：{seconds}秒（{seconds//60}分{seconds%60}秒）")
    while seconds:
        mins, secs = divmod(seconds, 60)
        timeformat = f"{mins:02}:{secs:02}"
        print(f"\r剩余时间：{timeformat}", end="")
        time.sleep(1)
        seconds -= 1
    print("\n时间到！加油完成1公里跑步！")

def main():
    try:
        height = float(input("请输入身高（cm）："))
        weight = float(input("请输入体重（kg）："))
    except ValueError:
        print("输入无效，请输入数字。")
        return

    bmi = calculate_bmi(height, weight)
    print(f"你的BMI指数为：{bmi:.2f}")
    run_time = recommend_1km_time(bmi)
    print(f"建议的1公里目标时间为：{run_time//60}分{run_time%60}秒")
    countdown(run_time)

if __name__ == "__main__":
    main()