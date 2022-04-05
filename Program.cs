
BiliApis.SessData = "e60849e1%2C1658467696%2Cfd1e2%2A11";
Season season = await BiliApis.GetSeasonInfo(41492);
Console.WriteLine(season.Title);
DownloadOptions options = new() { Directory = "E:/videos/anime/Love Live！虹咲學園學園偶像同好會 第二季", Codec = "hev", DownloadTypes = ~(DownloadType.Video|DownloadType.Audio) };
await DownloadHelper.DownloadSeason(season, options);