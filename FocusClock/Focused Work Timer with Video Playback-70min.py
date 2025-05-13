import time
import pygame
from pygame.locals import QUIT, KEYDOWN, K_ESCAPE

def play_video(video_file):
    pygame.init()
    pygame.display.set_caption("专注时钟")

    video = pygame.movie.Movie(video_file)
    video_screen = pygame.display.set_mode(video.get_size())
    video_screen.fill((255, 255, 255))
    video.play()

    clock = pygame.time.Clock()

    running = True
    while running:
        for event in pygame.event.get():
            if event.type == QUIT or (event.type == KEYDOWN and event.key == K_ESCAPE):
                video.stop()
                running = False

        video_screen.blit(video.get_surface(), (0, 0))
        pygame.display.flip()
        clock.tick(30)  # 设置视频帧率

    pygame.quit()

def focus_timer(minutes, video_file):
    seconds = minutes * 60
    for remaining_time in range(seconds, 0, -1):
        mins, secs = divmod(remaining_time, 60)
        timeformat = '{:02d}:{:02d}'.format(mins, secs)
        print(f"倒计时剩余时间: {timeformat}", end='\r')
        time.sleep(1)

    print("\n专注时间到！")
    play_video(video_file)

if __name__ == "__main__":
    try:
        minutes = int(input("请输入专注时间（分钟）：70"))
        video_file = input("请输入视频文件路径：")
        focus_timer(minutes, video_file)
    except ValueError:
        print("请输入一个有效的数字。")
