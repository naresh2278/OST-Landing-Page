using Dell.B2BOnlineTools.Common.Models.QMsg;
using System;
using System.Collections.Generic;

namespace Dell.B2BOnlineTools.Common.Documentation.Samples
{
    public class QMsgSample
    {
        public QueueMessage Instance;
        public QMsgSample()
        {
            var y = new QueueMessage()
            {
                Header = new Header(),
                Body = new Body()
            };
            y.Operation = new Operation()
            {
                Name = "Dell.B2BOnlineTools.OfferDirector.Batch.SmartPriceCaching",
                Type = Constants.QMsg.Operation.Type.Batch,
                Services = new List<Service>()
            };
            y.Operation.Services.Add(new Service()
            {
                Index = 0,
                Name = "GetConfig",
                Type = Constants.QMsg.Service.Type.URL,
                Provider = new Url()
                {
                    Endpoint = "https://dma-g4.cfapps.pcf1.vc1.pcf.dell.com/api/dma/{0}/{1}/{2}/{3}/config/smartprice",
                    
                },
                Parameters = new List<Parameter>()
            });
            y.Operation.Services[0].Parameters.Add(new Parameter()
            {
                Index = 0,
                Name = "customersetId",
                Type = Constants.QMsg.Parameter.Type.Url,
                DefaultValue = "g_8"
            });
            y.Operation.Services[0].Parameters.Add(new Parameter()
            {
                Index = 1,
                Name = "catalogId",
                DataType = typeof(int).Name,
                Type = Constants.QMsg.Parameter.Type.Url,
                DefaultValue = 8                
            });
            y.Operation.Services[0].Parameters.Add(new Parameter()
            {
                Index = 2,
                Name = "orderCode",
                Type = Constants.QMsg.Parameter.Type.Url
            });
            y.Operation.Services[0].Parameters.Add(new Parameter()
            {
                Index = 3,
                Name = "languageId",
                DataType = typeof(int).Name,
                Type = Constants.QMsg.Parameter.Type.Url
            });
            y.Header.Channel = new Channel()
            {
                Name = "channelName",
                Type = Constants.QMsg.Channel.Type.RabbitMQ                
            };
            y.Header.Destination = new Destination
            {
                Name = "destName",
                Type = new string[] { "ado", "WebApi" },
                Services = new List<Service>()
            };
            y.Header.Destination.Services.Add(new Service()
            {
                Index = 0,
                Type = Constants.QMsg.Service.Type.ADO,
                Name = "SavePayload",
                Provider = new Ado
                {
                    Type = Constants.QMsg.Ado.Type.MsSql,
                    CommandType = Constants.QMsg.Ado.CommandType.StoredProcedure,
                    CommandText = "Abcd",
                    ConnectionString = "",
                },                
            });
            y.Header.Source = new Source { Name = "SourceAppName" };
            y.Body.Items = new Items()
            {
                Arguments = new System.Collections.Generic.List<ArgInfo>(),
                Values = new System.Collections.Generic.List<object[]>()
            };
            y.Body.Items.Arguments.Add(new ArgInfo()
            {
                Index = 0,
                Name = "customersetId",
            });
            y.Body.Items.Arguments.Add(new ArgInfo()
            {
                Index = 1,
                Name = "catalogId",
                DataType = typeof(int).Name
            });
            y.Body.Items.Arguments.Add(new ArgInfo()
            {
                Index = 2,
                Name = "orderCode",
            });
            y.Body.Items.Arguments.Add(new ArgInfo()
            {
                Index = 3,
                Name = "languageId",
                DataType = typeof(int).Name
            });
            y.Body.Items.Values.Add(new object[] { "g_8", 8, "CUP5810XLW7PM", 5 });
            y.Body.Items.Values.Add(new object[] { "g_8", 8, "DST_Latitude_100_Automation", 1 });
            Instance = y;
        }
    }
}
