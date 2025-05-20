# 单位换算器：厘米 <-> 英寸

def cm_to_inch(cm):
    return cm / 2.54

def inch_to_cm(inch):
    return inch * 2.54

print("=== 单位换算器 ===")
print("1: 厘米 -> 英寸")
print("2: 英寸 -> 厘米")

choice = input("请选择转换方向（1 或 2）：")

try:
    value = float(input("请输入要转换的数值："))

    if choice == "1":
        result = cm_to_inch(value)
        print(f"{value} 厘米 = {result:.2f} 英寸")
    elif choice == "2":
        result = inch_to_cm(value)
        print(f"{value} 英寸 = {result:.2f} 厘米")
    else:
        print("无效选择，请输入 1 或 2。")
except ValueError:
    print("请输入有效的数字。")
