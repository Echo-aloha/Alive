import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import com.google.gson.*;

import java.io.IOException;

public class BilibiliCommentWithSubReplyCrawler {

    private static final String VIDEO_INFO_API = "https://api.bilibili.com/x/web-interface/view?bvid=";
    private static final String COMMENT_API = "https://api.bilibili.com/x/v2/reply?type=1&oid=";
    private static final String SUB_REPLY_API = "https://api.bilibili.com/x/v2/reply/reply?type=1&oid=%d&root=%d&page=1";

    private final OkHttpClient client = new OkHttpClient();
    private final Gson gson = new Gson();

    public long getAidFromBvid(String bvid) throws IOException {
        String url = VIDEO_INFO_API + bvid;
        Request request = new Request.Builder().url(url).header("User-Agent", "Mozilla/5.0").build();
        try (Response response = client.newCall(request).execute()) {
            if (!response.isSuccessful()) throw new IOException("无法获取 aid");
            JsonObject json = gson.fromJson(response.body().string(), JsonObject.class);
            return json.getAsJsonObject("data").get("aid").getAsLong();
        }
    }

    public void fetchPagedCommentsWithSub(long aid, int pages, int sortType) throws IOException {
        for (int page = 1; page <= pages; page++) {
            String url = COMMENT_API + aid + "&sort=" + sortType + "&page=" + page;
            Request request = new Request.Builder().url(url).header("User-Agent", "Mozilla/5.0").build();
            try (Response response = client.newCall(request).execute()) {
                if (!response.isSuccessful()) {
                    System.err.println("第 " + page + " 页请求失败，跳过");
                    continue;
                }

                JsonObject json = gson.fromJson(response.body().string(), JsonObject.class);
                JsonArray replies = json.getAsJsonObject("data").getAsJsonArray("replies");
                if (replies == null || replies.size() == 0) {
                    System.out.println("第 " + page + " 页无评论，结束");
                    break;
                }

                System.out.println("=== 第 " + page + " 页评论 ===");
                for (JsonElement elem : replies) {
                    JsonObject reply = elem.getAsJsonObject();
                    String uname = reply.getAsJsonObject("member").get("uname").getAsString();
                    String message = reply.getAsJsonObject("content").get("message").getAsString();
                    int likes = reply.get("like").getAsInt();
                    long rpid = reply.get("rpid").getAsLong();

                    System.out.println("- 主评论用户: " + uname);
                    System.out.println("  内容: " + message);
                    System.out.println("  点赞: " + likes);

                    // 获取子评论（楼中楼）
                    fetchSubComments(aid, rpid);
                    System.out.println();
                }
            }
        }
    }

    public void fetchSubComments(long aid, long rootRpid) throws IOException {
        String url = String.format(SUB_REPLY_API, aid, rootRpid);
        Request request = new Request.Builder().url(url).header("User-Agent", "Mozilla/5.0").build();
        try (Response response = client.newCall(request).execute()) {
            if (!response.isSuccessful()) {
                System.out.println("  [子评论获取失败]");
                return;
            }

            JsonObject json = gson.fromJson(response.body().string(), JsonObject.class);
            JsonArray replies = json.getAsJsonObject("data").getAsJsonArray("replies");
            if (replies == null || replies.size() == 0) {
                System.out.println("  [无子评论]");
                return;
            }

            for (JsonElement elem : replies) {
                JsonObject reply = elem.getAsJsonObject();
                String uname = reply.getAsJsonObject("member").get("uname").getAsString();
                String message = reply.getAsJsonObject("content").get("message").getAsString();
                int likes = reply.get("like").getAsInt();
                System.out.println("    ↳ 回复用户: " + uname);
                System.out.println("      内容: " + message);
                System.out.println("      点赞: " + likes);
            }
        }
    }

    public static void main(String[] args) {
        if (args.length < 1 || args.length > 3) {
            System.err.println("用法: java BilibiliCommentWithSubReplyCrawler <BV号> [页数] [排序方式]");
            System.err.println("排序方式: 2=热度排序（默认），0=时间排序");
            System.exit(1);
        }

        String bvid = args[0];
        int pages = args.length >= 2 ? Integer.parseInt(args[1]) : 1;
        int sort = args.length == 3 ? Integer.parseInt(args[2]) : 2;

        BilibiliCommentWithSubReplyCrawler crawler = new BilibiliCommentWithSubReplyCrawler();
        try {
            long aid = crawler.getAidFromBvid(bvid);
            crawler.fetchPagedCommentsWithSub(aid, pages, sort);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
