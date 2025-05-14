import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import com.google.gson.Gson;
import com.google.gson.JsonObject;

import java.io.IOException;

public class BilibiliCrawler {

    private static final String API_URL = "https://api.bilibili.com/x/web-interface/view?bvid=";

    private final OkHttpClient httpClient;
    private final Gson gson;

    public BilibiliCrawler() {
        this.httpClient = new OkHttpClient();
        this.gson = new Gson();
    }

    /**
     * 根据 BV 号抓取视频信息
     * @param bvid 视频的 BV 号，例如 "BV1xx411c7mD"
     * @return VideoInfo 对象，包含标题、UP 主、播放量、弹幕数等
     * @throws IOException 网络请求出错时抛出
     */
    public VideoInfo fetchVideoInfo(String bvid) throws IOException {
        String url = API_URL + bvid;
        Request request = new Request.Builder()
                .url(url)
                .header("User-Agent", "Mozilla/5.0 (Java)")
                .build();

        try (Response response = httpClient.newCall(request).execute()) {
            if (!response.isSuccessful()) {
                throw new IOException("Unexpected HTTP code: " + response.code());
            }
            String body = response.body().string();
            JsonObject root = gson.fromJson(body, JsonObject.class);
            JsonObject data = root.getAsJsonObject("data");

            VideoInfo info = new VideoInfo();
            info.setBvid(bvid);
            info.setTitle(data.get("title").getAsString());
            info.setAuthor(data.getAsJsonObject("owner").get("name").getAsString());
            info.setViewCount(data.getAsJsonObject("stat").get("view").getAsLong());
            info.setDanmakuCount(data.getAsJsonObject("stat").get("danmaku").getAsInt());
            info.setLikeCount(data.getAsJsonObject("stat").get("like").getAsInt());
            info.setCoinCount(data.getAsJsonObject("stat").get("coin").getAsInt());
            info.setFavoriteCount(data.getAsJsonObject("stat").get("favorite").getAsInt());
            info.setShareCount(data.getAsJsonObject("stat").get("share").getAsInt());
            return info;
        }
    }

    public static void main(String[] args) {
        if (args.length != 1) {
            System.err.println("Usage: java BilibiliCrawler <BV号>");
            System.exit(1);
        }

        String bvid = args[0];
        BilibiliCrawler crawler = new BilibiliCrawler();
        try {
            VideoInfo info = crawler.fetchVideoInfo(bvid);
            System.out.println("=== Bilibili Video Info ===");
            System.out.println("BV号       : " + info.getBvid());
            System.out.println("标题       : " + info.getTitle());
            System.out.println("UP 主      : " + info.getAuthor());
            System.out.println("播放量     : " + info.getViewCount());
            System.out.println("弹幕数     : " + info.getDanmakuCount());
            System.out.println("点赞数     : " + info.getLikeCount());
            System.out.println("投币数     : " + info.getCoinCount());
            System.out.println("收藏数     : " + info.getFavoriteCount());
            System.out.println("分享数     : " + info.getShareCount());
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}

/**
 * 简单的数据模型类 VideoInfo.java
 */
class VideoInfo {
    private String bvid;
    private String title;
    private String author;
    private long viewCount;
    private int danmakuCount;
    private int likeCount;
    private int coinCount;
    private int favoriteCount;
    private int shareCount;

    // -- 下面省略所有 getter 和 setter，请根据 IDE 自动生成 --

    public String getBvid() { return bvid; }
    public void setBvid(String bvid) { this.bvid = bvid; }
    public String getTitle() { return title; }
    public void setTitle(String title) { this.title = title; }
    public String getAuthor() { return author; }
    public void setAuthor(String author) { this.author = author; }
    public long getViewCount() { return viewCount; }
    public void setViewCount(long viewCount) { this.viewCount = viewCount; }
    public int getDanmakuCount() { return danmakuCount; }
    public void setDanmakuCount(int danmakuCount) { this.danmakuCount = danmakuCount; }
    public int getLikeCount() { return likeCount; }
    public void setLikeCount(int likeCount) { this.likeCount = likeCount; }
    public int getCoinCount() { return coinCount; }
    public void setCoinCount(int coinCount) { this.coinCount = coinCount; }
    public int getFavoriteCount() { return favoriteCount; }
    public void setFavoriteCount(int favoriteCount) { this.favoriteCount = favoriteCount; }
    public int getShareCount() { return shareCount; }
    public void setShareCount(int shareCount) { this.shareCount = shareCount; }
}
