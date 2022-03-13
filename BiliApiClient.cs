using System.Net.Http.Json;

static class BiliApiClient
{
    static readonly HttpClient httpClient = new();

    static class BiliApiCaller<T>
    {
        public static async Task<T> GetAsync(string requestUri)
        {

            var res = await httpClient.GetFromJsonAsync<BiliApiRawResponse<T>>(requestUri);
            return res.Data ?? res.Result;
        }
    }

    static BiliApiClient()
    {
        httpClient.BaseAddress = new Uri("https://api.bilibili.com/");
        httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36 Edg/84.0.522.63");
        httpClient.DefaultRequestHeaders.Add("referer", "https://www.bilibili.com/");
    }

    public static async Task<BiliSeason> GetSeasonInfo(long ssid)
    {
        return await BiliApiCaller<BiliSeason>.GetAsync($"/pgc/view/web/season?season_id={ssid}");
    }

    public static async Task<BiliVideo> GetVideoInfo(string bvid)
    {
        return await BiliApiCaller<BiliVideo>.GetAsync($"/x/web-interface/view?bvid={bvid}");
    }
}