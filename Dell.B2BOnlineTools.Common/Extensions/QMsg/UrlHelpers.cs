
using Dell.B2BOnlineTools.Common.Models.QMsg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Dell.B2BOnlineTools.Common.Extensions.QMsg
{
    public static partial class QMsgHelper
    {
        public static string Endpoint(this Url url) => url.IsEndPointEncrypted ? url.Endpoint.Decrypt() : url.Endpoint;
        public static HttpContentType HttpContentType(this Url url)
        {
            switch (url.DataType.ToLowerInvariant())
            {
                case Constants.QMsg.Url.DataType.XML:
                    return Common.HttpContentType.Xml;
                default:
                    return Common.HttpContentType.Json;
            }
        }
        public static HttpMethod HttpMethod(this Url url)
        {
            switch (url.Method.ToLowerInvariant())
            {
                case Constants.QMsg.Url.Method.POST:
                    return Common.HttpMethod.Post;
                case Constants.QMsg.Url.Method.DELETE:
                    return Common.HttpMethod.Delete;
                case Constants.QMsg.Url.Method.PUT:
                    return Common.HttpMethod.Put;
                default:
                    return Common.HttpMethod.Get;
            }
        }
        public static Dictionary<string, string> HttpHeaders(this Url url)
        {
            if (url.Headers != null && !url.IsHeadersEncrypted)
                return url.Headers;
            else if(url.Headers != null && url.IsHeadersEncrypted)
                return url.Headers.ToDictionary(x => x.Key.Decrypt(), x => x.Value.Decrypt());
            return null;
        }
        public static HttpClientHandler HttpClientHandler(this Url url)
        {
            if (url.Credential != null && !url.Credential.UseDefaultCredential)
            {
                return new HttpClientHandler()
                {
                    Credentials = new CredentialCache()
                    {
                        {
                            new Uri(url.Endpoint()),
                            Environment.OSVersion.Platform == PlatformID.Unix ? "NTLM" : "NEGOTIATE",
                            new NetworkCredential()
                            {
                                UserName = url.IsCredentialEncrypted ? url.Credential.UserName.Decrypt() : url.Credential.UserName,
                                Password = url.IsCredentialEncrypted ? url.Credential.Password.Decrypt() : url.Credential.Password,
                                Domain = !string.IsNullOrEmpty(url.Credential.Domain)
                                        ? url.IsCredentialEncrypted ? url.Credential.Domain.Decrypt() : url.Credential.Domain
                                        : string.Empty
                            }
                        }
                    }
                };
            }
            if (url.Credential != null && url.Credential.UseDefaultCredential)
                return new HttpClientHandler() { UseDefaultCredentials = true };
            return null;
        }

    }
}
