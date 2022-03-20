static class BiliApis
{
    public static async Task<BiliSeason> GetSeasonInfo(long ssid)
    {
        return await BiliHttpClient.GetFromJsonAsync<BiliSeason>($"/pgc/view/web/season?season_id={ssid}");
    }

    public static async Task<BiliVideo> GetVideoInfo(string bvid)
    {
        return await BiliHttpClient.GetFromJsonAsync<BiliVideo>($"/x/web-interface/view?bvid={bvid}");
    }

    public static async Task<BiliVideo> GetVideoInfo(long avid)
    {
        return await BiliHttpClient.GetFromJsonAsync<BiliVideo>($"/x/web-interface/view?aid={avid}");
    }

    public static async Task<BiliPlayUrl> GetPlayUrlAsync(string bvid, long cid)
    {
        return await BiliHttpClient.GetFromJsonAsync<BiliPlayUrl>($"/x/player/playurl?bvid={bvid}&cid={cid}&qn=127&fnval=4048&fnver=0&fourk=1");
    }
}
