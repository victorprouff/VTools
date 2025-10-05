using System.Text.Json.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace VTools.Services.DTOs;

public class WebDavPropfind
{
    [JsonIgnore]
    [XmlElement("href", Namespace = "DAV:")]
    public string? Href { get; set; }

    [JsonIgnore]
    [XmlElement("propstat", Namespace = "DAV:")]
    public WebDavStat? Stat { get; set; }

    public bool IsOk => (this?.Stat?.IsOk ?? false) && !string.IsNullOrWhiteSpace(this.Href);

    public bool IsDirectory => this?.Stat?.Prop?.ResourceType?.Collection != null;

    public string FullName => HttpUtility.UrlDecode(this.Href!);

    public string Basename => this.IsDirectory ? new DirectoryInfo(this.FullName).Name : Path.GetFileName(this.FullName);

    public string? Dirname => this.IsDirectory ? new DirectoryInfo(this.FullName).Parent!.FullName : Path.GetDirectoryName(this.FullName);
}