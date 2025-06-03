# 英文单词统计器

def analyze_text(text):
    words = text.split()
    total_words = len(words)
    
    if total_words == 0:
        return "没有检测到任何单词。"

    total_length = sum(len(word) for word in words)
    longest_word = max(words, key=len)

    print(f"单词总数：{total_words}")
    print(f"平均单词长度：{total_length / total_words:.2f}")
    print(f"最长的单词：{longest_word}")

# 用户输入英文段落
text = input("请输入一段英文文本：\n")
analyze_text(text)
