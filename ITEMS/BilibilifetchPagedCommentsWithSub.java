public void fetchSubComments(long aid, long rootRpid) throws IOException {
    int page = 1;
    int totalReplies = 0;

    while (true) {
        String url = String.format("https://api.bilibili.com/x/v2/reply/reply?type=1&oid=%d&root=%d&page=%d", aid, rootRpid, page);
        Request request = new Request.Builder().url(url).header("User-Agent", "Mozilla/5.0").build();

        try (Response response = client.newCall(request).execute()) {
            if (!response.isSuccessful()) {
                System.out.println("  [子评论第 " + page + " 页获取失败]");
                break;
            }

            JsonObject json = gson.fromJson(response.body().string(), JsonObject.class);
            JsonArray replies = json.getAsJsonObject("data").getAsJsonArray("replies");
            if (replies == null || replies.size() == 0) {
                if (page == 1) System.out.println("  [无子评论]");
                break;
            }

            for (JsonElement elem : replies) {
                JsonObject reply = elem.getAsJsonObject();
                String uname = reply.getAsJsonObject("member").get("uname").getAsString();
                String message = reply.getAsJsonObject("content").get("message").getAsString();
                int likes = reply.get("like").getAsInt();
                System.out.println("    ↳ 回复用户: " + uname);
                System.out.println("      内容: " + message);
                System.out.println("      点赞: " + likes);
                totalReplies++;
            }

            page++; // 下一页
            Thread.sleep(1000); // 子评论限速
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    if (totalReplies > 0) {
        System.out.println("    [共抓取 " + totalReplies + " 条子评论]");
    }
}

###########################################################

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

                fetchSubComments(aid, rpid);
                System.out.println();
            }

            Thread.sleep(1000); // 每页主评论请求限速
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }
}
