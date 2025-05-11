import time

# 推荐番茄钟数映射
pomodoro_map = {
    "低年级": {
        "语文": 1,
        "数学": 1,
        "英语": 1,
        "科学": 1,
        "其他": 1
    },
    "中年级": {
        "语文": 2,
        "数学": 2,
        "英语": 1,
        "科学": 2,
        "其他": 1
    },
    "高年级": {
        "语文": 2,
        "数学": 2,
        "英语": 2,
        "科学": 2,
        "其他": 1
    }
}

def countdown(minutes, label="学习"):
    seconds = minutes * 60
    print(f"\n--- 开始 {label}：{minutes} 分钟 ---")
    while seconds:
        mins, secs = divmod(seconds, 60)
        print(f"\r剩余时间 {label}：{mins:02d}:{secs:02d}", end="")
        time.sleep(1)
        seconds -= 1
    print(f"\n--- {label}结束！---")

def main():
    print("番茄钟学习计划")
    stage = input("请输入年级阶段（低年级/中年级/高年级）：").strip()
    subject = input("请输入科目（语文/数学/英语/科学/其他）：").strip()

    if stage not in pomodoro_map or subject not in pomodoro_map[stage]:
        print("输入无效，请重新运行并输入正确的年级或科目。")
        return

    num_pomodoros = pomodoro_map[stage][subject]
    print(f"\n推荐学习番茄钟周期：{num_pomodoros} 个（每个 25 分钟，休息 5 分钟）")

    for i in range(1, num_pomodoros + 1):
        countdown(25, f"第 {i} 个番茄钟")
        if i < num_pomodoros:
            countdown(5, "休息")

    print("\n全部番茄钟学习完成！记得奖励自己一下！")

if __name__ == "__main__":
    main()