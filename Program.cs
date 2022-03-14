var ss = await BiliApis.GetSeasonInfo(40156);
Console.WriteLine(ss);
foreach (var ep in ss.Episodes)
{
    Console.WriteLine(ep);
}
var vid = await BiliApis.GetVideoInfo("BV1ex411J7GE");
Console.WriteLine(vid);
foreach(var page in vid.Pages)
{
    Console.WriteLine(page);
}