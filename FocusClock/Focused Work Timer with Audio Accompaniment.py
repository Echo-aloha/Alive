import time
import pygame

def play_music():
    pygame.mixer.init()
    pygame.mixer.music.load("your_music_file.mp3")  # 替换成你自己的音乐文件路径
    pygame.mixer.music.play()

def stop_music():
    pygame.mixer.music.stop()

def focus_timer(minutes):
    seconds = minutes * 60
    for remaining_time in range(seconds, 0, -1):
        mins, secs = divmod(remaining_time, 60)
        timeformat = '{:02d}:{:02d}'.format(mins, secs)
        print(f"倒计时剩余时间: {timeformat}", end='\r')
        time.sleep(1)

    stop_music()
    print("\n专注时间到！")

if __name__ == "__main__":
    try:
        minutes = int(input("请输入专注时间（分钟）："))
        music_choice = input("是否播放音乐？（y/n）: ").lower()

        if music_choice == 'y':
            play_music()

        focus_timer(minutes)
    except ValueError:
        print("请输入一个有效的数字。")
    finally:
        stop_music()
