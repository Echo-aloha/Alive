# 数字排序器

# 用户输入一组数字，英文逗号分隔
input_str = input("请输入一组数字，用英文逗号分隔（例如 5,3,9,1）：")

try:
    # 转换为数字列表
    numbers = [float(x.strip()) for x in input_str.split(",")]
    
    # 排序
    sorted_numbers = sorted(numbers)
    
    # 输出结果
    print("排序后的数字为：")
    print(sorted_numbers)
except ValueError:
    print("输入有误，请确保只输入数字并用英文逗号分隔。")
