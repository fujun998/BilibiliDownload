class BiliSeason : BiliResponse
{

    public string Title { get; set; }
    public string Evaluate { get; set; }
    public BiliEpisode[] Episodes { get; set; }
}

class BiliEpisode
{
    string Title { get; set; }
    string BVId { get; set; }
    long Cid { get; set; }
    long Duration { get; set; }
    BiliVideoDimension Dimension { get; set; }
    string LongTitle { get; set; }
}