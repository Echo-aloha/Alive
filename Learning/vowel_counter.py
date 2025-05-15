# 统计元音字母个数

def count_vowels(text):
    vowels = "aeiou"
    count = 0
    for char in text.lower():
        if char in vowels:
            count += 1
    return count

# 获取用户输入
sentence = input("请输入一段英文文本：")

# 统计并输出结果
vowel_count = count_vowels(sentence)
print(f"元音字母总数为：{vowel_count}")
