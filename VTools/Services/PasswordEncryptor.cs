using VTools.Services.Interfaces;
using BC = BCrypt.Net;

namespace VTools.Services;

public class PasswordEncryptor : IPasswordEncryptor
{
    public string GenerateHash(string password, string salt) => BC.BCrypt.HashPassword(password + salt);

    public string GenerateSalt() => BC.BCrypt.GenerateSalt();

    public bool VerifyPassword(string password, string salt, string passwordHash) => BC.BCrypt.Verify(password + salt, passwordHash);
}