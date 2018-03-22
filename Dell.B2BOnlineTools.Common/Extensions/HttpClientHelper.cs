
using Dell.B2BOnlineTools.Common.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dell.B2BOnlineTools.Common.Extensions
{
    public static class HttpClientHelper
    {
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            HttpRequestMessage request = new HttpRequestMessage(new System.Net.Http.HttpMethod("PATCH"), requestUri);
            request.Content = content;
            return await client.SendAsync(request);
        }
        public static HttpClientHandler ToHttpClientHandler(this Credential credential)
        {
            if (credential == null) return null;
            HttpClientHandler clientHandler = new HttpClientHandler();
            if (credential.UseDefaultCredential)
                clientHandler.UseDefaultCredentials = true;
            else
            {
                var networkCredential = new NetworkCredential()
                {
                    UserName = credential.UserName,
                    Password = credential.Password
                };
                if (!string.IsNullOrEmpty(credential.Domain))
                    networkCredential.Domain = credential.Domain;
                clientHandler.Credentials = networkCredential;
            }
            return clientHandler;
        }

    }//End of Class
}
