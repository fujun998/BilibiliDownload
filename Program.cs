async void Download(string uri,string path)
{
    var srcStream = await BiliHttpClient.GetStreamAsync(uri);
    await srcStream.CopyToAsync(File.OpenWrite(path));
}