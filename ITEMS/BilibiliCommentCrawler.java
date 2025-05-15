// 文件名：BilibiliCommentCrawler.java
// 脚本名：BilibiliCommentCrawler

import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import com.google.gson.*;

import java.io.IOException;

public class BilibiliCommentCrawler {

    private static final String VIDEO_INFO_API = "https://api.bilibili.com/x/web-interface/view?bvid=";
    private static final String COMMENT_API = "https://api.bilibili.com/x/v2/reply?type=1&oid=";

    private final OkHttpClient client = new OkHttpClient();
    private final Gson gson = new Gson();

    /**
     * 根据 BV 号获取 AV 号（aid），用于后续获取评论
     */
    public long getAidFromBvid(String bvid) throws IOException {
        String url = VIDEO_INFO_API + bvid;
        Request request = new Request.Builder()
                .url(url)
                .header("User-Agent", "Mozilla/5.0")
                .build();
        try (Response response = client.newCall(request).execute()) {
            if (!response.isSuccessful()) throw new IOException("Failed to get aid");
            JsonObject json = gson.fromJson(response.body().string(), JsonObject.class);
            return json.getAsJsonObject("data").get("aid").getAsLong();
        }
    }

    /**
     * 获取第一页评论并输出
     */
    public void fetchAndPrintComments(long aid) throws IOException {
        String url = COMMENT_API + aid + "&sort=2&page=1"; // sort=2 热度排序, page=1 第一页
        Request request = new Request.Builder()
                .url(url)
                .header("User-Agent", "Mozilla/5.0")
                .build();
        try (Response response = client.newCall(request).execute()) {
            if (!response.isSuccessful()) throw new IOException("Failed to fetch comments");
            JsonObject json = gson.fromJson(response.body().string(), JsonObject.class);
            JsonArray replies = json.getAsJsonObject("data").getAsJsonArray("replies");
            System.out.println("=== 热门评论（前 20 条） ===");
            for (JsonElement elem : replies) {
                JsonObject reply = elem.getAsJsonObject();
                String uname = reply.getAsJsonObject("member").get("uname").getAsString();
                String message = reply.getAsJsonObject("content").get("message").getAsString();
                int likes = reply.get("like").getAsInt();
                System.out.println("- 用户: " + uname);
                System.out.println("  内容: " + message);
                System.out.println("  点赞: " + likes);
                System.out.println();
            }
        }
    }

    public static void main(String[] args) {
        if (args.length != 1) {
            System
