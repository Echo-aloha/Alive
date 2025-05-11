import requests
import json
import xml.etree.ElementTree as ET
import openpyxl
import os


HEADERS = {'User-Agent': 'Mozilla/5.0'}


def get_video_info(bvid):
    url = f'https://api.bilibili.com/x/web-interface/view?bvid={bvid}'
    res = requests.get(url, headers=HEADERS)
    if res.status_code == 200:
        data = res.json()
        if data['code'] == 0:
            info = data['data']
            return {
                'title': info['title'],
                'views': info['stat']['view'],
                'up_name': info['owner']['name'],
                'up_mid': info['owner']['mid'],
                'aid': info['aid'],
                'cid': info['cid']
            }
    return None


def get_comments(aid, page=1, sort=2):
    url = 'https://api.bilibili.com/x/v2/reply'
    params = {'type': 1, 'oid': aid, 'pn': page, 'ps': 20, 'sort': sort}
    res = requests.get(url, headers=HEADERS, params=params)
    comments = []
    if res.status_code == 200:
        replies = res.json()['data']['replies']
        for r in replies:
            comments.append({
                'user': r['member']['uname'],
                'message': r['content']['message']
            })
    return comments


def get_danmaku(cid):
    url = f'https://comment.bilibili.com/{cid}.xml'
    res = requests.get(url)
    danmakus = []
    if res.status_code == 200:
        root = ET.fromstring(res.content)
        danmakus = [d.text for d in root.findall('d')]
    return danmakus


def search_video(keyword, page=1):
    url = 'https://api.bilibili.com/x/web-interface/search/type'
    params = {'search_type': 'video', 'keyword': keyword, 'page': page}
    res = requests.get(url, headers=HEADERS, params=params)
    results = []
    if res.status_code == 200:
        for r in res.json()['data']['result']:
            results.append({
                'title': r['title'],
                'bvid': r['bvid'],
                'author': r['author']
            })
    return results


def save_json(data, filename):
    with open(filename, 'w', encoding='utf-8') as f:
        json.dump(data, f, ensure_ascii=False, indent=2)
    print(f"保存为JSON：{filename}")


def save_excel(data, filename, headers=None):
    wb = openpyxl.Workbook()
    ws = wb.active
    if headers:
        ws.append(headers)
    for item in data:
        if isinstance(item, dict):
            ws.append(list(item.values()))
        else:
            ws.append([item])
    wb.save(filename)
    print(f"保存为Excel：{filename}")


def main():
    print("B站爬虫工具：")
    print("1. 查询视频信息")
    print("2. 获取评论")
    print("3. 获取弹幕")
    print("4. 搜索视频")
    choice = input("请选择功能（1-4）：")

    if choice == '1':
        bvid = input("请输入BV号：")
        info = get_video_info(bvid)
        if info:
            print(json.dumps(info, indent=2, ensure_ascii=False))
            save_json(info, 'video_info.json')
        else:
            print("获取失败")

    elif choice == '2':
        bvid = input("请输入BV号：")
        info = get_video_info(bvid)
        if not info:
            print("视频信息获取失败")
            return
        comments = get_comments(info['aid'])
        for c in comments:
            print(f"{c['user']}: {c['message']}")
        save_excel(comments, 'comments.xlsx', headers=['用户名', '评论内容'])

    elif choice == '3':
        bvid = input("请输入BV号：")
        info = get_video_info(bvid)
        if not info:
            print("视频信息获取失败")
            return
        danmakus = get_danmaku(info['cid'])
        for d in danmakus[:10]:
            print(d)
        save_excel(danmakus, 'danmaku.xlsx', headers=['弹幕内容'])

    elif choice == '4':
        keyword = input("请输入搜索关键词：")
        results = search_video(keyword)
        for r in results[:5]:
            print(f"{r['title']} - {r['bvid']} - {r['author']}")
        save_excel(results, 'search_results.xlsx', headers=['标题', 'BV号', 'UP主'])

    else:
        print("无效选择")


if __name__ == '__main__':
    main()