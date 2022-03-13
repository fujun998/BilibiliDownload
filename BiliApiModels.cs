record BiliSeason(string Title, BiliVideo[] Episodes);

record BiliVideo(string BVId, long Cid);

record BiliVideoUrl(BiliVideoDashInfo Dash);

record BiliVideoDashInfo(BiliDashUrl[] video, BiliDashUrl[] audio); //临时对象，用于包含API返回值中的"dash"对象

record BiliDashUrl(int id, string BaseUrl);