# 列表统计函数：最大值、最小值、平均值

def calculate_stats(numbers):
    max_num = max(numbers)
    min_num = min(numbers)
    average = sum(numbers) / len(numbers)
    return max_num, min_num, average

# 输入数字，用英文逗号分隔
input_str = input("请输入一组数字（用英文逗号分隔，例如 12, 5, 8）：")

# 字符串分割并转换为浮点数列表
number_list = [float(x.strip()) for x in input_str.split(",")]

# 调用函数计算结果
max_value, min_value, avg_value = calculate_stats(number_list)

# 输出结果
print(f"最大值：{max_value}")
print(f"最小值：{min_value}")
print(f"平均值：{avg_value:.2f}")
