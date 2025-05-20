# 文件行数统计器

def count_lines(filename):
    try:
        with open(filename, 'r', encoding='utf-8') as f:
            lines = f.readlines()
            return len(lines)
    except FileNotFoundError:
        return "文件未找到，请确认文件路径是否正确。"
    except Exception as e:
        return f"发生错误：{e}"

# 用户输入文件路径
filename = input("请输入要统计行数的文本文件路径：")

# 调用函数并输出结果
result = count_lines(filename)
print(f"行数统计结果：{result}")
