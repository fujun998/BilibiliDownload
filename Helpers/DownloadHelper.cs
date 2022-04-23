using System.IO.Compression;
using System.Text.Json;

static class DownloadHelper
{
    static Aria2NET.Aria2NetClient ariaClient = new("http://localhost:6800/jsonrpc");

    public static async Task Download(string url, string path, bool isLarge = false)
    {
        try
        {
            if (true)
            {
                var srcStream = await BiliHttpClient.GetStreamAsync(url);
                await srcStream.CopyToAsync(File.Create(path));
            }
            else
            {
                await ariaClient.AddUri(
                    new List<string> { url },
                    new Dictionary<string, object>
                    {
                        { "dir", Path.GetDirectoryName(Path.GetFullPath(path)) },
                        { "out", Path.GetFileName(path) },
                        { "referer", "https://www.bilibili.com/" }
                    });
            }
        }
        catch
        {
            throw;
        }
    }

    public static async Task DownloadDash(PlayUrl playUrl, string title, DownloadOptions options)
    {
        string path = Path.Join(options.Directory, title);

        try
        {
            if (options.DownloadTypes.HasFlag(DownloadType.Audio))
            {
                if (playUrl.Dash.Dolby is not null)
                {
                    await Download(playUrl.Dash.Dolby.Audio[0].BaseUrl, $"{path}.audio.mp4", true);
                }
                else
                {
                    await Download(playUrl.Dash.Audio.Max().BaseUrl, $"{path}.audio.mp4", true);
                }
            }

            if (options.DownloadTypes.HasFlag(DownloadType.Video))
            {
                await Download(
                    playUrl.Dash.Video
                        .Where(dash => dash.Quality <= options.Quality && dash.CodecId <= options.CodecId)
                        .First().BaseUrl,
                    $"{path}.video.mp4",
                    true
                );
            }
        }
        catch
        {
            throw;
        }
    }

    public static async Task DownloadVideo(string bvid, long cid, DownloadOptions options)
    {
        try
        {
            Video video = await BiliApis.GetVideoInfo(bvid);

            string title = string.IsNullOrEmpty(options.Title) ? video.Title : options.Title;

            string basePath = Path.Join(options.Directory, title);

            if (options.DownloadTypes.HasFlag(DownloadType.Cover))
            {
                await Download(video.CoverUrl, basePath + ".{video.CoverUrl.Split('.').Last()}");
            }

            if (options.DownloadTypes.HasFlag(DownloadType.Dm))
            {
                await Download($"http://api.bilibili.com/x/v1/dm/list.so?oid={cid}", basePath + ".dm.xml");
            }

            if (options.DownloadTypes.HasFlag(DownloadType.Subtitle))
            {
                foreach (var sub in video.Subtitle.List)
                {
                    await Download(sub.SubtitleUrl, $"{basePath}.{sub.Language}.{sub.SubtitleUrl.Split('.').Last()}");
                }
            }

            await DownloadDash(await BiliApis.GetPlayUrlAsync(bvid, cid), title, options);
        }
        catch
        {
            throw;
        }
    }

    public static async Task DownloadSeason(Season season, DownloadOptions options)
    {
        try
        {
            File.WriteAllText(Path.Join(options.Directory, $"seasoninfo.json"), season.RawJson);

            if (options.DownloadTypes.HasFlag(DownloadType.Cover))
            {
                await Download(season.CoverUrl, Path.Join(options.Directory, $"cover.{season.CoverUrl.Split('.').Last()}"));
            }

            Task.WaitAll(
                (from ep in season.Episodes
                 select DownloadVideo(
                     ep.BVId, ep.CId,
                     new DownloadOptions()
                     {
                         Quality = options.Quality,
                         CodecId = options.CodecId,
                         Directory = options.Directory,
                         DownloadTypes = options.DownloadTypes & (~DownloadType.Cover),
                         Title = string.IsNullOrEmpty(options.Title) ? ep.GetFullTitle(season.Title) : ep.GetFullTitle(options.Title)
                     })
                ).ToArray()
            );
        }
        catch
        {
            throw;
        }
    }
}

class DownloadOptions
{
    public string? Title { get; set; }
    public Quality Quality { get; set; } = Quality._8K;
    public int CodecId { get; set; } = 12;
    public string Directory { get; set; } = "output";
    public DownloadType DownloadTypes { get; set; } = DownloadType.Video | DownloadType.Audio;
}

[Flags]
enum DownloadType
{
    None = 0,
    Video = 1,
    Audio = 2,
    Dm = 4,
    Subtitle = 8,
    Cover = 16,
}