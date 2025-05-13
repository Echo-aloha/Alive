# 计算学生的平均成绩并判断是否及格

# 输入成绩
math_score = float(input("请输入数学成绩："))
english_score = float(input("请输入英语成绩："))
science_score = float(input("请输入科学成绩："))

# 计算平均分
average_score = (math_score + english_score + science_score) / 3

# 输出平均分
print("平均成绩为：", average_score)

# 判断是否及格
if average_score >= 60:
    print("恭喜，你及格了！")
else:
    print("很遗憾，你没有及格。")
