using System.Net;

namespace VTools.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message, Exception exception) : base(message, exception, HttpStatusCode.NotFound)
    {
    }
    
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }
}