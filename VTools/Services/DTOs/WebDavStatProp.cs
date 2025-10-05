using System.Xml.Serialization;

namespace VTools.Services.DTOs;

public class WebDavStatProp
{
    [XmlElement("getlastmodified", Namespace = "DAV:")]
    public string? LastModified { get; set; }

    [XmlElement("getetag", Namespace = "DAV:")]
    public string? ETag { get; set; }

    [XmlElement("getcontenttype", Namespace = "DAV:")]
    public string? ContentType { get; set; }

    [XmlElement("getcontentlength", Namespace = "DAV:")]
    public long? ContentLength { get; set; }

    [XmlElement("resourcetype", Namespace = "DAV:")]
    public WebDavResourceType? ResourceType { get; set; }
}