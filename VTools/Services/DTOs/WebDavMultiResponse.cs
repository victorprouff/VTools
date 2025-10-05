using System.Xml.Serialization;

namespace VTools.Services.DTOs;

[XmlRoot(ElementName = "multistatus", Namespace = "DAV:")]
public class WebDavMultiResponse<TResponse> where TResponse : class
{
    [XmlIgnore]
    public int StatusCode { get; set; }

    [XmlElement("response", Namespace = "DAV:")]
    public TResponse[]? Responses { get; set; } = [];

    [XmlIgnore]
    public bool IsSuccess => this.StatusCode == 207 && this.Responses != null;
}