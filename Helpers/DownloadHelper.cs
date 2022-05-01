using System.IO.Compression;
using System.Text;
using System.Text.Json;

static class DownloadHelper
{
    static Aria2NET.Aria2NetClient ariaClient = new("http://localhost:6800/jsonrpc");

    static string FixPath(string filename)
    {
        string res = filename.Replace('<', '＜')
        .Replace('>', '＞')
        .Replace(':', '：')
        .Replace('|', '｜')
        .Replace('?', '？');
        return res;
    }

    public static async Task Download(string url, string path, bool isLarge = false)
    {
        path = FixPath(path);
        try
        {
            if (!isLarge)
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

    public static async Task DownloadSubtitles(SubtitleInfo subtitles, string directory, string title)
    {
        JsonElement root;
        StringBuilder sb = new();
        TimeSpan from, to;
        string content;
        int i = 0;
        foreach (var sub in subtitles.List)
        {
            i = 0;
            root = JsonDocument.Parse(await BiliHttpClient.GetStreamAsync(sub.SubtitleUrl)).RootElement;
            sb.Clear();
            foreach (var line in root.GetProperty("body").EnumerateArray())
            {
                from = TimeSpan.FromSeconds(line.GetProperty("from").GetDouble());
                to = TimeSpan.FromSeconds(line.GetProperty("to").GetDouble());
                content = line.GetProperty("content").GetString();
                sb.AppendLine(i++.ToString());
                sb.AppendLine($"{from.ToString(@"hh\:mm\:ss\.fff")} --> {to.ToString(@"hh\:mm\:ss\.fff")}");
                sb.AppendLine(content);
                sb.AppendLine();
            }
            File.WriteAllText(FixPath(Path.Join(directory, $"{title}.{sub.Language}.srt")), sb.ToString());
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
                await DownloadSubtitles(video.Subtitle, options.Directory, title);
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
            string title = string.IsNullOrEmpty(options.Title) ? season.Title : options.Title;

            File.WriteAllText(Path.Join(options.Directory, $"{title}.json"), season.RawJson);

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
                         Title = ep.GetFullTitle(title)
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