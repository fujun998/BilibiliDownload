using System.Text.Json.Serialization;

class Season : BiliResponse
{

    public string Title { get; set; }
    public string Evaluate { get; set; }
    public Episode[] Episodes { get; set; }

    [JsonPropertyName("cover")]
    public string CoverUrl { get; set; }
}

class Episode
{
    public string Title { get; set; }
    public string BVId { get; set; }
    public long CId { get; set; }
    public long Duration { get; set; }
    public VideoDimension Dimension { get; set; }
    public string LongTitle { get; set; }

    public string GetFullTitle(string seasonTitle) => $"{seasonTitle} {(int.TryParse(Title, out int n) ? n.ToString("D2") : Title)} - {LongTitle}";
}