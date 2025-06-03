# 简易计算器

def calculate(num1, num2, operator):
    if operator == '+':
        return num1 + num2
    elif operator == '-':
        return num1 - num2
    elif operator == '*':
        return num1 * num2
    elif operator == '/':
        if num2 == 0:
            return "错误：除数不能为零"
        return num1 / num2
    else:
        return "不支持的运算符"

try:
    num1 = float(input("请输入第一个数字："))
    operator = input("请输入运算符（+ - * /）：")
    num2 = float(input("请输入第二个数字："))

    result = calculate(num1, num2, operator)
    print(f"结果是：{result}")
except ValueError:
    print("请输入有效的数字。")
