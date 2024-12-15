using System.Net;
using VTools.Exceptions;

namespace VTools.Services.Exceptions;

public class JwtSecretNotConfiguredException : BaseException
{
    public JwtSecretNotConfiguredException(string message, Exception exception)
        : base(message, exception, HttpStatusCode.BadRequest)
    {
    }
    
    public JwtSecretNotConfiguredException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}