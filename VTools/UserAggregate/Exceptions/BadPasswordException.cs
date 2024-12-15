using System.Net;
using VTools.Exceptions;

namespace VTools.UserAggregate.Exceptions;

public class BadPasswordException : BaseException
{
    public BadPasswordException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}