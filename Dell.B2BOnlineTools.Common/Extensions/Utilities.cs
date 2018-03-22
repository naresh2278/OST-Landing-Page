using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace Dell.B2BOnlineTools.Common.Extensions
{
    public sealed class Utilities
    {
        public static IEnumerable<string> GetAssemblyList()
        {
            List<string> dlls = new List<string>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .OrderBy(x => x.FullName).ToList();
            foreach (var assembly in assemblies)
            {
                if (!assembly.FullName.ToLowerInvariant().Contains("version=0.0.0.0"))
                {
                    yield return assembly.FullName;
                }
            }
        }

        public static string GetAssemblyListAsHtml()
        {//Dell.B2BOnlineTools.SmartPrice.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            StringBuilder sb = new StringBuilder();
            sb.Append("<!DOCTYPE html><head>");
            sb.Append("<meta charset='utf-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'>");
            sb.Append("<title>Assembly Version</title>");
            sb.Append("</head><body>");
            sb.Append("<table border='1'><tr>");
            //sb.Append($"<caption>{Environment.OSVersion.ToString()}</caption>");
            sb.Append("<th>Sl.</th><th>Assembly</th><th>Version</th>");
            //sb.Append("<th>Culture</th><th>Public Key Token</th>");
            sb.Append("</tr>");

            int i = 1;
            var currentAssembly = Assembly.GetCallingAssembly().GetName().FullName;
            GetAssemblyList().ToList().ForEach(x =>
            {
                bool highlight = string.Compare(x, currentAssembly) == 0;
                sb.Append($"<tr style='background-color:{(highlight ? "#5995f7" : "none")}'><td>{i++}</td>");
                x.Split(',').Take(2).ToList().ForEach(y =>
                {
                    if (y.Contains('='))
                        sb.Append($"<td>{y.Split('=')[1].Trim()}</td>");
                    else
                        sb.Append($"<td>{y.Trim()}</td>");
                });
                sb.Append("</tr>");
            });
            sb.Append("</table>");
            sb.Append("</body></html>");
            return sb.ToString();
        }

        public static T HttpClient<T>(string url, HttpMethod httpMethod, HttpContentType httpContentType, out HttpStatusCode statusCode,
            string postBody = null, string requestUri = "",
            Dictionary<string, string> requestHeaders = null, HttpClientHandler handler = null, ushort? TimeOutInSeconds = null)
        {
            try
            {                
                var jsonString = string.Empty;
                if (requestUri == null) requestUri = string.Empty;
                HttpClient client = handler != null ? new HttpClient(handler) : new HttpClient();                
                if (TimeOutInSeconds.HasValue) client.Timeout = TimeSpan.FromSeconds(TimeOutInSeconds.Value);
                client.BaseAddress = new Uri(url);

                switch (httpContentType)
                {
                    case HttpContentType.Html:
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Dell.B2BOnlineTools.Common.Constants.HtmlContentType));
                        break;
                    case HttpContentType.Json:
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Dell.B2BOnlineTools.Common.Constants.JsonContentType));
                        break;
                    case HttpContentType.Xml:
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Dell.B2BOnlineTools.Common.Constants.XmlContentType));
                        break;
                }

                if (requestHeaders != null)
                { requestHeaders.Keys.ToList().ForEach(key => client.DefaultRequestHeaders.Add(key, requestHeaders[key])); }

                HttpResponseMessage responseMessage = null;
                if (httpMethod == HttpMethod.Delete)
                { }
                else if (httpMethod == HttpMethod.Get)
                { responseMessage = client.GetAsync(requestUri).Result; }
                else if (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put || httpMethod == HttpMethod.Patch)
                {
                    HttpContent content = new StringContent(postBody, Encoding.UTF8,
                        httpContentType == HttpContentType.Json
                            ? Dell.B2BOnlineTools.Common.Constants.JsonContentType
                            : httpContentType == HttpContentType.Html
                                ? Dell.B2BOnlineTools.Common.Constants.HtmlContentType
                                : Dell.B2BOnlineTools.Common.Constants.XmlContentType);
                    switch (httpMethod)
                    {
                        case HttpMethod.Post:
                            responseMessage = client.PostAsync(requestUri, content).Result;
                            break;
                        case HttpMethod.Put:
                            responseMessage = client.PutAsync(requestUri, content).Result;
                            break;
                        case HttpMethod.Patch:
                            responseMessage = client.PatchAsync(requestUri, content).Result;
                            break;
                    }
                }

                if (responseMessage != null)
                {
                    statusCode = responseMessage.StatusCode;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        jsonString = responseMessage.Content.ReadAsStringAsync().Result;
                        if (typeof(T) == typeof(string))
                            return (T) Convert.ChangeType(jsonString, typeof(string));
                        T result = JsonConvert.DeserializeObject<T>(jsonString);
                        return (T)result;
                    }
                }
                else { statusCode = HttpStatusCode.NoContent; }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.ToString()}");
                throw;
            }
            return default(T);
        }//End of HttpClient
        
    }//End of Class    
}
