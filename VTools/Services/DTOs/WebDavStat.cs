using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace VTools.Services.DTOs;

public class WebDavStat
{
    [JsonIgnore]
    [XmlElement("prop", Namespace = "DAV:")]
    public required WebDavStatProp Prop { get; set; }

    [JsonIgnore]
    [XmlElement("status", Namespace = "DAV:")]
    public required string Status { get; set; }

    public bool IsOk => this.Status == "HTTP/1.1 200 OK";
}