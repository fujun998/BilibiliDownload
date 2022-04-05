using System.Text.Json;

static class DownloadHelper
{
    public static async Task Download(string url, string path)
    {
        try
        {
            Console.WriteLine(url);
            var srcStream = await BiliHttpClient.GetStreamAsync(url);
            await srcStream.CopyToAsync(File.OpenWrite(path));
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
            if (options.DownloadTypes.HasFlag(DownloadType.Video))
            {
                await Download(
                    playUrl.Dash.Video
                        .Where(dash => dash.Quality <= options.Quality && dash.CodecId <= options.CodecId)
                        .First().BaseUrl,
                    $"{path}.video.mp4"
                );
            }

            if (options.DownloadTypes.HasFlag(DownloadType.Audio))
            {
                if (playUrl.Dash.Dolby is not null)
                {
                    await Download(playUrl.Dash.Dolby.Audio[0].BaseUrl, $"{path}.audio.mp4");
                }
                else
                {
                    await Download(playUrl.Dash.Audio.Max().BaseUrl, $"{path}.audio.mp4");
                }
            }
        }
        catch
        {
            throw;
        }
    }

    public static async Task DownloadVideo(string bvid, long cid, DownloadOptions options, string? title = null)
    {
        try
        {
            Video video = await BiliApis.GetVideoInfo(bvid);

            if (string.IsNullOrEmpty(title))
            {
                title = video.Title;
            }

            string basePath = Path.Join(options.Directory, title);

            if (options.DownloadTypes.HasFlag(DownloadType.Cover))
            {
                await Download(video.CoverUrl, basePath + ".{video.CoverUrl.Split('.').Last()}");
            }

            if (options.DownloadTypes.HasFlag(DownloadType.Dm))
            {
                await Download($"http://api.bilibili.com/x/v1/dm/list.so?oid={cid}", basePath + ".danmuku.xml");
            }

            if (options.DownloadTypes.HasFlag(DownloadType.Subtitle))
            {
                foreach (var sub in video.Subtitle.List.Where(sub => sub.Language.StartsWith(Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)))
                {
                    await Download(sub.SubtitleUrl, basePath + $".{sub.Language}.{sub.SubtitleUrl.Split('.').Last()}");
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
            //if (options.DownloadTypes.HasFlag(DownloadType.Info))
            {
                File.WriteAllText(Path.Join(options.Directory, $"{season.Title}.json"), season.ToString());
            }

            if (options.DownloadTypes.HasFlag(DownloadType.Cover))
            {
                await Download(season.CoverUrl, Path.Join(options.Directory, $"{season.Title}.{season.CoverUrl.Split('.').Last()}"));
            }

            Task.WaitAll(
                (from ep in season.Episodes
                 select DownloadVideo(
                     ep.BVId, ep.CId,
                     new DownloadOptions() { Quality = options.Quality, CodecId = options.CodecId, Directory = options.Directory, DownloadTypes = options.DownloadTypes & (~DownloadType.Cover) },
                     ep.GetFullTitle(season.Title))
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