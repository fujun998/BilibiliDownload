abstract class BiliResponse
{
    public string RawJson{ get; set; }

    public override string ToString()
    {
        return RawJson;
    }
}
