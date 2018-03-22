using Dell.B2BOnlineTools.Common.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dell.B2BOnlineTools.Common.Models.QMsg
{
    public class Url
    {
        [Required]
        public string Endpoint { get; set; } = string.Empty;
        public bool IsEndPointEncrypted { get; set; } = false;
        public string Type { get; set; } = Constants.QMsg.Url.Type.WebApi;
        public string Method { get; set; } = Constants.QMsg.Url.Method.GET;
        public string DataType { get; set; } = Constants.QMsg.Url.DataType.JSON;
        public Dictionary<string, string> Headers { get; set; } = null;
        public bool IsHeadersEncrypted { get; set; } = false;
        public Credential Credential { get; set; } = null;
        public bool IsCredentialEncrypted { get; set; } = true;
    }    

    public class Ado
    {
        [Required]
        public string Type { get; set; }
        public string ConnectionString { get; set; }
        public string CommandType { get; set; }
        public string CommandText { get; set; }
        public string Execute { get; set; }
        public bool IsConnectionEncrypted { get; set; } = true;
        public bool IsCommandEncrypted { get; set; } = false;
    }

    public class Service
    {
        [Required]
        public ushort Index { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Name { get; set; }
        public object Provider { get; set; }
        public List<Parameter> Parameters { get; set; }
    }

    public class Parameter
    {
        [Required]
        public ushort Index { get; set; }
        [Required]
        public string Name { get; set; }
        public string DataType { get; set; } = typeof(string).Name;
        public string RegExp { get; set; } = string.Empty;
        [Required]
        public string Type { get; set; }
        public object DefaultValue { get; set; } = null;
        public bool IsValueEncrypted { get; set; } = false;
    }

}
