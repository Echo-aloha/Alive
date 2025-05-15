# 密码强度检查器

import string

def check_strength(password):
    length_ok = len(password) >= 8
    has_digit = any(char.isdigit() for char in password)
    has_letter = any(char.isalpha() for char in password)
    has_symbol = any(char in string.punctuation for char in password)

    strength = 0
    if length_ok:
        strength += 1
    if has_digit:
        strength += 1
    if has_letter:
        strength += 1
    if has_symbol:
        strength += 1

    return strength

# 用户输入
password = input("请输入密码进行强度检测：")

# 分析密码
strength = check_strength(password)

# 输出结果
if strength == 4:
    print("密码强度：强")
elif strength == 3:
    print("密码强度：中")
elif strength == 2:
    print("密码强度：弱")
else:
    print("密码强度：极弱，建议修改密码")
