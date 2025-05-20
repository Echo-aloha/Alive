# 每日心情记录器

from datetime import datetime

def log_mood():
    today = datetime.now().strftime("%Y-%m-%d")
    mood = input("你今天的心情如何？（开心 / 一般 / 不开心 等）：")
    note = input("今天想记录的一句话：")

    entry = f"日期：{today}\n心情：{mood}\n记录：{note}\n{'-'*30}\n"

    with open("mood_log.txt", "a", encoding="utf-8") as f:
        f.write(entry)

    print("✅ 今天的心情已记录！")

log_mood()
