import datetime

def get_quote():
    # 在这里可以添加你的名言列表
    quotes = [
       
    ]
    # 获取今天的日期，用于选择名言
    today = datetime.datetime.now().date()
    # 使用日期作为索引来选择名言
    index = today.day % len(quotes)
    return quotes[index]

def main():
    today = datetime.datetime.now().date()
    print("今天是：", today.strftime("%Y-%m-%d"))
    print("名言：", get_quote())

if __name__ == "__main__":
    main()
