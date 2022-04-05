[Serializable]
class BiliApiException : Exception
{
    public int Code { get; }
    public BiliApiException(int code, string? message) : base(message)
    {
        Code = code;
    }
    protected BiliApiException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}