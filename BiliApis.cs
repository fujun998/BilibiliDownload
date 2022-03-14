static partial class BiliApis
{
    public static async Task<BiliSeason> GetSeasonInfo(long ssid)
    {
        return await BiliApiClient<BiliSeason>.GetAsync($"/pgc/view/web/season?season_id={ssid}");
    }

    public static async Task<BiliVideo> GetVideoInfo(string bvid)
    {
        return await BiliApiClient<BiliVideo>.GetAsync($"/x/web-interface/view?bvid={bvid}");
    }
}