import time
import pygame

# 设置倒计时时长（秒/s）
countdown_seconds = 270

# 设置音乐文件路径
music_file = "your_music.mp3"

# 初始化pygame.mixer
pygame.mixer.init()

# 开始倒计时
for i in range(countdown_seconds, 0, -1):
    print(f"倒计时 {i} 秒...")
    time.sleep(1)

print("倒计时结束，播放音乐!")

# 播放音乐
pygame.mixer.music.load(music_file)
pygame.mixer.music.play()

# 让音乐播放完后程序终止
while pygame.
