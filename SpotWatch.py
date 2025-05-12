import time

class Stopwatch:
    def __init__(self):
        self.start_time = None
        self.end_time = None
        self.elapsed_time = 0
        self.running = False
        self.lap_times = []

    def start(self):
        if not self.running:
            self.start_time = time.time() - self.elapsed_time
            self.running = True
            print("秒表计时开始")

    def stop(self):
        if self.running:
            self.end_time = time.time()
            self.elapsed_time = self.end_time - self.start_time
            self.running = False
            print("秒表停止，总时间为:", self.get_elapsed_time())

    def lap(self):
        if self.running:
            lap_time = time.time() - self.start_time
            self.lap_times.append(lap_time)
            print("计次:", len(self.lap_times), "时间:", self.format_time(lap_time))

    def reset(self):
        self.start_time = None
        self.end_time = None
        self.elapsed_time = 0
        self.running = False
        self.lap_times = []
        print("秒表重置")

    def get_elapsed_time(self):
        if self.running:
            self.elapsed_time = time.time() - self.start_time
        return self.format_time(self.elapsed_time)

    def format_time(self, elapsed):
        mins = int(elapsed // 60)
        sec = int(elapsed % 60)
        msec = int((elapsed - int(elapsed)) * 100)
        return f"{mins:02d}:{sec:02d}:{msec:02d}"

# 示例用法
stopwatch = Stopwatch()

stopwatch.start()
time.sleep(2)
stopwatch.lap()
time.sleep(1)
stopwatch.lap()
stopwatch.stop()

print("总时间:", stopwatch.get_elapsed_time())

stopwatch.reset()
