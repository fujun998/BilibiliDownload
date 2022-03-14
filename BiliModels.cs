using System.Text.Json.Serialization;

record BiliSeason(BiliEpisode[] Episodes, string Title);

record BiliVideo(string Title, string BVId, long Cid, BiliVideoDimension Dimension, BiliVideoPage[] Pages);

record BiliVideoPage(long Cid, int Page, BiliVideoDimension Dimension, string Part);

record BiliEpisode(string Title, string BVId, long Cid, BiliVideoDimension Dimension, string LongTitle);

record struct BiliVideoDimension(int Width, int Height, int Rotate);

record BiliVideoDashInfo(BiliDashUrl[] Video, BiliDashUrl[] Audio);

record BiliDashUrl(int Id, string BaseUrl);