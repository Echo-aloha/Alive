# 统计字母出现次数

text = input("请输入一段英文文本：")
text = text.lower()  # 转换为小写，忽略大小写

letter_count = {}  # 创建空字典用于统计

for char in text:
    if char.isalpha():  # 只统计字母
        if char in letter_count:
            letter_count[char] += 1
        else:
            letter_count[char] = 1

# 输出结果
for letter, count in letter_count.items():
    print(f"{letter} : {count}")
