var ss = await BiliApis.GetSeasonInfo(40156);
//Console.WriteLine(ss);
foreach (var ep in ss.Episodes)
{
    //Console.WriteLine(ep);
}

var vid = await BiliApis.GetVideoInfo("BV1rp4y1e745");
Console.WriteLine(vid);

var playurl = await BiliApis.GetPlayUrlAsync("BV1rp4y1e745", 244954665);
foreach (var dash in playurl.Dash.Video)
{
    //Console.WriteLine(dash);
}

var stream = await BiliHttpClient.GetStreamAsync(playurl.Dash.Video.Last().BaseUrl);
stream.CopyTo(File.OpenWrite("output/o.mp4"));