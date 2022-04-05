using System.Text.Json.Serialization;

class Video : BiliResponse
{
    public string Title { get; set; }
    public string BVId { get; set; }
    public long CId { get; set; }
    public long Duration { get; set; }
    public VideoDimension Dimension { get; set; }
    public VideoPage[] Pages { get; set; }

    [JsonPropertyName("pic")]
    public string CoverUrl { get; set; }
    public SubtitleInfo Subtitle { get; set; }
}

class VideoPage
{
    public long CId { get; set; }
    public int Page { get; set; }
    public long Duration { get; set; }
    public VideoDimension Dimension { get; set; }
    public string Part { get; set; }
}

class VideoDimension
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int Rotate { get; set; }
}

class SubtitleInfo
{
    public Subtitle[] List { get; set; }
}

class Subtitle
{
    [JsonPropertyName("lan")]
    public string Language { get; set; }
    public string SubtitleUrl { get; set; }
}