using System.IO.Compression;

//BiliApis.SessData = args[1];
//Season season = await BiliApis.GetSeasonInfo(long.Parse(args[0]));
//Console.WriteLine(season.Title);
//Directory.SetCurrentDirectory("E:\\videos\\anime\\Love Live！虹咲學園學園偶像同好會 第二季");
DownloadOptions options = new()
{
    //Title = "Love Live！虹咲學園學園偶像同好會 第二季",
    Directory = "e:/videos",
    CodecId = 12,
    DownloadTypes = DownloadType.Video// | DownloadType.Audio
};
//await DownloadHelper.DownloadSeason(season, options);
BiliApis.SessData = "e60849e1%2C1658467696%2Cfd1e2%2A11";
var video = await BiliApis.GetVideoInfo(args[0]);
await DownloadHelper.DownloadVideo(video.BVId, video.CId, options);