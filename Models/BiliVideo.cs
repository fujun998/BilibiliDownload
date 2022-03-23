class BiliVideo : BiliResponse
{
    public string Title { get; set; }
    public string BVId { get; set; }
    public long Cid { get; set; }
    public long Duration { get; set; }
    public BiliVideoDimension Dimension { get; set; }
    public BiliVideoPage[] Pages { get; set; }
}

class BiliVideoPage
{
    public long Cid { get; set; }
    public int Page { get; set; }
    public long Duration { get; set; }
    public BiliVideoDimension Dimension { get; set; }
    public string Part { get; set; }
}

class BiliVideoDimension
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int Rotate { get; set; }
}