using System.Runtime.Serialization;

namespace VerticalSlice.Exceptions;

public class HttpStatusCodeException
    : Exception
{
    public HttpStatusCodeException()
    {
    }

    public HttpStatusCodeException(int statusCode, string message) 
        : base(message)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCodeException(int statusCode, string message, Exception innerException)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    protected HttpStatusCodeException(int statusCode, SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; set; }
}