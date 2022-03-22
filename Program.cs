var ss = await BiliApis.GetSeasonInfo(40156);
//Console.WriteLine(ss.RawJson);
foreach (var ep in ss.Episodes)
{
    //Console.WriteLine(ep);
}

//var vid = await BiliApis.GetVideoInfo("BV1rp4y1e745");
var vid = await BiliApis.GetVideoInfo("BV1XR4y1F7ZQ");

//var playurl = await BiliApis.GetPlayUrlAsync("BV1rp4y1e745", 244954665);
var playurl = await BiliApis.GetPlayUrlAsync("BV1XR4y1F7ZQ", 553981907);
//Console.WriteLine(playurl.RawJson);
//File.WriteAllText("Sample-AV1.json", playurl.RawJson);
foreach (var dash in playurl.Dash.Video)
{
    //Console.WriteLine(dash);
}
Console.WriteLine(playurl.Dash.Video[4].BaseUrl);
Console.WriteLine(playurl.Dash.Video[4].Bandwidth * playurl.Timelength / 8000);
Console.WriteLine((await (await BiliHttpClient.GetAsync(playurl.Dash.Video[4].BaseUrl)).Content.ReadAsByteArrayAsync()).Length);
//srcStream.CopyTo(File.OpenWrite("output/sample.mp4"));