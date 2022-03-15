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

    public static async Task<BiliVideoPlayUrl> GetPlayUrlAsync(string bvid,long cid)
    {
        return await BiliApiClient<BiliVideoPlayUrl>.GetAsync($"/x/player/playurl?bvid={bvid}&cid={cid}&qn=127&fnval=4048&fnver=0&fourk=1");
    }
}
