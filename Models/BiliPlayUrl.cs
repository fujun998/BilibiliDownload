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
    [JsonPropertyName("id")]
    public BiliQuality Quality { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string FrameRate { get; set; }
    
    [JsonPropertyName("codecid")]
    public BiliCodec Codec { get; set; }
    //public string Codecs { get; set; }
    public long Bandwidth { get; set; }
    public string BaseUrl { get; set; }
}

class BiliVideoFormat
{
    public BiliQuality Quality { get; set; }
    public string NewDescription { get; set; }
    public string[] Codecs { get; set; }
}

enum BiliCodec
{
    AVC = 7,
    HEVC = 12,
    AV1 = 13
}

enum BiliQuality
{
    _240P = 6,
    _360P = 16,
    _480P = 32,
    _720P = 64,
    _720P60 = 74,
    _1080P = 80,
    _1080PH = 112,
    _1080P60 = 116,
    _4K = 120,
    _HDR = 125,
    _DolbyVision = 512,
    _8K = 127
}