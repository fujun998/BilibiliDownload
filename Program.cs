
BiliApis.SessData = "";
Season season = await BiliApis.GetSeasonInfo(41492);
Console.WriteLine(season.Title);
DownloadOptions options = new() { Directory = "E:/videos/anime/Love Live！虹咲學園學園偶像同好會 第二季", CodecId = 12, DownloadTypes = ~(DownloadType.Video|DownloadType.Audio) };
await DownloadHelper.DownloadSeason(season, options);