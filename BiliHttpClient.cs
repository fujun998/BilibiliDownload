using System.Text.Json;
using System.Net.Http.Json;

static class BiliHttpClient
{
    static readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://api.bilibili.com/"),
        DefaultRequestHeaders ={
            {"user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36 Edg/84.0.522.63"},
            {"referer", "https://www.bilibili.com/"}
        }
    };

    static readonly JsonSerializerOptions serializerOptions = new()
    {
        //PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = new UnderscoreNamingPolicy()
    };

    public static async Task<T> GetFromJsonAsync<T>(string requestUri) where T : BiliResponse
    {
        try
        {
            using var resJson = JsonDocument.Parse(await httpClient.GetStreamAsync(requestUri));

            if (resJson.RootElement.TryGetProperty("data", out JsonElement resElem) ||
                resJson.RootElement.TryGetProperty("result", out resElem))
            {
                T res = resElem.Deserialize<T>(serializerOptions);
                res.RawJson = resElem.GetRawText();
                return res;
            }
            else
            {
                throw resJson.Deserialize<BiliApiException>(serializerOptions);
            }
        }
        catch
        {
            throw;
        }
    }

    public static async Task<Stream> GetStreamAsync(string requestUri)
    {
        try
        {
            return await httpClient.GetStreamAsync(requestUri);
        }
        catch
        {
            throw;
        }
    }
}