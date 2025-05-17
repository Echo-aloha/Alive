# 随机密码生成器

import random
import string

def generate_password(length):
    if length < 4:
        return "密码长度至少为4位"
    
    # 构建字符池：大写、小写、数字、符号
    all_chars = string.ascii_letters + string.digits + string.punctuation
    
    # 随机选择字符生成密码
    password = ''.join(random.choices(all_chars, k=length))
    
    return password

# 用户输入期望长度
try:
    length = int(input("请输入你希望的密码长度（建议8位以上）："))
    password = generate_password(length)
    print(f"生成的密码是：{password}")
except ValueError:
    print("请输入一个有效的整数。")
