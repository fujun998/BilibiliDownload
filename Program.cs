var ss = await BiliApiClient.GetSeasonInfo(40156);
Console.WriteLine(ss);
foreach (var ep in ss.Episodes)
{
    Console.WriteLine(ep);
}