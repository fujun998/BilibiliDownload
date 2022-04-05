using System.Text.Json.Serialization;

class PlayUrl : BiliResponse
{
    public DashInfo Dash { get; set; }

    [JsonPropertyName("timelength")]
    public long TimeLength { get; set; }
    public Quality[] AcceptQuality { get; set; }
    public VideoFormat[] SupportFormats { get; set; }
}

class DashInfo
{
    public DashUrl[] Video { get; set; }
    public DashUrl[] Audio { get; set; }
    public DashInfo? Dolby { get; set; }
}

class DashUrl : IComparable<DashUrl>
{
    [JsonPropertyName("id")]
    public Quality Quality { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string FrameRate { get; set; }

    [JsonPropertyName("codecid")]
    public int CodecId { get; set; }
    public string Codecs { get; set; }
    public long Bandwidth { get; set; }
    public string BaseUrl { get; set; }
    public int CompareTo(DashUrl? other) => other is null ? 1 : Quality.CompareTo(other.Quality);
}

class VideoFormat
{
    public Quality Quality { get; set; }
    public string NewDescription { get; set; }
    public string[] Codecs { get; set; }
}

enum Quality
{
    //Video
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
    _DolbyVision = 126,
    _8K = 127,

    //Audio
    _192kbps = 30280,
    _132kbps = 30232,
    _64kbps = 30216,
    _DolbyAtoms = 30250
}