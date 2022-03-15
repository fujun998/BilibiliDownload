using System.Text.Json;
using System.Net.Http.Json;

static partial class BiliApis
{
    static partial class BiliApiClient<T>
    {
        static readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://api.bilibili.com/"),
            DefaultRequestHeaders={
                {"user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36 Edg/84.0.522.63"},
                {"referer", "https://www.bilibili.com/"}
            }
        };

        static readonly JsonSerializerOptions serializerOptions = new()
        {
            //PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = new UnderscoreNamingPolicy()
        };

        record BiliApiResponse<TResult>(int Code, T? Data, T? Result, string? Message);
        //使用Data还是Result应根据具体API的返回值而定

        public static async Task<T> GetAsync(string requestUri)
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<BiliApiResponse<T>>(requestUri, serializerOptions);
                if (res.Data is not null)
                {
                    return res.Data;
                }
                else if (res.Result is not null)
                {
                    return res.Result;
                }
                else
                {
                    throw new BiliApiException(res.Code, res.Message);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
