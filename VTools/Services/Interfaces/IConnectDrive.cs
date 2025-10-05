using System.Net;
using System.Web;
using System.Xml.Serialization;

namespace VTools.Services.Interfaces;

public interface IConnectDrive
{
    Task<DTOs.WebDavMultiResponse<DTOs.WebDavPropfind>> ListDirectory(string path, int maxDepth = 1);
    Task<DTOs.FileResult> GetFile(string filePath);
    Task<FileState> HasFileChanged(string filePath, string etag);
    string? GetRealativeItemPath(string? input);
}

public class WebDavClient : IConnectDrive
{
    private HttpClient _client;

    private Uri? _baseUrl;

    public WebDavClient(HttpClient httpClient)
    {
        _client = httpClient;
        _baseUrl = _client.BaseAddress;
    }

    public async Task<DTOs.WebDavMultiResponse<DTOs.WebDavPropfind>> ListDirectory(string path, int maxDepth = 1)
    {
        this.EnsurePathSyntax(path);
        path = path.Trim().TrimEnd('/');

        var payload = @"
                <?xml version=""1.0""?>
                <d:propfind xmlns:d=""DAV:"">
                    <d:prop>
                        <d:resourcetype/>
                        <d:getlastmodified/>
                        <d:getcontentlength/>
                        <d:getcontenttype/>
                        <d:resourcetype/>
                        <d:getetag/>
                    </d:prop>
                </d:propfind>
            ";

        using var msg = new HttpRequestMessage(new HttpMethod("PROPFIND"), path);
        msg.Headers.Add("Depth", $"{maxDepth}");
        msg.Headers.Accept.Clear();
        msg.Headers.Accept.ParseAdd("application/xml; charset=utf-8");
        msg.Content = new StringContent(payload.Trim());

        HttpResponseMessage? response = null;

        try
        {
            response = await this._client.SendAsync(msg);
        }
        catch (HttpRequestException)
        {
            return new DTOs.WebDavMultiResponse<DTOs.WebDavPropfind>()
            {
                Responses = null,
                StatusCode = -1
            };
        }

        if (response.StatusCode != HttpStatusCode.MultiStatus)
        {
            return new DTOs.WebDavMultiResponse<DTOs.WebDavPropfind>()
            {
                Responses = null,
                StatusCode = (int)response.StatusCode
            };
        }

        using var stream = await response.Content.ReadAsStreamAsync();
        var text = await response.Content.ReadAsStringAsync();

        var serializer = new XmlSerializer(typeof(DTOs.WebDavMultiResponse<DTOs.WebDavPropfind>));
        var result = (DTOs.WebDavMultiResponse<DTOs.WebDavPropfind>?)serializer.Deserialize(stream);

        if (result != null)
        {
            result.StatusCode = (int)response.StatusCode;
        }

        if (result != null && result.IsSuccess && result.Responses != null)
        {
            foreach (var item in result.Responses)
            {
                item.Href = this.GetRealativeItemPath(item.Href);
            }

            result.Responses = result.Responses.Where(i => i.Href != path).ToArray();
        }

        return result!;
    }

    public async Task<DTOs.FileResult> GetFile(string filePath)
    {
        this.EnsurePathSyntax(filePath);
        filePath = filePath.Trim().TrimEnd('/');

        using var response = await this._client.GetAsync(filePath);
        response.Headers.TryGetValues("ETag", out var etags);
        response.Headers.TryGetValues("Content-Type", out var contentTypes);

        var result = new DTOs.FileResult()
        {
            StatusCode = (int)response.StatusCode,
            Etag = etags?.FirstOrDefault(),
            ContentType = contentTypes?.FirstOrDefault()
        };

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var memory = new MemoryStream();
            using var stream = await response.Content.ReadAsStreamAsync();
            await stream.CopyToAsync(memory);
            memory.Position = 0;
            result.Content = memory;
        }

        return result;
    }

    public async Task<FileState> HasFileChanged(string filePath, string etag)
    {
        this.EnsurePathSyntax(filePath);
        filePath = filePath.Trim().TrimEnd('/');

        var request = new HttpRequestMessage(HttpMethod.Head, filePath);
        request.Headers.Add("If-None-Match", etag);

        var response = await this._client.SendAsync(request);

        int[] validResponseCodes = [304, 412, 200, 404];
        if (!validResponseCodes.Contains((int)response.StatusCode))
        {
            throw new InvalidDataException("WebDAV response contains unexpected status code");
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return FileState.NotFound;
        }

        return response.StatusCode == HttpStatusCode.OK ? FileState.Changed : FileState.Unchanged;
    }

    public string? GetRealativeItemPath(string? input)
    {
        if (input == null || this._baseUrl == null || string.IsNullOrWhiteSpace(this._baseUrl.AbsolutePath))
        {
            return input;
        }

        var basePath = $"/{this._baseUrl.AbsolutePath.Trim('/', ' ')}/";
        // if (!input.StartsWith(basePath))
        // {
        //     throw new ArgumentException("Given path is not starting with base path");
        // }

        return ("./" + HttpUtility.UrlDecode(input.Substring(basePath.Length))).TrimEnd('/');
    }

    protected void EnsurePathSyntax(string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("path must not be empty");
        }

        if (!path.StartsWith("./") && path != ".")
        {
            throw new ArgumentException("path must start with './'");
        }

        if (path.Contains("//"))
        {
            throw new ArgumentException("path must not contain '//'");
        }

        if (path.Contains("/../") || path.EndsWith("..") || path.EndsWith("../"))
        {
            throw new ArgumentException("path must not contain relative path components like '../");
        }

        if (path.Contains("\\"))
        {
            throw new ArgumentException("path must not contain '\\'");
        }
    }
}