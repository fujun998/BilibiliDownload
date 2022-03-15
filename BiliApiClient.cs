using System.Text.Json;
using System.Net.Http.Json;

static partial class BiliApis
{
    static partial class BiliApiClient<T>
    {
        static readonly HttpClient httpClient = new();

        static readonly JsonSerializerOptions serializerOptions = new()
        {
            //PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = new UnderscoreNamingPolicy()
        };

        static BiliApiClient()
        {
            httpClient.BaseAddress = new Uri("https://api.bilibili.com/");
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36 Edg/84.0.522.63");
            httpClient.DefaultRequestHeaders.Add("referer", "https://www.bilibili.com/");
        }

        public static async Task<T?> GetAsync(string requestUri)
        {
            try
            {
                using var resJson = JsonDocument.Parse(await httpClient.GetStreamAsync(requestUri));
                JsonElement resRoot = resJson.RootElement;
                int code = resRoot.GetProperty("code").GetInt32();
                if (code == 0)
                {
                    if (!resRoot.TryGetProperty("result", out JsonElement resElem)) resElem = resRoot.GetProperty("data");
                    return resElem.Deserialize<T>(serializerOptions);
                }
                else
                {
                    throw resJson.Deserialize<BiliException>(serializerOptions);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
