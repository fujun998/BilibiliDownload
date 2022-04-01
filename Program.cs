async void Download(string uri, string path)
{
    var srcStream = await BiliHttpClient.GetStreamAsync(uri);
    await srcStream.CopyToAsync(File.OpenWrite(path));
}

async void DownloadVideo(BiliPlayUrl playUrl, BiliQuality quality, BiliCodec codec)
{
    Download(
        playUrl.Dash.Video.Where(dash => dash.Codec == codec && dash.Quality == quality).Single().BaseUrl,
        "output/video.mp4"
        );

    Download(
        playUrl.Dash.Audio[0].BaseUrl,
        "output/audio.mp4"
    );
}