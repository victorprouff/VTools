namespace VTools.Services.DTOs;

public class FileResult : IDisposable
{
    public int StatusCode { get; set; }

    public Stream? Content { get; set; }

    public string? Etag { get; set; }

    public string? ContentType { get; set; }

    public bool IsSuccess => this.StatusCode == 200 && this.Content != null;

    public void Dispose()
    {
        this.Content?.Dispose();
    }
}