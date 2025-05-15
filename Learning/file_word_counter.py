# 文件读取与统计行数、字数

# 打开文件（假设文件名为 sample.txt）
filename = "sample.txt"

try:
    with open(filename, "r", encoding="utf-8") as file:
        lines = file.readlines()  # 读取所有行，返回一个列表
        total_lines = len(lines)  # 行数统计
        total_words = 0

        for line in lines:
            words = line.split()  # 默认按空格分词
            total_words += len(words)

    print(f"文件共有 {total_lines} 行")
    print(f"文件共有 {total_words} 个词")

except FileNotFoundError:
    print("文件未找到，请确认文件名是否正确。")
