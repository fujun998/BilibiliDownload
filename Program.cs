//using System.Net.Http;
using HttpClient client = new();
Console.WriteLine(await client.GetStringAsync("https://api.bilibili.com/x/player/playurl?avid=969628065&cid=244954665&qn=127&fnval=80&fnver=0&fourk=1"));