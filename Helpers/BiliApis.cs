static class BiliApis
{
    public static string SessData { get; set; } = "";

    public static async Task<Season> GetSeasonInfo(long ssid)
    {
        return await BiliHttpClient.GetFromJsonAsync<Season>($"/pgc/view/web/season?season_id={ssid}");
    }

    public static async Task<Video> GetVideoInfo(string bvid)
    {
        return await BiliHttpClient.GetFromJsonAsync<Video>($"/x/web-interface/view?bvid={bvid}");
    }

    public static async Task<Video> GetVideoInfo(long avid)
    {
        return await BiliHttpClient.GetFromJsonAsync<Video>($"/x/web-interface/view?aid={avid}");
    }

    public static async Task<PlayUrl> GetPlayUrlAsync(string bvid, long cid)
    {
        return await BiliHttpClient.GetFromJsonAsync<PlayUrl>($"/x/player/playurl?bvid={bvid}&cid={cid}&qn=127&fnval=4048&fnver=0&fourk=1",$"SESSDATA={SessData}");
        /*if(res.Dash.Dolby is not null)
        {
            res.Dash.Audio = res.Dash.Audio.Append(res.Dash.Dolby.Audio.Single()).ToArray();
        }*/
    }
}
