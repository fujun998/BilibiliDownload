using System.Text.Json.Serialization;

record BiliSeason(BiliEpisode[] Episodes, string Title);

record BiliVideo(string Title, string BVId, long Cid, BiliVideoPage[] Pages);

record BiliVideoPage(long Cid);

record BiliEpisode(string Title, string BVId, long Cid, string LongTitle);

record BiliVideoDashInfo(BiliDashUrl[] Video, BiliDashUrl[] Audio);

record BiliDashUrl(int Id, string BaseUrl);