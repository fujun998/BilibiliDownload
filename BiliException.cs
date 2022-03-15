[System.Serializable]
class BiliException : Exception
{
    public int Code{ get; }
    public BiliException(int code, string? message) : base(message)
    {
        Code = code;
    }
    protected BiliException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}