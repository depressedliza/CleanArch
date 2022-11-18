namespace Application.Common.Exceptions;

public class HttpException : Exception
{
    public HttpException(string message) : base(message) { }
}
