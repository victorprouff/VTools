using System.Xml.Serialization;

namespace VTools.Services.DTOs;

public class WebDavResourceType
{
    [XmlElement("collection", Namespace = "DAV:")]
    public string? Collection { get; set; }
}