using System.Text.Json.Serialization;

class BiliPlayUrl : BiliResponse
{
    public BiliVideoDashInfo Dash { get; set; }

    [JsonPropertyName("timelength")]
    public long TimeLength { get; set; }
    public BiliQuality[] AcceptQuality { get; set; }
    public BiliVideoFormat[] SupportFormats { get; set; }
}

class BiliVideoDashInfo
{
    public BiliDashUrl[] Video { get; set; }
    public BiliDashUrl[] Audio { get; set; }
}

class BiliDashUrl
{
    int Id { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string FrameRate { get; set; }
    
    [JsonPropertyName("codecid")]
    public int CodecId { get; set; }
    public string Codecs { get; set; }
    
    [JsonPropertyName("bandwidth")]
    public long BandWidth { get; set; }
    public string BaseUrl { get; set; }
}

class BiliVideoFormat
{
    public BiliQuality Quality { get; set; }
    public string NewDescription { get; set; }
    public string[] Codecs { get; set; }
}