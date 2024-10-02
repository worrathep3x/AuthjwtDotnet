using Microsoft.AspNetCore.SignalR.Protocol;

namespace Application.Model;

public class Response<T>
{
    public bool Status { get; set; }
    public string? Message { get; set; }

    public T? Result { get; set; }

    private Response(T data)
    {
        this.Status = true;
        this.Result = data;
    }

    private Response(string message)
    {
        this.Status = false;
        this.Message = message;
    }

    public static Response<T> FailedResult(string message) => new Response<T>(message);
    public static Response<T> SuccessResult(T data) => new Response<T?>(data);


}
