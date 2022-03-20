using System.Text.Json.Serialization;

abstract class BiliResponse
{
    public string RawJson{ get; set; }

    public override string ToString()
    {
        return RawJson;
    }
}

class BiliVideo : BiliResponse
{
    public string Title { get; set; }
    public string BVId { get; set; }
    public long Cid { get; set; }
    public long Duration { get; set; }
    public BiliVideoDimension Dimension { get; set; }
    public List<BiliVideoPage> Pages { get; } = new();
}

record struct BiliVideoPage(long Cid, int Page, long Duration, BiliVideoDimension Dimension, string Part);

record struct BiliVideoDimension(int Width, int Height, int Rotate);


class BiliSeason : BiliResponse
{

    public string Title { get; set; }
    public string Evaluate { get; set; }
    public List<BiliEpisode> Episodes { get; } = new();
}

record struct BiliEpisode(string Title, string BVId, long Cid, long Duration, BiliVideoDimension Dimension, string LongTitle);


class BiliPlayUrl : BiliResponse
{
    public BiliVideoDashInfo Dash { get; set; }
}

record struct BiliVideoDashInfo(BiliDashUrl[] Video, BiliDashUrl[] Audio);

record struct BiliDashUrl(int Id, int Width, int Height, string FrameRate, int Codecid, string Codecs, string BaseUrl);