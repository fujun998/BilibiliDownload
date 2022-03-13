using System.Text.Json.Serialization;

record BiliSeason(BiliEpisode[] Episodes)
{
    [JsonPropertyName("season_title")]
    public string? Title{ get; init; }
}

record BiliVideo(string Title, string BVId, long Cid);

record BiliEpisode(string Title, string BVId, long Cid): BiliVideo(Title, BVId, Cid)
{
    [JsonPropertyName("long_title")]
    public string? LongTitle { get; init; }
}

record BiliVideoDashInfo(BiliDashUrl[] Video, BiliDashUrl[] Audio); //临时对象，用于包含API返回值中的"dash"对象

record BiliDashUrl(int Id, string BaseUrl);

record BiliApiRawResponse<T>(int Code, string Message, T Result, T Data);