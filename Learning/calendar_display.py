# 月份日历显示器

import calendar

try:
    year = int(input("请输入年份（例如 2025）："))
    month = int(input("请输入月份（1-12）："))

    # 打印日历
    print("\n📅 本月日历如下：\n")
    print(calendar.month(year, month))
except ValueError:
    print("请输入有效的整数年份和月份。")
except IndexError:
    print("月份应在 1 到 12 之间。")
