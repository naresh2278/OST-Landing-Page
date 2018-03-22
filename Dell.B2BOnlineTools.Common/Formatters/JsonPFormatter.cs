//using Newtonsoft.Json;
//using System;
//using System.IO;
//using System.Net.Http.Formatting;
//using System.Net.Http.Headers;
//using System.Threading.Tasks;
//using System.Web;

//namespace Dell.B2BOnlineTools.Common.Formatters
//{
//    public class JsonPFormatter : MediaTypeFormatter
//    {
//        public JsonPFormatter() => SupportedMediaTypes.Add(new MediaTypeHeaderValue(Constants.JsonPContentType));
//        public JsonPFormatter(MediaTypeMapping mediaTypeMapping) : this() => MediaTypeMappings.Add(mediaTypeMapping);

//        //THis checks if you can POST or PUT to this media-formater
//        public override bool CanReadType(Type type) => false;
//        //this checks if you can GET this media. You can exclude or include your Resources by checking for their types
//        public override bool CanWriteType(Type type) => true;

//        //This actually takes the data and writes it to the response 
//        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream,
//            System.Net.Http.HttpContent content, System.Net.TransportContext transportContext)
//        {
//            //you can cast your entity
//            //MyType entity=(MyType) value;
//            var callback = HttpContext.Current.Request.Params["callback"];
//            return Task.Factory.StartNew(() =>
//            {
//                using (StreamWriter sw = new StreamWriter(writeStream))
//                {
//                    if (string.IsNullOrEmpty(callback)) callback = "values";
//                    sw.Write($@"{callback}({JsonConvert.SerializeObject(value,
//                        Newtonsoft.Json.Formatting.None,
//                        new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
//                    )})");
//                    //sw.Write(callback + "(" + JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.None,
//                    //    new JsonSerializerSettings
//                    //    {
//                    //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//                    //    }) + ")");
//                }

//            });
//        }
//    }
//}
