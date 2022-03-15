using System.Text.Json.Serialization;

record BiliVideo(string Title, string BVId, long Cid, long Duration, BiliVideoDimension Dimension, BiliVideoPage[] Pages);

record BiliVideoPage(long Cid, int Page, long Duration, BiliVideoDimension Dimension, string Part);

record struct BiliVideoDimension(int Width, int Height, int Rotate);


record BiliSeason(BiliEpisode[] Episodes, string Title, string Evaluate);

record BiliEpisode(string Title, string BVId, long Cid, long Duration, BiliVideoDimension Dimension, string LongTitle);


record BiliVideoPlayUrl(BiliVideoDashInfo Dash);

record BiliVideoDashInfo(BiliDashUrl[] Video, BiliDashUrl[] Audio);

record BiliDashUrl(int Id, int Width, int Height, string FrameRate, int Codecid, string Codecs, string BaseUrl);