namespace VTools.UserAggregate.Models;

public record CreateUserCommand(Email Email, string username, string Password);