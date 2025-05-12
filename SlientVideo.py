import cv2

# 读取视频文件
video_path = 'your_video_path_here.mp4'  # 替换为你的视频路径
video = cv2.VideoCapture(video_path)

# 检查视频是否成功打开
if not video.isOpened():
    print("无法成功打开视频文件")
    exit()

# 获取视频的宽度和高度
width = int(video.get(cv2.CAP_PROP_FRAME_WIDTH))
height = int(video.get(cv2.CAP_PROP_FRAME_HEIGHT))

# 创建视频窗口
cv2.namedWindow('Video', cv2.WINDOW_NORMAL)

while True:
    ret, frame = video.read()

    # 如果视频读取结束，退出循环
    if not ret:
        break

    # 显示视频帧
    cv2.imshow('Video', frame)

    # 按下 'q' 键退出播放
    if cv2.waitKey(25) & 0xFF == ord('q'):
        break

# 释放资源并关闭窗口
video.release()
cv2.destroyAllWindows()
