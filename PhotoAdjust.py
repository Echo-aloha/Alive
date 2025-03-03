from PIL import Image
import os
import io

def resize_image(input_path, output_path, target_size=None, max_file_size_kb=None):
    """
    调整图片尺寸和文件大小
    :param input_path: 输入图片路径
    :param output_path: 输出图片路径
    :param target_size: (宽, 高) 元组，若为 None，则保持原尺寸
    :param max_file_size_kb: 目标文件大小（KB），若为 None，则不压缩
    """
    # 打开图片
    img = Image.open(input_path)
    
    # 调整尺寸
    if target_size:
        img = img.resize(target_size, Image.ANTIALIAS)

    # 若不限制文件大小，直接保存
    if max_file_size_kb is None:
        img.save(output_path)
        return
    
    # 逐步调整图片质量，直到符合文件大小要求
    quality = 95  # 初始质量
    while quality > 10:
        img_bytes = io.BytesIO()
        img.save(img_bytes, format='JPEG', quality=quality)
        file_size_kb = len(img_bytes.getvalue()) / 1024  # 计算文件大小（KB）
        
        if file_size_kb <= max_file_size_kb:
            with open(output_path, "wb") as f:
                f.write(img_bytes.getvalue())
            return
        
        quality -= 5  # 逐步降低质量
        
    print("无法将图片压缩到目标大小，请尝试调整参数。")

# 示例用法
input_image = "input.jpg"  # 输入图片路径
output_image = "output.jpg"  # 输出图片路径
target_size = (800, 600)  # 目标尺寸 (宽, 高)，可根据需要修改
max_file_size_kb = 200  # 目标文件大小 (KB)

resize_image(input_image, output_image, target_size, max_file_size_kb)