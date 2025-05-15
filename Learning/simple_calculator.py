# 简易四则运算计算器

def add(a, b):
    return a + b

def subtract(a, b):
    return a - b

def multiply(a, b):
    return a * b

def divide(a, b):
    if b == 0:
        return "错误：除数不能为0"
    return a / b

print("=== 简易计算器 ===")
print("支持运算：加 (+)、减 (-)、乘 (*)、除 (/)")

try:
    num1 = float(input("请输入第一个数字："))
    operator = input("请输入运算符 (+ - * /)：")
    num2 = float(input("请输入第二个数字："))

    if operator == "+":
        result = add(num1, num2)
    elif operator == "-":
        result = subtract(num1, num2)
    elif operator == "*":
        result = multiply(num1, num2)
    elif operator == "/":
        result = divide(num1, num2)
    else:
        result = "错误：不支持的运算符"

    print(f"结果：{result}")
except ValueError:
    print("输入错误：请输入有效的数字")
