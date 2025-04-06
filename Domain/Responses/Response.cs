using System.Net;

namespace Domain.Responses;

public class Response<T>
{
    public bool IsSucces { get; set; }
    public T? Data { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public Response(T? data)
    {
        Data = data;
        IsSucces = true;
        StatusCode = 200;
        Message = null;

    }

    public Response(HttpStatusCode statusCode, string message)
    {
        Data = default;
        IsSucces = false;
        StatusCode = (int)statusCode;
        Message = message;
    }

}
