using System.IO.Compression;

//BiliApis.SessData = args[1];
Season season = await BiliApis.GetSeasonInfo(long.Parse(args[0]));
//Console.WriteLine(season.Title);

DownloadOptions options = new()
{
    Directory = "output",
    CodecId = 12,
    DownloadTypes = DownloadType.Subtitle
};
BiliApis.SessData = "e60849e1%2C1658467696%2Cfd1e2%2A11";
await DownloadHelper.DownloadSeason(season, options);