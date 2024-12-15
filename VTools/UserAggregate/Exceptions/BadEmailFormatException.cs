using System.Net;
using VTools.Exceptions;

namespace VTools.UserAggregate.Exceptions;

public class BadEmailFormatException : BaseException
{
    public BadEmailFormatException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}