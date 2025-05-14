# BMI 计算器

# 输入身高和体重
height = float(input("请输入你的身高（单位：米）："))
weight = float(input("请输入你的体重（单位：千克）："))

# 计算 BMI
bmi = weight / (height ** 2)

# 输出 BMI 值
print(f"你的BMI是：{bmi:.2f}")

# 判断 BMI 所在范围
if bmi < 18.5:
    print("体重偏轻")
elif 18.5 <= bmi < 24.9:
    print("体重正常")
elif 24.9 <= bmi < 29.9:
    print("体重偏重")
else:
    print("肥胖")
