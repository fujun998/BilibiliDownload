using System.Text.Json;
using System.Net.Http.Json;

static partial class BiliApiClient
{
    static class BiliApiHelper<T>
    {
        static readonly HttpClient httpClient = new();

        static readonly JsonSerializerOptions serializerOptions = new()
        {
            PropertyNamingPolicy = new UnderscoreNamingPolicy()
        };

        static BiliApiHelper()
        {
            httpClient.BaseAddress = new Uri("https://api.bilibili.com/");
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36 Edg/84.0.522.63");
            httpClient.DefaultRequestHeaders.Add("referer", "https://www.bilibili.com/");
        }

        public static async Task<T> GetAsync(string requestUri)
        {
            var res = await httpClient.GetFromJsonAsync<BiliRawResponse<T>>(requestUri, serializerOptions);
            return res.Data ?? res.Result;
        }

        class UnderscoreNamingPolicy : JsonNamingPolicy
        {
            public override string ConvertName(string name)
            {
                return name;
            }
        }
    }
}