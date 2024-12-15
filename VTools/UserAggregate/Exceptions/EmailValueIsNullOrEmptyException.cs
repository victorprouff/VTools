using System.Net;
using VTools.Exceptions;

namespace VTools.UserAggregate.Exceptions;

public class EmailValueIsNullOrEmptyException : BaseException
{
    public EmailValueIsNullOrEmptyException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}