# 输出九九乘法表

for i in range(1, 10):  # 外层循环：控制行数，从1到9
    for j in range(1, i + 1):  # 内层循环：每行输出的内容数量
        print(f"{j}×{i}={i * j}", end="\t")  # 使用f字符串格式化输出
    print()  # 换行
