static partial class BiliApiClient
{
    public static async Task<BiliSeason> GetSeasonInfo(long ssid)
    {
        return await BiliApiCaller<BiliSeason>.GetAsync($"/pgc/view/web/season?season_id={ssid}");
    }

    public static async Task<BiliVideo> GetVideoInfo(string bvid)
    {
        return await BiliApiCaller<BiliVideo>.GetAsync($"/x/web-interface/view?bvid={bvid}");
    }
}