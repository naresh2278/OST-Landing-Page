
namespace Dell.B2BOnlineTools.Common
{
    public partial class Constants
    {
        public const string JsonContentType = "application/json";
        public const string HtmlContentType = "text/html";
        public const string XmlContentType = "application/xml";
        public const string JsonPContentType = "application/javascript";
    }

    public enum HttpMethod
    {
        Get,
        Post,
        Patch,
        Put,
        Delete
    }

    public enum HttpContentType
    {
        Json,
        Xml,
        Html
    }
}
