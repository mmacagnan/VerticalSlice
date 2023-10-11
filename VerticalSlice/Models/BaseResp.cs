namespace VerticalSlice.Models;


public class BaseResp<T>
{
    public Status Status { get; set; }
    public T Result { get; set; }
}

public class BaseResp
{
    public BaseResp(int code, string message, string token)
    {
        Status = new Status
        {
            Code = code,
            Message = message,
            Token = token
        };
    }

    public BaseResp()
    {
    }

    public Status Status { get; set; }
}

public class Status
{
    public int Code { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
}